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
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            var opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            var silvm = new StrafViewModel.StrafListIndexViewModel();
            foreach (var s in opvangtehuis.GetStraffen())
            {
                silvm.AddStrafIndexViewModel(new StrafViewModel.StrafIndexViewModel(s.Id, s.ImageUrl, s.Naam));
            }
            return View(silvm);
        }

        [HttpPost]
        public ActionResult Index(StrafViewModel.StrafListIndexViewModel model)
        {
            Opvangtehuis opvangtehuis;
            if (UserStillLoggedIn() || !(_gebruikerRepository.FindById((int)Session["gebruiker"]) is Opvoeder))
            {
                return ReturnToLogin();
            }

            if (!ImageIsValidType(model.StrafIndexViewModel.ImageUpload))
            {
                ModelState.AddModelError("ImageUpload", "Dit is geen foto");
            }

            if (ModelState.IsValid)
            {
                opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
                opvangtehuis.AddStraf(new Straf(model.StrafIndexViewModel.Naam, ImageUploadStrafAfbeeling(model.StrafIndexViewModel.ImageUpload)));
                _gebruikerRepository.SaveChanges();

                this.AddNotification("Straf toegevoegd", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }

            opvangtehuis = _gebruikerRepository.FindById((int)Session["gebruiker"]).Opvangtehuis;
            var silvm = new StrafViewModel.StrafListIndexViewModel();
            foreach (var s in opvangtehuis.GetStraffen())
            {
                silvm.AddStrafIndexViewModel(new StrafViewModel.StrafIndexViewModel(s.Id, s.ImageUrl, s.Naam));
            }
            return View(silvm);
        }

        #region helper
        public string ImageUploadStrafAfbeeling(HttpPostedFileBase file)
        {
            if (file != null)
            {
                var pic = System.IO.Path.GetFileName(file.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("~/Content/Images/StrafImages"), pic);

                file.SaveAs(path);

                return "/Content/Images/StrafImages/" + pic;
            }
            return "/Content/Images/Aanduidingen/vraagteken.png";
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

            if (file == null || validImageTypes.Contains(file.ContentType))
            {
                return true;
            }
            return false;
        }

        public bool UserStillLoggedIn()
        {
            return Session["gebruiker"] == null;
        }

        public ActionResult ReturnToLogin()
        {
            FormsAuthentication.SignOut();
            Session["gebruiker"] = null;
            return RedirectToAction("Login", "Account");
        }
        #endregion
    }
}