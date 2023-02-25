using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_game.Models.ViewModel
{
    public class PlayJodiViewModel
    {
        public int ID { get; set; }
        public string GameName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> GameNumber { get; set; }
        public Nullable<int> Amount { get; set; }
    }
}