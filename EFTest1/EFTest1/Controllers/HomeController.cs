using EFTest1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EFTest1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Requests()
        {
            RequestSearchVM model = new RequestSearchVM();
            model.PopulateModel("");

            ViewData.Model = model;

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Requests(string searchValue)
        {
            RequestSearchVM model = new RequestSearchVM();
            model.PopulateModel(searchValue);

            ViewData.Model = model;

            return View();
        }

        [Authorize]
        public async Task<ActionResult> RequestDetails(int ID)
        {
            RequestDetailsVM model = new RequestDetailsVM();
            model.RequestID = ID;
            await model.PopulateModel();
            await model.PopulateLists();

            ViewData.Model = model;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> RequestDetails(RequestDetailsVM model)
        {
            if(ModelState.IsValid)
            {

            }

            await model.PopulateLists();
            return View(model);
        }
    }
}