using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace valorantApplication.Models.ViewModels
{
    public class DetailsTournamentDetails
    {
        public TournamentDetailsDto selectedTournamentDetails { get; set; }

        public IEnumerable<ValorantUserDto> ResponsibleValorantUsers { get; set; }
    }
}