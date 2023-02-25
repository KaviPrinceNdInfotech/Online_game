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
    public class GameController : Controller
    {
        onlinegame_AdminEntities ent = new onlinegame_AdminEntities();
        public ActionResult GameList()
        {
            try
            {
                var data = ent.Games.ToList();
                return View(data);
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }

        public ActionResult AddGame()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }
        [HttpPost]
        public ActionResult AddGame(GameModel model)
        {
            try
            {
                if (model != null)
                {
                    Game gm = new Game()
                    {
                        GameName = model.GameName,
                    };
                    ent.Games.Add(gm);
                    ent.SaveChanges();
                }
                return RedirectToAction("GameList");
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }

        public ActionResult UpdateGame(int id)
        {
            try
            {
                var result = ent.Games.FirstOrDefault(x =>x.Id == id);
                return View(result);
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }
        [HttpPost]
        public ActionResult UpdateGame(int id,GameModel model)
        {
            try
            {
                var result = ent.Games.FirstOrDefault(x =>x.Id == id);
                if(result != null)
                {
                    result.GameName = model.GameName;
                }
                ent.SaveChanges();
                return RedirectToAction("GameList");
            }
            catch (Exception ex)
            {
                throw new Exception("Server Error" + ex.Message);
            }
        }

        public ActionResult DeleteGame(int id)
        {
            try
            {
                var result = ent.Games.FirstOrDefault(x =>x.Id == id);
                if (result != null)
                {
                    ent.Games.Remove(result);
                    ent.SaveChanges();
                }
                return RedirectToAction("GameList");
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }
    }
}