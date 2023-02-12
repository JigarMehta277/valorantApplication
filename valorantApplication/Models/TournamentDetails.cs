using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace valorantApplication.Models
{
    public class TournamentDetails
    {
        [Key]
        public int TournamentId { get; set; }
        public string LatestTournament { get; set; }
        
        public string LatestAgent { get; set; }

        public int TotalKills { get; set; }

        public string result { get; set; }

        //A tournamnet can have many users

        public ICollection<valorantUser> valorantUsers { get; set; }
    }

    public class TournamentDetailsDto
    {
        public int TournamentId { get; set; }
        public string LatestTournament { get; set; }

        public string LatestAgent { get; set; }

        public int TotalKills { get; set; }

        public string result { get; set; }
    }
}