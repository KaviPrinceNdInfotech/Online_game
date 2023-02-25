using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_game.Models.Domain;
using Online_game.Models.ApiViewModel;

namespace Online_game.Controllers.Api
{
    public class GameApiController : Controller
    {
        
        onlinegame_AdminEntities ent = new onlinegame_AdminEntities();

        
        [HttpGet, Route("GameApi/PlayJodiList")]
        public JsonResult PlayJodiList()
        {
            try
            {
                var data1 = (from p in ent.Games
                             select new GamesDTO
                             {
                                 Id = p.Id,
                                 GameName = p.GameName
                             }).ToList();
                var data2 = (from n in ent.GameNumbers
                             select new GameNumberDTO { 
                             ID = n.ID,
                             Number = n.Number
                             }).ToList();

                if(data1 != null)
                {
                    return Json(new { Status = 200, Message = "Success", data1,data2},JsonRequestBehavior.AllowGet);
                }
                
                 return Json(new { Status = 400, Message = "Data Not Found"});
              
            }
            catch (Exception ex) 
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }

        [HttpPost, Route("GameApi/AddPlayJodi")]
        public JsonResult AddPlayJodi(AddPlayJodiDTO model)
        {
            try
            {
                if(model != null)
                {
                    //int check = Convert.ToInt32(Session["UserId"]);
                    var userid = ent.Registrations.FirstOrDefault(x =>x.id == model.UserId);
                    PlayJodi data = new PlayJodi();
                    {
                        data.GameName = model.GameName;
                        data.GameNumber = model.GameNumber;
                        data.Amount = model.Amount;
                        data.CreateDate = DateTime.Now.Date;
                        data.IsActive = true;
                        data.UserId = model.UserId;
                    }
                    ent.PlayJodis.Add(data);
                    ent.SaveChanges();
                }
                return Json(new { Status = 200, Message = "Money Added Successfully" });
            }
            catch (Exception)
            {

                return Json(new {Status=500,Message="Server Error"});
            }
                
        }

        [HttpGet, Route("GameApi/PlayHarupList")]
        public JsonResult PlayHarupList()
        {
            try
            {
                var data = (from p in ent.Games
                             select new GamesDTO
                             {
                                 Id = p.Id,
                                 GameName = p.GameName
                             }).ToList();
                return Json(new {Status=200,data,Message="Success"},JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }

        [HttpPost, Route("GameApi/PlayHarupAdd")]
        public JsonResult PlayHarupAdd(PlayHarupDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PlayHarup ph = new PlayHarup()
                    {
                        Aside = model.Aside,
                        Bside = model.Bside,
                        GameId = model.GameId,
                        Amount = model.Amount,
                        CreateDate = DateTime.Now
                    };
                    ent.PlayHarups.Add(ph);
                    ent.SaveChanges();
                    return Json(new {Status=200,
                    model.Aside,
                    model.Bside,
                    model.GameId,
                    model.Amount,
                    Message="Record has been added"
                    });
                }
                return Json(new
                {
                    Status = 400,
                    model.Aside,
                    model.Bside,
                    model.GameId,
                    model.Amount,
                    Message = "Record has not been added"
                });

            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }

        [HttpGet, Route("GameApi/GameNumberHarupList")]
        public JsonResult GameNumberHarupList()
        {
            try
            {
                var data = (from p in ent.GameNumberHarups
                            select new GameHarupNumDTO
                            {
                                Id = p.Id,
                                Aside = p.Aside,
                                Bside = p.Bside
                            }).ToList();
                return Json(new { Status = 200, data, Message = "Success" },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }
        

        
    }
}