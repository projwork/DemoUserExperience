using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DemoUser.Mvc.Models;
using Newtonsoft.Json;

namespace DemoUser.Mvc.Controllers
{
    public class UsersController : Controller
    {
        // GET: UsersController
        public ActionResult Index()
        {
            var client = new HttpClient();
            var users = new List<UserModel>();

            var response = client.GetAsync("https://localhost:5001/api/Users").Result;

            if (response.IsSuccessStatusCode)
            {
                users = JsonConvert.DeserializeObject<List<UserModel>>(response.Content.ReadAsStringAsync().Result);
            }
            return View(users);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            var client = new HttpClient();
            var userDetail = new UserDetailModel();

            var response = client.GetAsync($"https://localhost:5001/api/Users/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                userDetail = JsonConvert.DeserializeObject<UserDetailModel>(response.Content.ReadAsStringAsync().Result);
            }

            return View(userDetail);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            var user = new CreateUserModel();
            user.IntialCount = 0;
            return View(user);
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUserModel request)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //HTTP POST
                    var postTask = client.PostAsJsonAsync("https://localhost:5001/api/Users", request);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View(request);
                
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
