using System;
using System.Linq;
using System.Threading.Tasks;
using CommonModels;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ServiceRepository _service;

        public HomeController(ServiceRepository service, ILogger<HomeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: Home/Index
        // Returns view where user can search Cabins
        public IActionResult Index()
        {
            ViewBag.FirstEntry = true;
            return View();
        }

        // POST: Home/ByResort
        // This is because can't send HttpGet in _ResortsDropDownPartial
        [HttpPost]
        public ActionResult ByResort (CabinSearch cabinSearch)
        {
            return RedirectToAction("Search", cabinSearch);
        }

        // Post: Home/Index
        // Returns view with List of Cabins by given search conditions
        [HttpGet]
        public async Task<ActionResult> Search(CabinSearch cabinSearch)
        {
            if (cabinSearch.Rooms == null) cabinSearch.Rooms = "1";
            else cabinSearch.Rooms = cabinSearch.Rooms.Remove(cabinSearch.Rooms.Length - 14);
            if (cabinSearch.Rooms == ">10") cabinSearch.Rooms = "11";

            var cabins = await _service.GetCabins(cabinSearch.SearchWord, cabinSearch.Arrival, cabinSearch.Departure, cabinSearch.Rooms);
            ViewBag.FirstEntry = false;

            if (cabinSearch.Arrival != DateTime.MinValue) ViewBag.Arrival = cabinSearch.Arrival.ToString("dd'.'MM'.'yyyy");
            if (cabinSearch.Departure != DateTime.MinValue) ViewBag.Departure = cabinSearch.Departure.ToString("dd'.'MM'.'yyyy");

            var pageNumbers = 1;
            var pageSize = 10;

            if (cabins != null)
            {
                switch (cabinSearch.Sort)
                {
                    case "Hinta - Halvimmat ensin":
                        cabins = cabins.OrderBy(cabin => cabin.CabinPricePerDay);
                        break;
                    case "Hinta - Kalleimmat ensin":
                        cabins = cabins.OrderByDescending(cabin => cabin.CabinPricePerDay);
                        break;
                    case "Makuuhuoneet - Max.":
                        cabins = cabins.OrderByDescending(cabin => cabin.Rooms);
                        break;
                    case "Makuuhuoneet - Min.":
                        cabins = cabins.OrderBy(cabin => cabin.Rooms);
                        break;
                    default:
                        cabins = cabins.OrderBy(cabin => cabin.Rooms);
                        cabinSearch.Sort = "Makuuhuoneet - Min.";
                        break;

                        //case "Pinta-ala - Suurimmat ensin":
                        //    cabins = cabins.OrderByDescending(cabin => cabin.Area);
                        //    break;
                        //case "Pinta-ala - Pienimmät ensin":
                        //    cabins = cabins.OrderBy(cabin => cabin.Area);
                        //    break;
                        //case "Nimi - Laskeva aakkosjärjestys":
                        //    cabins = cabins.OrderBy(cabin => cabin.CabinName);
                        //    break;
                        //case "Nimi - Nouseva aakkosjärjestys":
                        //    cabins = cabins.OrderByDescending(cabin => cabin.CabinName);
                        //    break;
                }

                ViewBag.SelectedSorting = cabinSearch.Sort;

                // Counting page numbers
                if (cabins.Count() > pageSize)
                {
                    pageNumbers += cabins.Count() / pageSize;
                    if (cabins.Count() % pageSize == 0) pageNumbers--;
                }

                if (cabinSearch.PageNumber == 0) cabinSearch.PageNumber = 1;

                cabins = cabins.Skip((cabinSearch.PageNumber - 1) * pageSize)
                  .Take(pageSize);

                ViewBag.Cabins = cabins;

                ViewBag.PageNumbers = pageNumbers;
            }

            return View(cabinSearch);
        }

        // GET: Home/Details/5
        // Return view with selected Cabin details
        public async Task<ActionResult> Details(int id)
        {
            Cabin cabin = await _service.GetCabin(id);
            return View(cabin);
        }

        // GET: Home/BecomeCabinOwner
        // Returns View where User can send Request to become CabinOwner
        public ActionResult BecomeCabinOwner()
        {
            if (User.IsInRole("CabinOwner") || User.IsInRole("Administrator")) return RedirectToAction("Create", "Cabins");
            return View();
        }
    }
}