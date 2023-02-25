using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_game.Models.ViewModel;
using Online_game.Models.Domain;

namespace Online_game.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
       onlinegame_AdminEntities ent = new onlinegame_AdminEntities();
        public ActionResult PlayJodiList()
        {
            try
            {
                var data = ent.PlayJodis.ToList();
                return View(data);
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }
        public ActionResult AddPlayJodi()
        {
            try
            {
                ViewBag.gamenumber = new SelectList(ent.GameNumbers.ToList(), "ID" , "Number");
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult AddPlayJodi(PlayJodiViewModel model)
        {
            try
            {
                ViewBag.gamenumber = new SelectList(ent.GameNumbers.ToList(), "ID", "Number");
                if (ModelState.IsValid)
                {
                    PlayJodi pd = new PlayJodi()
                    {
                        GameName = model.GameName,
                        CreateDate = model.CreateDate,
                        IsActive = model.IsActive,
                        GameNumber = model.GameNumber,
                        Amount = model.Amount
                    };
                    ent.PlayJodis.Add(pd);
                    ent.SaveChanges();
                }
                TempData["msg"] = "Game Added Successfully";
                return RedirectToAction("AddPlayJodi");
            }
            catch (Exception ex)
            {
                throw new Exception("Server Error" + ex.Message);
            }
        }

    }
}