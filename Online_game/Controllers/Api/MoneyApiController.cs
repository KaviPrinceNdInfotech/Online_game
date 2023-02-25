using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_game.Models.ApiViewModel;
using Online_game.Models.Domain;


namespace Online_game.Controllers.Api
{
    public class MoneyApiController : Controller
    {
        private onlinegame_AdminEntities ent = new onlinegame_AdminEntities();

        [HttpGet, Route("MoneyApi/WalletTransaction/{id}")]
        public JsonResult WalletTransaction(int id)
        {
            try
            {
                var result = ent.Results.FirstOrDefault();
                var Moneydata =  ent.AddMoneys.FirstOrDefault(x =>x.UserId == id);
                var total = Moneydata.Amount;
                var Jodidata = ent.PlayJodis.FirstOrDefault();
                var Jodiamount = Jodidata.Amount;
                var Harupdata = ent.PlayHarups.FirstOrDefault();
                var Harupamount = Harupdata.Amount;
                if (Jodidata.CreateDate > DateTime.Now && Harupdata.CreateDate > DateTime.Now)
                {
                    if (result.JodiId.ToString() == Jodidata.GameName.ToString())
                    {
                        var amc = total + Jodiamount;
                        Moneydata.Amount = amc;
                        if (amc != null)
                        {
                            WalletTransaction watl = new WalletTransaction();
                            {
                                watl.CreateDate = DateTime.Now;
                                watl.AddAmount = (int)Jodiamount;
                                watl.UserId = id;
                            }
                            ent.WalletTransactions.Add(watl);
                            ent.SaveChanges();
                        }
                        ent.SaveChanges();
                    }
                    else
                    {
                        var amc = total - Jodiamount;
                        Moneydata.Amount = amc;
                        
                        if (amc != null)
                        {
                            WalletTransaction watl = new WalletTransaction();
                            {
                                watl.CreateDate = DateTime.Now;
                                watl.SubAmount = (int)Jodiamount;
                                watl.UserId = id;
                            }
                            ent.WalletTransactions.Add(watl);
                            ent.SaveChanges();
                        }
                        ent.SaveChanges();
                    }
                    if (result.AsideId.ToString() == Harupdata.Aside.ToString())
                    {
                        var amc = total + Harupamount;
                        Moneydata.Amount = amc;
                        if(amc != null)
                        {
                            WalletTransaction watl = new WalletTransaction();
                            {
                                watl.CreateDate = DateTime.Now;
                                watl.AddAmount = (int)Harupamount;
                                watl.UserId = id;
                            }
                            ent.WalletTransactions.Add(watl);
                            ent.SaveChanges();
                        }
                        ent.SaveChanges();

                    }
                    else
                    {
                        var amc = total - Harupamount;
                        Moneydata.Amount = amc;
                        if (amc != null)
                        {
                            WalletTransaction watl = new WalletTransaction();
                            {
                                watl.CreateDate = DateTime.Now;
                                watl.SubAmount = (int)Harupamount;
                                watl.UserId = id;
                            }
                            ent.WalletTransactions.Add(watl);
                            ent.SaveChanges();
                        }
                        ent.SaveChanges();
                    }
                    if (result.BsideId.ToString() == Harupdata.Bside.ToString())
                    {
                        var amc = total + Harupamount;
                        Moneydata.Amount = amc;
                        if (amc != null)
                        {
                            WalletTransaction watl = new WalletTransaction();
                            {
                                watl.CreateDate = DateTime.Now;
                                watl.AddAmount = (int)Harupamount;
                                watl.UserId = id;
                            }
                            ent.WalletTransactions.Add(watl);
                            ent.SaveChanges();
                        }
                        ent.SaveChanges();
                    }
                    else
                    {
                        var amc = total - Harupamount;
                        Moneydata.Amount = amc;
                        if (amc != null)
                        {
                            WalletTransaction watl = new WalletTransaction();
                            {
                                watl.CreateDate = DateTime.Now;
                                watl.SubAmount = (int)Harupamount;
                                watl.UserId = id;
                            }
                            ent.WalletTransactions.Add(watl);
                            ent.SaveChanges();
                        }
                        ent.SaveChanges();
                    }
                }
                var Total = (from s in ent.AddMoneys 
                               where s.UserId == id select s.Amount).FirstOrDefault();
                
                var Transctions = (from ts in ent.WalletTransactions
                                   join Am in ent.AddMoneys on ts.UserId equals Am.UserId
                                   select new WalletTransctionDTO
                                   {
                                        CreateDate = ts.CreateDate.ToString(),
                                        AddAmount = ts.AddAmount,
                                        SubAmount = ts.SubAmount
                                   }).ToList();
                return Json(new { Status = 200, Total, Jodiamount, Harupamount, Transctions, Messge = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Server Error" + ex.Message);
            }
        }

        [HttpPost, Route("MoneyApi/WithdrawFundsPost")]
        public JsonResult WithdrawFundsPost(WithdrawFundDTO model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new
                    {
                        Status = 200,
                        model.Amount,
                        model.Description,
                        Message = "All fields are requried"
                    });
                }
                WithdrawFund wdf = new WithdrawFund()
                {
                    Amount = model.Amount,
                    Description = model.Description,
                    CreateDate = DateTime.Now.ToString(),
                    UserId = Convert.ToInt32(Session["UserId"])
                };
                ent.WithdrawFunds.Add(wdf);
                ent.SaveChanges();
                return Json(new
                {
                    Status = 200,
                    model.Amount,
                    model.Description,
                    Message = "Record has been added successful"
                });
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }
        
        [HttpGet, Route("MoneyApi/WithdrawHistory/{id}")]
        public JsonResult WithdrawHistory(int id)
        {
            try
            {
                var data = (from a in ent.WithdrawFunds
                            select new WithdrawHistoryDTO
                            {
                                Amount = (int)a.Amount,
                                Date = a.CreateDate
                            }).ToList();
                return Json(new {Status=200, data, Message="Success"},JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw new Exception("Server Error" + ex.Message);
            }
        }
    }
}