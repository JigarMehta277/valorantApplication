using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
//using valorantApplication.Migrations;
using valorantApplication.Models;
using System.Web.Script.Serialization;

namespace valorantApplication.Controllers
{
    public class valorantUserController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static valorantUserController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44321/api/valorantUsersData/");
        }

        // GET: valorantUser/List
        public ActionResult List()
        {
            //objective: communicate with valorantUser data api to retrieve a list of valorantUsers
            //Curl https://localhost:44321/api/valorantUsersData/ListvalorantUsers

        
            string url = "ListvalorantUsers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<ValorantUserDto> valorantUsers = response.Content.ReadAsAsync<IEnumerable<ValorantUserDto>>().Result;
            Debug.WriteLine("Number of valorantUsers recieved: ");
            Debug.WriteLine(valorantUsers.Count());


            return View(valorantUsers);
        }

        // GET: valorantUser/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with valorantUser data api to retrieve one valorantUser
            //Curl https://localhost:44321/api/valorantUsersData/FindValorantUser/(id)

        
            string url = "FindValorantUser/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is");
            //Debug.WriteLine(response.StatusCode);

            ValorantUserDto selectedvalorantUsers = response.Content.ReadAsAsync<ValorantUserDto>().Result;
            //Debug.WriteLine("valorantUsers recieved: ");
            //Debug.WriteLine(selectedvalorantUsers.UserName);

            return View(selectedvalorantUsers);
        }

        public ActionResult Error()
        {


            return View();
        }

        // GET: valorantUser/New
        public ActionResult New()
        {
            return View();
        }

        // POST: valorantUser/Create
        [HttpPost]
        public ActionResult Create(valorantUser ValorantUser)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(ValorantUser.UserName);
            //objective add a new valorant user into our system using the API
            //Curl -H"Content-Type:application/json" -d @ValorantUser.json https://localhost:44321/api/valorantUsersData/AddValorantUser
            string url = "AddValorantUser";

            
            string jsonpayload = jss.Serialize(ValorantUser);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);

            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
            
        }

        // GET: valorantUser/Edit/5
        public ActionResult Edit(int id)
        {
            //objective: communicate with valorantUser data api to retrieve one valorantUser
            //Curl https://localhost:44321/api/valorantUsersData/FindValorantUser/(id)


            string url = "FindValorantUser/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is");
            //Debug.WriteLine(response.StatusCode);

            ValorantUserDto selectedvalorantUsers = response.Content.ReadAsAsync<ValorantUserDto>().Result;
            //Debug.WriteLine("valorantUsers recieved: ");
            //Debug.WriteLine(selectedvalorantUsers.UserName);

            return View(selectedvalorantUsers);
        }

        // POST: valorantUser/Update/5
        [HttpPost]
        public ActionResult Update(int id, valorantUser ValorantUser)
        {
            string url = "UpdateValorantUser/"+id;
            


            string jsonpayload = jss.Serialize(ValorantUser);

            Debug.WriteLine(jsonpayload);

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

        // GET: valorantUser/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            //objective: communicate with valorantUser data api to retrieve one valorantUser
            //Curl https://localhost:44321/api/valorantUsersData/FindValorantUser/(id)


            string url = "FindValorantUser/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is");
            //Debug.WriteLine(response.StatusCode);

            ValorantUserDto selectedvalorantUsers = response.Content.ReadAsAsync<ValorantUserDto>().Result;
            //Debug.WriteLine("valorantUsers recieved: ");
            //Debug.WriteLine(selectedvalorantUsers.UserName);

            return View(selectedvalorantUsers);
        }

        // POST: valorantUser/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, valorantUser ValorantUser)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(ValorantUser.UserName);
            //objective add a new valorant user into our system using the API
            //Curl -H"Content-Type:application/json" -d @ValorantUser.json https://localhost:44321/api/valorantUsersData/AddValorantUser
            string url = "DeletevalorantUser/" + id;


            string jsonpayload = jss.Serialize(ValorantUser);

            Debug.WriteLine(jsonpayload);

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
