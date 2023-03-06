using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_game.Models.ApiViewModel
{
    public class WithdrawFundDTO
    {
        public int Id { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
    public class WithdrawHistoryDTO
    {
        public int Amount { get; set; }
        public string Date { get; set; }
      

    }
}