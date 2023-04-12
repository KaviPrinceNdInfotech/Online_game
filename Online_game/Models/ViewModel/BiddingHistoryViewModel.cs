using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_game.Models.ViewModel
{
    public class BiddingHistoryViewModel
    {
        public decimal MaxAmount { get; set; }
        public int MinAmount { get; set; }
        public string GameName { get; set; }
    }
}