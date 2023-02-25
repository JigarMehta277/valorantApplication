using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace valorantApplication.Models.ViewModels
{
    public class DetailsValorantUser
    {
        public ValorantUserDto selectedvalorantUsers { get; set; }
        public IEnumerable<TournamentDetailsDto> ReservedTournamentDetails { get; set; }
    }
}