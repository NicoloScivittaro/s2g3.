using cinemamltisala.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace cinemamltisala.Controllers
{
    public class HomeController : Controller
    {
        private static List<Ticket> tickets = new List<Ticket>();
        private static List<CinemaHall> halls = new List<CinemaHall>
        {
            new CinemaHall { Nome = "SALA NORD" },
            new CinemaHall { Nome = "SALA EST" },
            new CinemaHall { Nome = "SALA SUD" }
        };

        public IActionResult Index()
        {
            return View(halls);
        }

        [HttpGet]
        public IActionResult SellTicket()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SellTicket(Ticket ticket)
        {
            var hall = halls.FirstOrDefault(h => h.Nome == ticket.Sala);

            if (hall != null && !hall.IsFull())
            {
                hall.BigliettiVenduti++;
                if (ticket.Ridotto)
                {
                    hall.BigliettiRidotti++;
                }

                tickets.Add(ticket);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "La sala selezionata è piena.");
                return View();
            }
        }
    }
}