using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace valorantApplication.Models
{
    public class valorantUser
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string ValorantID { get; set; }
        
        public string MostPlayedAgent { get; set; }

        public string PlayHours { get; set; }

        public int Scale { get; set; }

        public string Reason { get; set; }

        public string Skill { get; set; }

        public string Position { get; set; }

        public string Strategies { get; set; }

        public string Ranks { get; set; }

        //A user can participate in many tournaments

        public ICollection<TournamentDetails> TournamentDetails { get; set; }
    }
}