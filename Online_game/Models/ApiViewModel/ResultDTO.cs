using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_game.Models.ApiViewModel
{
    public class ResultDTO
    {
        public string Date { get; set; }
        public int gametype { get; set; }
    }
    public class ViewResultAllDTO
    {
        public string Date { get; set; }
        public string GameName { get; set; }
        public int? jodiid { get; set; }
        public string Asideid { get; set; }
        public string Bsideid { get; set; }
    }
    public class ViewResultJodiDTO
    {
        public string Date { get; set; }
        public string GameName { get; set; }
        public int? jodiid { get; set; }
    }
    public class ViewResultHarupDTO
    {
        public string Date { get; set; }
        public string GameName { get; set; }
        public string Asideid { get; set; }
        public string Bsideid { get; set; }
    }

}