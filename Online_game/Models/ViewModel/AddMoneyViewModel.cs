using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_game.Models.ViewModel
{
    public class AddMoneyViewModel
    {
        public int Id { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Description { get; set; }
        public Nullable<int> UserId { get; set; }
    }

    public class AddMoneyView
    {
        public int Id { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}