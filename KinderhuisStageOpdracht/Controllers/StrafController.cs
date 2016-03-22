using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

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
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateStraf(StrafViewModel.StrafIndexViewModel model)
        {
            if (UserStillLoggedIn() != null)
            {
                return UserStillLoggedIn();
            }

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

                return "~/Content/Images/StrafImages/" + pic;
            }
            return "~/Content/Images/Aanduidingen/vraagteken.png";
        }

        public bool ImageIsValidType(HttpPostedFileBase file)
        {
            var validImageTypes = new[]
            {
                "image/jpg",
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

        public ActionResult UserStillLoggedIn()
        {
            if (Session["gebruiker"] == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }
            return null;
        }
        #endregion
    }
}