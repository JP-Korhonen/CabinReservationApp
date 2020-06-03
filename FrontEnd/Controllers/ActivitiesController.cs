using System.Linq;
using System.Threading.Tasks;
using CommonModels;
using FrontEnd.Areas.Identity.Data;
using FrontEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly ServiceRepository _service;
        private readonly UserManager<FrontEndUser> _userManager;

        public ActivitiesController(ServiceRepository service, UserManager<FrontEndUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        // GET: Activities/Index
        // Returns view where Administrator can search Activities
        public ActionResult Index(int success)
        {
            // If request came from Create/Edit/Delete
            switch (success)
            {
                case 1:
                    ViewBag.Message = "Lisäpalvelu lisätty onnistuneesti!";
                    break;
                case 2:
                    ViewBag.Message = "Lisäpalvelua muokattu onnistuneesti!";
                    break;
                case 3:
                    ViewBag.Message = "Lisäpalvelu poistettu onnistuneesti!";
                    break;
                default:
                    ViewBag.Message = null;
                    break;
            }

            ViewBag.FirstEntry = true;
            return View();
        }

        // GET: Activities/Index
        // Returns view with List of Activities by search conditions
        [HttpPost]
        public async Task<IActionResult> Index(Activity activity, int pageNumber)
        {
            var activities = await _service.GetActivities(User, activity.Resort.ResortName, activity.ActivityName, activity.ActivityProvider);

            ViewBag.FirstEntry = false;

            // If request came from Resorts/Index, set these
            ViewBag.ResortName = activity.Resort.ResortName;

            var pageNumbers = 1;
            var pageSize = 10;

            if (activities != null)
            {
                // Counting page numbers
                if (activities.Count() > pageSize)
                {
                    pageNumbers += activities.Count() / pageSize;
                    if (activities.Count() % pageSize == 0) pageNumbers--;
                }

                if (pageNumber == 0) pageNumber = 1;

                activities = activities.Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize);

                ViewBag.Activities = activities;

                ViewBag.PageNumbers = pageNumbers;
                ViewBag.PageNumber = pageNumber;
            }

            return View();
        }

        // GET: Activities/Create
        // Returns view where Administrator can create new Activity
        public async Task<ActionResult> Create()
        {
            // Getting Resorts to Dropdownlist
            ViewBag.Resorts = await _service.GetResortsSelectListItems();
            if (null == ViewBag.Resorts)
            {
                ViewBag.ErrorMessage = "Tietokannassa ei ole yhtään toimipistettä";
                return View("ErrorPage");
            }

            // Getting PostalCodes to Dropdownlist
            ViewBag.PostalCodes = await _service.GetPostalCodesSelectListItems();
            if (null == ViewBag.PostalCodes)
            {
                ViewBag.ErrorMessage = "Tietokannassa ei ole yhtään postinumeroa";
                return View("ErrorPage");
            }

            return View();
        }

        // POST: Activities/Create
        // Creates new Activity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Activity activity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool postActivity = await _service.PostActivity(User, activity);

                    if (postActivity) return RedirectToAction("Index", new { success = 1 });

                    return View("ErrorPage");
                }

                // Getting Resorts to Dropdownlist
                ViewBag.Resorts = await _service.GetResortsSelectListItems();
                if (null == ViewBag.Resorts)
                {
                    ViewBag.ErrorMessage = "Tietokannassa ei ole yhtään toimipistettä";
                    return View("ErrorPage");
                }

                // Getting PostalCodes to Dropdownlist
                ViewBag.PostalCodes = await _service.GetPostalCodesSelectListItems();
                if (null == ViewBag.PostalCodes)
                {
                    ViewBag.ErrorMessage = "Tietokannassa ei ole yhtään postinumeroa";
                    return View("ErrorPage");
                }

                return View(activity);
            }
            catch
            {
                return View("ErrorPage");
            }
        }

        // GET: Activities/Details/5
        // Returns view with Activity Details
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var activity = await _service.GetActivity(id);
                return View(activity);
            }
            catch
            {
                return View("ErrorPage");
            }
        }

        // GET: Activities/Edit/5
        // Returns view where Administrator can edit Activity
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                // Getting PostalCodes to Dropdownlist
                ViewBag.PostalCodes = await _service.GetPostalCodesSelectListItems();
                if (null == ViewBag.PostalCodes)
                {
                    ViewBag.ErrorMessage = "Tietokannassa ei ole yhtään postinumeroa";
                    return View("ErrorPage");
                }

                var activity = await _service.GetActivity(id);
                return View(activity);
            }
            catch
            {
                return View("ErrorPage");
            }
        }

        // POST: Activities/Edit/5
        // Edits Activity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Activity activity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool editActivity = await _service.PutActivity(User, id, activity);

                    if (editActivity) return RedirectToAction("Index", new { success = 2 });
                }

                // Getting PostalCodes to Dropdownlist
                ViewBag.PostalCodes = await _service.GetPostalCodesSelectListItems();
                if (null == ViewBag.PostalCodes)
                {
                    ViewBag.ErrorMessage = "Tietokannassa ei ole yhtään postinumeroa";
                    return View("ErrorPage");
                }
                return View(activity);
            }
            catch
            {
                return View("ErrorPage");
            }
        }

        // GET: Activities/Delete/5
        // Returns view where Administrator can delete Activity
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var activity = await _service.GetActivity(id);
                return View(activity);
            }
            catch
            {
                return View("ErrorPage");
            }
        }

        // POST: Activities/Delete/5
        // Deletes Activity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Activity activity)
        {
            try
            {
                bool deleteActivity = await _service.DeleteActivity(User, id);

                if (deleteActivity) return RedirectToAction("Index", new { success = 3 });

                ViewBag.ErrorMessage = "Jos yritit poistaa lisäpalvelua jolla on lisäpalveluvarauksia, poistaminen ei ole sallittua";
                return View("ErrorPage");
            }
            catch
            {
                return View();
            }
        }
    }
}