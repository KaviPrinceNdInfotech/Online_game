using Online_game.Models.ApiViewModel;
using Online_game.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_game.Controllers.Api
{
    public class ResultApiController : Controller
    {
        private onlinegame_AdminEntities ent = new onlinegame_AdminEntities();

        [HttpPost, Route("ResultApi/ViewResult")]
        public JsonResult ViewResult(ResultDTO model)
        {
            try
            {
                var db = ent.Results.FirstOrDefault(x => x.CreateDate == model.Date);
                if (model.Date == null)
                {
                    var data = (from r in ent.Results
                                join g in ent.Games on r.GameId equals g.Id
                                select new ViewResultAllDTO
                                {
                                    Date = r.CreateDate,
                                    GameName = g.GameName,
                                    jodiid = (int)r.JodiId,
                                    Asideid = r.AsideId,
                                    Bsideid = r.BsideId,
                                }).ToList();
                    return Json(new { Status = 200, data, Message = "Success" }, JsonRequestBehavior.AllowGet);
                }
                if (db.TypeGameId == 1)
                {
                    var data = (from r in ent.Results
                                join g in ent.Games on r.GameId equals g.Id
                                where r.CreateDate != model.Date && r.TypeGameId == model.gametype
                                select new ViewResultJodiDTO
                                {
                                    Date = r.CreateDate,
                                    GameName = g.GameName,
                                    jodiid = (int)r.JodiId
                                }).ToList();
                    return Json(new { Status = 200, data, Message = "Success" }, JsonRequestBehavior.AllowGet);
                }
                if (db.TypeGameId == 2)
                {
                    var data = (from r in ent.Results
                                join g in ent.Games on r.GameId equals g.Id
                                where r.CreateDate != model.Date && r.TypeGameId == model.gametype
                                select new ViewResultHarupDTO
                                {
                                    Date = r.CreateDate,
                                    GameName = g.GameName,
                                    Asideid = r.AsideId,
                                    Bsideid = r.BsideId
                                }).ToList();
                    return Json(new { Status = 200, data, Message = "Success" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Status = 400, Message = "Somthing Error" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }

        [HttpGet, Route("ResultApi/BiddingHistory/{id}")]
        public JsonResult BiddingHistory(int id)
        {
            try
            {
               
                var PlayJodi = (from j in ent.PlayJodis
                                join g in ent.Games on j.GameNumber equals g.Id
                             where j.UserId == id
                             select new BiddingHistoryDTO1
                             {
                                 JodiAmc = (int)j.Amount,
                                 JodiDate = j.CreateDate.ToString(),
                                 GameName = g.GameName
                                 
                             }).ToList();
                var Harup = (from h in ent.PlayHarups
                             join g in ent.Games on h.GameId equals g.Id
                             where h.UserId == id
                             select new BiddingHistoryDTO2
                             {
                                 HarupAmc = (int)h.Amount,
                                 HarupDate = h.CreateDate.ToString(),
                                 GameName = g.GameName
                             }).ToList();
                return Json(new
                {
                    Status = 200,
                    PlayJodi,
                    Harup,
                    Message = "Success"
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet, Route("ResultApi/Notification?Length=")]
        public JsonResult Notification(int Length)
        {
            try
            {
                int id = Length;
                var data = ent.Results.FirstOrDefault(x =>x.Id == id);
                var result = (from r in ent.Games
                              where r.Id == data.GameId
                              select r.GameName).FirstOrDefault();
                var message = " "+DateTime.Now.ToString("h:mm tt")+ " " + result + " lucky number is "+data.JodiId+" ";
                return Json(new {Status=200, message, Message="Success"},JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}