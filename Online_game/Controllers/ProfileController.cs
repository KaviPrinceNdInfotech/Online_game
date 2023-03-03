using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_game.Models.ViewModel;
using Online_game.Models.Domain;
using System.Web.Security;
using Online_game.Models.ApiViewModel;
using System.Web.Http.Results;
using System.Web.Services.Description;

namespace Online_game.Controllers
{
    public class ProfileController : Controller
    {
        private readonly onlinegame_AdminEntities ent = new onlinegame_AdminEntities();
      
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                
                if(ModelState.IsValid)
                {
                    var Checked = ent.LoginMasters.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);
                    if (Checked != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        TempData["loginmsg"] = "Login Successfull";
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        TempData["loginmsg"] = "Invalid Username and Password.";
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message );
            }
            return View();
        }
        public ActionResult Dashboard()
        {
            ViewBag.customer = ent.Registrations.ToList().Count();
            ViewBag.Gamelist = ent.Games.ToList().Count();
            ViewBag.Bidding = ent.BiddingHistorys.ToList().Count();

            //ViewBag.totalproduct = ent.Products.ToList().Count();
            return View();
        }
        public ActionResult Userlist()
        {
            try
            {
                var data = ent.Registrations.ToList();
                return View(data);
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ForgetPassword model)
        {
                var message = "";
                if (ModelState.IsValid)
                {
                    if (model.NewPassword == model.ConfirmPassword)
                    {
                        var data = ent.LoginMasters.FirstOrDefault(x => x.Password == model.CurrentPassword);
                    if (data != null)
                    {
                        data.Password = model.NewPassword;
                        ent.SaveChanges();
                        message = "Password change successfully";
                    }
                    }
                }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }
    }      
        
}
