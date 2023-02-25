using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_game.Models.ApiViewModel
{
    public class WalletTransctionDTO
    {
        public string CreateDate { get; set; }
        public Nullable<int> AddAmount { get; set; }
        public Nullable<int> SubAmount { get; set; }
    }
}