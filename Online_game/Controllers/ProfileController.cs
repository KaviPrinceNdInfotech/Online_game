using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_game.Models.ViewModel;
using Online_game.Models.Domain;
using System.Web.Security;

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
            return View();
        }
    }
}