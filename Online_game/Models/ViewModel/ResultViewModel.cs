using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_game.Models.ViewModel
{
    public class ResultViewModel
    {
        public int Id { get; set; }
        public string CreateDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> TypeGameId { get; set; }
        public int? GameId { get; set; }
        public int? JodiId { get; set; }
        public string AsideId { get; set; }
        public string BsideId { get; set; }
    }
}