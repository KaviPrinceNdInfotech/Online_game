﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_game.Models.ViewModel
{
    public class PlayHarupViewModel
    {
        public int Id { get; set; }
        public Nullable<int> Aside { get; set; }
        public Nullable<int> Bside { get; set; }
        public Nullable<int> GameId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}