using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamification.Models
{
    public class leaderboardViewModel
    {
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public String position { get; set; }
        public String username { get; set; }
        public String iconPath { get; set; }
        public String rcrsDone { get; set; }
        public String score { get; set; }
        public String efficiency { get; set; }
        public List<String> rcrTypes { get; set; }
    }
}