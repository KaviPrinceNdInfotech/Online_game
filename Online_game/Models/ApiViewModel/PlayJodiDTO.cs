using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Online_game.Models.ApiViewModel
{
    public class GamesDTO
    {
        public int Id { get; set; }

        public string GameName { get; set; }
       
    }
    public class GameNumberDTO
    {
        public int ID { get; set; }
        
        public Nullable<int> Number { get; set; }
    }

    public class AddPlayJodiDTO
    {
        //public int ID { get; set; }
       
        public string GameName { get; set; }
     
        public Nullable<int> GameNumber { get; set; }

        public Nullable<int> Amount { get; set; }
        public int UserId { get; set; }
    }
}