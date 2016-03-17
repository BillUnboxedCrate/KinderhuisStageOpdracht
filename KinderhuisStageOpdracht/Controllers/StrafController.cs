using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinderhuisStageOpdracht.Extensions;
using KinderhuisStageOpdracht.Models.Domain;
using KinderhuisStageOpdracht.Models.Viewmodels;

namespace KinderhuisStageOpdracht.Controllers
{
    public class StrafController : Controller
    {
        private readonly IGebruikerRepository _gebruikerRepository;

        public StrafController(IGebruikerRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
        }

        // GET: Klacht
        public ActionResult Index()
        {
            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            var silvm = new StrafViewModel.StrafListIndexViewModel();
            foreach (var s in opvangtehuis.GetStraffen())
            {
                silvm.AddStrafIndexViewModel(new StrafViewModel.StrafIndexViewModel(s.Id, s.Naam));
            }
            return View(silvm);
        }

        public ActionResult CreateStraf()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStraf(StrafViewModel.StrafIndexViewModel model)
        {
            if (!ImageIsValidType(model.ImageUpload))
            {
                ModelState.AddModelError("ImageUpload", "Dit is geen foto");
            }

            if (ModelState.IsValid)
            {
                var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                opvangtehuis.AddStraf(new Straf(model.Naam, "~/Content/Images/Aanduidingen/vraagteken.png"));
                _gebruikerRepository.SaveChanges();

                this.AddNotification("Straf toegevoegd", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }

            return View();

        }

        #region helper
        public string ImageUploadStrafAfbeeling(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var pic = System.IO.Path.GetFileName(file.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/StrafImages"), pic);

                file.SaveAs(path);

                return path;
            }
            return "~/Content/Images/Aanduidingen/vraagteken.png";
        }

        public bool ImageIsValidType(HttpPostedFileBase file)
        {
            var validImageTypes = new[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (validImageTypes.Contains(file.ContentType))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}