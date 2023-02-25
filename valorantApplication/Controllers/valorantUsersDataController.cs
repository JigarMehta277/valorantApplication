using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using valorantApplication.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace valorantApplication.Controllers
{
    public class valorantUsersDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/valorantUsersData/ListValorantUsers
        [HttpGet]
        public IEnumerable<ValorantUserDto> ListvalorantUsers()
        {
            List<valorantUser> ValorantUsers = db.valorantUsers.ToList();
            List<ValorantUserDto> ValorantUserDtos = new List<ValorantUserDto>();

            ValorantUsers.ForEach(a => ValorantUserDtos.Add(new ValorantUserDto()
            {
                UserId = a.UserId,
                UserName = a.UserName,
                ValorantID = a.ValorantID,
                MostPlayedAgent = a.MostPlayedAgent,
                PlayHours = a.PlayHours,
                Scale = a.Scale,
                Reason = a.Reason,
                Skill = a.Skill,
                Position = a.Position,
                Strategies = a.Strategies,
                Ranks = a.Ranks
            }));

            return ValorantUserDtos;
        }


        /// <summary>
        /// Returns all the valorantusers for tournamnet details
        /// </summary>
        /// <returns>
        /// HEADER: 200(OK)
        /// CONTENT: all the valorant users in the database
        /// </returns>
        /// <param name="id">valorant user primary key</param>
        /// <returns></returns>
        // GET: api/valorantUsersData/ListvalorantUsersForTournamentDetails/1
        [HttpGet]
        [ResponseType(typeof(ValorantUserDto))]
        public IEnumerable<ValorantUserDto> ListvalorantUsersForTournamentDetails(int id)
        {
            List<valorantUser> ValorantUsers = db.valorantUsers.Where(
                v=>v.TournamentDetails.Any(
                    t=>t.TournamentId == id)
                    ).ToList();
            List<ValorantUserDto> ValorantUserDtos = new List<ValorantUserDto>();

            ValorantUsers.ForEach(a => ValorantUserDtos.Add(new ValorantUserDto()
            {
                UserId = a.UserId,
                UserName = a.UserName,
                ValorantID = a.ValorantID,
                MostPlayedAgent = a.MostPlayedAgent,
                PlayHours = a.PlayHours,
                Scale = a.Scale,
                Reason = a.Reason,
                Skill = a.Skill,
                Position = a.Position,
                Strategies = a.Strategies,
                Ranks = a.Ranks
            }));

            return ValorantUserDtos;
        }

        // GET: api/valorantUsersData/FindValorantUser/5
        [ResponseType(typeof(valorantUser))]
        [HttpGet]
        public IHttpActionResult FindValorantUser(int id)
        {
            valorantUser valorantUser = db.valorantUsers.Find(id);
            ValorantUserDto valorantUserDto = new ValorantUserDto()
            {
                UserId = valorantUser.UserId,
                UserName = valorantUser.UserName,
                ValorantID = valorantUser.ValorantID,
                MostPlayedAgent = valorantUser.MostPlayedAgent,
                PlayHours = valorantUser.PlayHours,
                Scale = valorantUser.Scale,
                Reason = valorantUser.Reason,
                Skill = valorantUser.Skill,
                Position = valorantUser.Position,
                Strategies = valorantUser.Strategies,
                Ranks = valorantUser.Ranks
            };
            if (valorantUser == null)
            {
                return NotFound();
            }

            return Ok(valorantUserDto);
        }

        // POST: api/valorantUsersData/UpdateValorantUser/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateValorantUser(int id, valorantUser valorantUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != valorantUser.UserId)
            {
                return BadRequest();
            }

            db.Entry(valorantUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!valorantUserExists(id))
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

        // POST: api/valorantUsersData/AddValorantUser
        [ResponseType(typeof(valorantUser))]
        [HttpPost]
        public IHttpActionResult AddValorantUser(valorantUser valorantUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.valorantUsers.Add(valorantUser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = valorantUser.UserId }, valorantUser);
        }

        // POST: api/valorantUsersData/DeleteValorantUser/5
        [ResponseType(typeof(valorantUser))]
        [HttpPost]
        public IHttpActionResult DeletevalorantUser(int id)
        {
            valorantUser valorantUser = db.valorantUsers.Find(id);
            if (valorantUser == null)
            {
                return NotFound();
            }

            db.valorantUsers.Remove(valorantUser);
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

        private bool valorantUserExists(int id)
        {
            return db.valorantUsers.Count(e => e.UserId == id) > 0;
        }
    }
}