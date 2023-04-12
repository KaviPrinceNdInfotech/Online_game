using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Online_game.Models.ApiViewModel;
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
        public ActionResult Bidding()
        {
            var data = ent.Database.SqlQuery<BiddingHistoryViewModel>
                ("select G.GameName,PH.Amount AS MaxAmount,PJ.Amount AS " +
                "MinAmount from Games AS G with(nolock) inner join PlayHarup " +
                "AS PH with(nolock) ON G.Id = PH.GameId INNER JOIN PlayJodis AS " +
                "PJ with(nolock) ON G.Id = PJ.GameNumber").ToList();
            //var data = ent.Database.SqlQuery<BiddingHistoryViewModel>
            //    ("select G.GameName, Min(PH.Amount) AS MinHarupAmount, Max(PH.Amount) AS MaxHarupAmount, Min(PJ.Amount) " +
            //    "AS MinJodisAmount, Max(PJ.Amount) AS MaxJodisAmount from Games AS G /with(nolock) inner join PlayHarup " +
            //    "AS PH with(nolock) ON G.Id = PH.GameId INNER JOIN PlayJodis AS" +
            //    " PJ with(nolock) ON G.Id = PJ.GameNumber group by G.GameName").ToList();
            return View(data);                           
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
    

        public ActionResult ResultList()
        {
            try
            {
                var data = ent.Results.ToList();
                return View(data);
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }
    }
}