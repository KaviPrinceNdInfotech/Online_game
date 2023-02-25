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
    public class PlayHarupController : Controller
    {
        private readonly onlinegame_AdminEntities ent = new onlinegame_AdminEntities();

        public ActionResult ListHarupData()
        {
            try
            {
                var data = ent.PlayHarups.ToList();  
                return View(data);
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }

        public ActionResult AddHarupData()
        {
            ViewBag.games = new SelectList(ent.Games.ToList(), "Id", "GameName");
            return View();
        }
        [HttpPost]
        public ActionResult AddHarupData(PlayHarupViewModel model)
        {
            try
            {
                ViewBag.games = new SelectList(ent.Games.ToList(), "Id", "GameName");
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                PlayHarup ph = new PlayHarup()
                {
                    Aside = model.Aside,
                    Bside = model.Bside,
                    GameId = model.GameId,  
                    Amount = model.Amount,
                    CreateDate = model.CreateDate,
                };
                ent.PlayHarups.Add(ph);
                ent.SaveChanges();
                TempData["msg"] = "Added Successfully";
                return View();
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }
    }
}