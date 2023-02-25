using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using valorantApplication.Migrations;
using valorantApplication.Models;
using valorantApplication.Models.ViewModels;

namespace valorantApplication.Controllers
{
    public class TournamentDetailsController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static TournamentDetailsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44321/api/");
        }


        // GET: TournamentDetails/List
        public ActionResult List()
        {

            //objective: communicate with TournamentDetails data api to retrieve a list of TournamentDetails
            //Curl https://localhost:44321/api/valorantUsersData/TournamentDetails

            string url = "TournamentDetailsData/ListTournamentDetails";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<TournamentDetailsDto> TournamentDetails = response.Content.ReadAsAsync<IEnumerable<TournamentDetailsDto>>().Result;
            return View(TournamentDetails);
        }

        // GET: TournamentDetails/Details/5
        public ActionResult Details(int id)
        {

            DetailsTournamentDetails ViewModel = new DetailsTournamentDetails();

            //objective: communicate with TournamentDetails data api to retrieve single TournamentDetails
            //Curl https://localhost:44321/api/valorantUsersData/TournamentDetails/(id)

            string url = "TournamentDetailsData/FindTournamentDetails/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            TournamentDetailsDto selectedTournamentDetails = response.Content.ReadAsAsync<TournamentDetailsDto>().Result;

            ViewModel.selectedTournamentDetails = selectedTournamentDetails;

            //Show

            url = "valorantUsersData/ListvalorantUsersForTournamentDetails/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable< ValorantUserDto > ResponsibleValorantUsers= response.Content.ReadAsAsync<IEnumerable< ValorantUserDto >>().Result;

            ViewModel.ResponsibleValorantUsers = ResponsibleValorantUsers;

            return View(ViewModel);
        }

        public ActionResult Error()
        {


            return View();
        }

        // GET: TournamentDetails/New
        public ActionResult New()
        {
            return View();
        }

        // POST: TournamentDetails/Create
        [HttpPost]
        public ActionResult Create(TournamentDetails tournamentDetails)
        {
            //Curl -H"Content-Type:application/json" -d @ValorantUser.json https://localhost:44321/api/valorantUsersData/AddTournamentDetails
            string url = "TournamentDetailsData/AddTournamentDetails";

            string jsonpayload = jss.Serialize(tournamentDetails);

            HttpContent content = new StringContent(jsonpayload);

            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: TournamentDetails/Edit/id
        public ActionResult Edit(int id)
        {
            //objective: communicate with valorantUser data api to retrieve one valorantUser
            //Curl https://localhost:44321/api/valorantUsersData/FindTournamentDetails/(id)


            string url = "TournamentDetailsData/FindTournamentDetails/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is");
            Debug.WriteLine(response.StatusCode);

            TournamentDetailsDto selectedTournamentDetails = response.Content.ReadAsAsync<TournamentDetailsDto>().Result;
            Debug.WriteLine("valorantUsers recieved: ");
            Debug.WriteLine(selectedTournamentDetails.LatestTournament);

            return View(selectedTournamentDetails);
        }

        // POST: TournamentDetails/Update/id
        [HttpPost]
        public ActionResult Update(int id, TournamentDetails tournamentDetails)
        {
            string url = "TournamentDetailsData/UpdateTournamentDetails/" + id;

            string jsonpayload = jss.Serialize(tournamentDetails);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);

            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: TournamentDetails/DeleteConfirm/id
        public ActionResult DeleteConfirm(int id)
        {
          
            //Curl https://localhost:44321/api/valorantUsersData/FindTournamentDetails/(id)


            string url = "TournamentDetailsData/FindTournamentDetails/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

           

            TournamentDetailsDto selectedTournamentDetails = response.Content.ReadAsAsync<TournamentDetailsDto>().Result;
       

            return View(selectedTournamentDetails);
        }

        // POST: TournamentDetails/Delete/id
        [HttpPost]
        public ActionResult Delete(int id, TournamentDetails tournamentDetails)
        {
            string url = "TournamentDetailsData/DeleteTournamentDetails/" + id;


            string jsonpayload = jss.Serialize(tournamentDetails);


            HttpContent content = new StringContent(jsonpayload);

            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
