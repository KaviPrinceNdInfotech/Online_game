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
    public class ResultController : Controller
    {
        onlinegame_AdminEntities ent = new onlinegame_AdminEntities();
        public ActionResult AddResult()
        {
            try
            {
                ViewBag.GameType = new SelectList(ent.GameTypes.ToList(), "Id", "GameTypeName");
                ViewBag.GameName = new SelectList(ent.Games.ToList(), "Id", "GameName");
                ViewBag.JodiNumber = new SelectList(ent.GameNumbers.ToList(), "ID", "Number");
                ViewBag.HarupNumber = new SelectList(ent.GameNumberHarups.ToList(), "ID", "Aside");
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddResult(ResultViewModel model)
        {
            try
            {
                ViewBag.GameType = new SelectList(ent.GameTypes.ToList(), "Id", "GameTypeName");
                ViewBag.GameName = new SelectList(ent.Games.ToList(), "Id", "GameName");
                ViewBag.JodiNumber = new SelectList(ent.GameNumbers.ToList(), "ID", "Number");
                ViewBag.HarupNumber = new SelectList(ent.GameNumberHarups.ToList(), "ID", "Aside");

                if (!ModelState.IsValid)
                {
                    return View();
                }
                if (model.TypeGameId == 1)
                {
                    Result rs = new Result();
                    {
                        rs.CreateDate = model.CreateDate;
                        rs.IsActive = true;
                        rs.TypeGameId = model.TypeGameId;
                        rs.JodiId = model.JodiId;
                        rs.GameId = model.GameId;
                    };
                    //var id = rs.Id;
                    ent.Results.Add(rs);
                    ent.SaveChanges();
                    var result = (from e in ent.Results orderby e.Id descending  select e.Id ).FirstOrDefault();
                    //int id = result;
                    return RedirectToAction("Notification", "ResultApi", new { Length = result });
                }
                if(model.TypeGameId == 2)
                {
                    Result rs = new Result()
                    {
                        CreateDate = model.CreateDate,
                        IsActive = true,
                        TypeGameId = model.TypeGameId,
                        GameId = model.GameId,
                        AsideId = model.AsideId,
                        BsideId = model.BsideId,
                    };
                    ent.Results.Add(rs);
                    ent.SaveChanges();
                    var result = (from e in ent.Results orderby e.Id descending select e.Id).FirstOrDefault();
                    return RedirectToAction("Notification", "ResultApi", new { Length =result});
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
            return View();
        }
        public JsonResult GetListJodi(int id)
        {
            object data = null;
            if (id == 1)
            {
                data = new SelectList(ent.GameNumbers, "ID", "Number");
            }
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetListHarup(int id)
        {
            object data = null;
            if (id == 2)
            {               
                data = new SelectList(ent.GameNumberHarups, "Id", "Aside");
            }
            if(id == 2)
            {
                data = new SelectList(ent.GameNumberHarups, "Id", "Bside");
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult ShowResult()
        {
            try
            {
                var data = (from g in ent.Results
                            join r in ent.Games on g.Id equals r.Id into table1
                            from r in table1.ToList()
                            join j in ent.PlayJodis on g.JodiId equals j.GameNumber into table2
                            from j in table2.ToList()
                            select new ShowResultViewModel
                            {
                                GameName = r.GameName,
                                GameNumber = (int)j.GameNumber,
                                Amount = (int)j.Amount

                            }).ToList(); 
                    return View(data);
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
            
        }
    }
}