using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using valorantApplication.Models;

namespace valorantApplication.Controllers
{
    public class TournamentDetailsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TournamentDetailsData/ListTournamentDetails
        [HttpGet]
        public IEnumerable<TournamentDetailsDto> ListTournamentDetails()
        {
            List<TournamentDetails> TournamentDetail = db.TournamentDetails.ToList();
            List<TournamentDetailsDto> TournamentDetailsDtos = new List<TournamentDetailsDto>();

            TournamentDetail.ForEach(a => TournamentDetailsDtos.Add(new TournamentDetailsDto()
            {
                TournamentId = a.TournamentId,
                LatestTournament = a.LatestTournament,
                LatestAgent = a.LatestAgent,
                TotalKills = a.TotalKills,
                result = a.result
            }));
            return TournamentDetailsDtos;
        }

        /// <summary>
        /// Gathers information about tournaments details related to a particular valorant user.
        /// </summary>
        /// <returns>
        /// HEADER: 200(OK)
        /// CONTENT:
        /// </returns>
        /// <param name="id">valorant user id</param>
        /// <example>
        /// GET: api/TournamentDetailsData/ListTournamentDetailsForValorantUser/3
        /// </example>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(TournamentDetailsDto))]
        public IHttpActionResult ListTournamentDetailsForValorantUser(int id)
        {
            //all tournament details that have valorant user which match with our ID.
            List<TournamentDetails> TournamentDetail = db.TournamentDetails.Where(
                a=>a.valorantUsers.Any(
                   v=>v.UserId==id    
                )).ToList();
            List<TournamentDetailsDto> TournamentDetailsDtos = new List<TournamentDetailsDto>();

            TournamentDetail.ForEach(a => TournamentDetailsDtos.Add(new TournamentDetailsDto()
            {
                TournamentId = a.TournamentId,
                LatestTournament = a.LatestTournament,
                LatestAgent = a.LatestAgent,
                TotalKills = a.TotalKills,
                result = a.result
            }));

            return Ok(TournamentDetailsDtos);
        }

        // GET: api/TournamentDetailsData/FindTournamentDetails/5
        [ResponseType(typeof(TournamentDetails))]
        [HttpGet]
        public IHttpActionResult FindTournamentDetails(int id)
        {
            TournamentDetails tournamentDetails = db.TournamentDetails.Find(id);
            TournamentDetailsDto tournamentDetailsDto = new TournamentDetailsDto()
            {
                TournamentId = tournamentDetails.TournamentId,
                LatestTournament = tournamentDetails.LatestTournament,
                TotalKills = tournamentDetails.TotalKills,
                result = tournamentDetails.result
            };
            if (tournamentDetails == null)
            {
                return NotFound();
            }

            return Ok(tournamentDetails);
        }

        // PUT: api/TournamentDetailsData/UpdateTournamentDetails/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTournamentDetails(int id, TournamentDetails tournamentDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tournamentDetails.TournamentId)
            {
                return BadRequest();
            }

            db.Entry(tournamentDetails).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TournamentDetailsData/AddTournamentDetails
        [ResponseType(typeof(TournamentDetails))]
        [HttpPost]
        public IHttpActionResult AddTournamentDetails(TournamentDetails tournamentDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TournamentDetails.Add(tournamentDetails);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tournamentDetails.TournamentId }, tournamentDetails);
        }

        // DELETE: api/TournamentDetailsData/DeleteTournamentDetails/5
        [ResponseType(typeof(TournamentDetails))]
        [HttpPost]
        public IHttpActionResult DeleteTournamentDetails(int id)
        {
            TournamentDetails tournamentDetails = db.TournamentDetails.Find(id);
            if (tournamentDetails == null)
            {
                return NotFound();
            }

            db.TournamentDetails.Remove(tournamentDetails);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TournamentDetailsExists(int id)
        {
            return db.TournamentDetails.Count(e => e.TournamentId == id) > 0;
        }
    }
}