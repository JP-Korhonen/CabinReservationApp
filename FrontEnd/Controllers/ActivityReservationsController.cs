using System;
using System.Linq;
using System.Threading.Tasks;
using CommonModels;
using FrontEnd.Areas.Identity.Data;
using FrontEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class ActivityReservationsController : Controller
    {
        private readonly ServiceRepository _service;
        private readonly UserManager<FrontEndUser> _userManager;

        public ActivityReservationsController(ServiceRepository service, UserManager<FrontEndUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: ActivityReservations/Index
        // Returns view where Administrator can search ActivityReservations
        public ActionResult Index()
        {
            ViewBag.FirstEntry = true;
            return View();
        }

        // POST: ActivityReservations/Index
        // Gets List of ActivityReservations by search conditions
        [HttpPost]
        public async Task<IActionResult> Index(ActivityReservation activityReservation, int pageNumber)
        {
            var activityReservations = await _service.GetActivityReservations(User, activityReservation);

            ViewBag.FirstEntry = false;

            var pageNumbers = 1;
            var pageSize = 10;

            if (activityReservations != null)
            {
                // Counting page numbers
                if (activityReservations.Count() > pageSize)
                {
                    pageNumbers += activityReservations.Count() / pageSize;
                    if (activityReservations.Count() % pageSize == 0) pageNumbers--;
                }

                if (pageNumber == 0) pageNumber = 1;

                activityReservations = activityReservations.Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize);

                ViewBag.PageNumbers = pageNumbers;
                ViewBag.PageNumber = pageNumber;

                // Getting CabinReservations in ActivityReservations, because JsonIgnore-attribute in ActivityReservation.CabinReservation
                foreach (var item in activityReservations)
                {
                    item.CabinReservation = await _service.GetCabinReservation(User, item.CabinReservationId);
                }

                ViewBag.ActivityReservations = activityReservations;
            }

            if (activityReservation.CabinReservation.ReservationStartDate != DateTime.MinValue) ViewBag.Starting = activityReservation.CabinReservation.ReservationStartDate.ToString("dd'.'MM'.'yyyy");
            if (activityReservation.CabinReservation.ReservationEndDate != DateTime.MinValue) ViewBag.Ending = activityReservation.CabinReservation.ReservationEndDate.ToString("dd'.'MM'.'yyyy");

            return View();
        }

        // GET: ActivityReservations/Details/5
        // Returns view with ActivityReservation Details
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var activityReservation = await _service.GetActivityReservation(User, id);
                // Getting CabinReservations in ActivityReservations, because JsonIgnore-attribute in ActivityReservation.CabinReservation
                activityReservation.CabinReservation = await _service.GetCabinReservation(User, activityReservation.CabinReservationId);

                return View(activityReservation);
            }
            catch
            {
                return View("ErrorPage");
            }

        }
    }
}