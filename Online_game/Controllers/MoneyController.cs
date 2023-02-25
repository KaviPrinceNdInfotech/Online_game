using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_game.Models.Domain;
using Online_game.Models.ViewModel;

namespace Online_game.Controllers
{
    [Authorize]
    public class MoneyController : Controller
    {
        private onlinegame_AdminEntities ent = new onlinegame_AdminEntities();
        public ActionResult AddMoney()
        {
            try
            {
                ViewBag.Username = new SelectList(ent.Registrations.ToList(), "id", "Name");
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddMoney(AddMoneyViewModel model)
        {
            try
            {
                ViewBag.Username = new SelectList(ent.Registrations.ToList(), "id", "Name");
                if (!ModelState.IsValid)
                {
                    TempData["msg"] = "All fields are requried.";
                    return View();
                }
                //var add = ent.AddMoneys.FirstOrDefault(x =>x.UserId == Convert.ToInt32(Session["UserId"]));
                var data = ent.AddMoneys.FirstOrDefault(x =>x.UserId == model.UserId);
                if(data != null)
                {
                    var total = data.Amount + model.Amount;
                    if(total != null)
                    {
                        data.Amount = total;
                        ent.SaveChanges();
                    } 
                }
                else
                {
                    AddMoney AddM = new AddMoney()
                    {
                        Amount = model.Amount,
                        Description = model.Description,
                        UserId = model.UserId
                    };
                    ent.AddMoneys.Add(AddM);
                    ent.SaveChanges();
                }
                TempData["msg"] = "Add Money Successfully.";
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
            return View();
        }
        public ActionResult ListMoney()
        {
            try
            {
                var data = (from m in ent.AddMoneys
                            join r in ent.Registrations on m.UserId equals r.id
                            select new AddMoneyView
                            {
                                Amount = m.Amount,
                                Description=m.Description,
                                Name = r.Name
                            }).ToList();
                return View(data);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}