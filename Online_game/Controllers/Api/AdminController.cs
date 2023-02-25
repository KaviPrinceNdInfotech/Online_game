using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Online_game.Models.ApiViewModel;
using Online_game.Models.Domain;
using System.Web.Mvc;

namespace Online_game.Controllers.Api
{
    public class AdminController : Controller
    {
        onlinegame_AdminEntities ent = new onlinegame_AdminEntities();

       
        [HttpPost,Route("Admin/Login")]
        public JsonResult Login(LoginDTO model)
        {
            try
            {
                var data = ent.Registrations.FirstOrDefault(x => x.Mobile_no == model.Mobile_no && x.Password == model.Password);
                
                if(data != null)
                {
                    var money = ent.AddMoneys.FirstOrDefault(x => x.UserId == data.id);
                    Session["UserId"] = data.id;
                    if(money == null)
                    {
                        int Amount = 0;
                        return Json(new { Status = 200, data.id, data.Name, data.Mobile_no, data.Password, Amount, Message = "Login Successfully" });
                    }
                    return Json(new { Status = 200, data.id, data.Name ,data.Mobile_no, data.Password, money.Amount, Message = "Login Successfully" });
                }
                else
                {   
                    return Json(new { Status =400,model.Mobile_no,model.Password, Message = "Invalid Username Or Password" });
                }
            }
            catch 
            {
                return Json(new {Status=400,Message="Server Error"});    
            }
            
        }
        [HttpPost, Route("Admin/Registration")]
        public JsonResult Registration(RegistrationDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Status = 400,
                    model.id,
                    model.Name,
                    model.Mobile_no,
                    model.Password,
                    model.City,
                    Message = "All fields are requried."
                    });
                }
                Registration reg = new Registration()
                {
                    Name = model.Name,
                    Mobile_no = model.Mobile_no,
                    Password = model.Password,
                    City = model.City
                };
                ent.Registrations.Add(reg);
                ent.SaveChanges();
                return Json(new { Status = 200,
                model.id,
                model.Name,
                model.Mobile_no,
                model.Password,
                model.City,
                Message = "Registration Successfull"
                });
            }
            catch
            {

                return Json(new {Status=400,Message="Server Error",model});
            }
        }
        [HttpPut, Route("Admin/ChangePassword")]
        public JsonResult ChangePassword(ForgetPasswordDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Status = 400,
                        model.CurrentPassword,
                        model.NewPassword,
                        model.ConfirmPassword,
                        Message = "All fields are requried."
                    });
                }
                bool Checked = (model.NewPassword == model.ConfirmPassword);
                if (Checked)
                {
                    var data = ent.Registrations.FirstOrDefault(x => x.Password == model.CurrentPassword);
                    if (data != null)
                    {
                        data.Password = model.NewPassword;
                        ent.SaveChanges();
                        return Json(new
                        {
                            Status = 200,
                            model.CurrentPassword,
                            model.NewPassword,
                            model.ConfirmPassword,
                            Message = "Reset Password Successfully",
                        });
                    }
                   
                }
                    return Json(new
                    {
                        Status = 400,
                        model.CurrentPassword,
                        model.NewPassword,
                        model.ConfirmPassword,
                        Message = "Please Enter the Currect Password."
                    });
            }
            catch 
            {

                return Json(new { Status = 400,
                    model.CurrentPassword,
                    model.NewPassword,
                    model.ConfirmPassword,
                    Message = "Server Error"
                });
            }
        }
    }
}
