using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Project2.Models;

namespace Project2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SignInManager<MyUser> signInManager;

        private readonly MyUserService myUserService;

        public HomeController(ILogger<HomeController> logger, SignInManager<MyUser> signInManager, MyUserService myUserService)
        {
            _logger = logger;
            this.signInManager = signInManager;
            this.myUserService = myUserService;
        } 

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> UserDetail()
        {
            if (this.User.Identity.Name.Contains("admin"))
            {
                ViewBag.User = this.User.Identity.Name;
                ViewBag.ListUsers = await this.myUserService.List();
                List<ContactForm> contacts;
                if (System.IO.File.Exists("contacts.json"))
                {
                    string json = await System.IO.File.ReadAllTextAsync("contacts.json");

                    contacts = JsonConvert.DeserializeObject<List<ContactForm>>(json);
                    if (contacts == null)
                    {
                        contacts = new List<ContactForm>();
                    }
                }
                else
                {
                    contacts = new List<ContactForm>();
                }
                ViewBag.ListContacts = contacts;
            }
            else
            {
                ViewBag.User = this.User.Identity.Name;
                MyUser us = await this.myUserService.getUserByEmail(this.User.Identity.Name);

                ViewBag.ListUsers = new List<MyUser>() { us};
            }
            return View();
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserDetail");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Login(MyUser user)
        {
            if (ModelState.IsValid)
            {
                user.Id = await this.myUserService.getIdUser(user.Email.ToLower());
                if(user.Id == 0)
                {
                    return View();
                }
                //var result = await this.signInManager.PasswordSignInAsync(user.Email.ToLower(), user.Password, true, false);
                MyUser my = await this.myUserService.getUserById(user.Id);
                if(my.Password == user.Password)
                {
                    await this.signInManager.SignInAsync(user, true);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Password", "Password is not correct!");
                    return RedirectToAction("ErrorPassword");
                }
            }
            return View();
            
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult ErrorPassword()
        {

            return View();
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Portfolio()
        {
            

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactForm form)
        {
            if (ModelState.IsValid)
            {
                List<ContactForm> contacts;
                if (System.IO.File.Exists("contacts.json"))
                {
                    string json = await System.IO.File.ReadAllTextAsync("contacts.json");

                    contacts = JsonConvert.DeserializeObject<List<ContactForm>>(json);
                    if(contacts == null)
                    {
                        contacts = new List<ContactForm>();
                    }
                }
                else
                {
                    contacts = new List<ContactForm>();
                }
                contacts.Add(form);
                await System.IO.File.AppendAllTextAsync("contacts.json", JsonConvert.SerializeObject(contacts));
                return RedirectToAction("Success");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            MyUser us = await this.myUserService.getUserById(id);
            ViewBag.MyUser = us;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MyUser user)
        {
            List<MyUser> users = await this.myUserService.List();
            foreach(MyUser item in users)
            {
                if(item.Id == user.Id)
                {
                    await this.myUserService.EditUser(user);
                    return RedirectToAction("UserDetail");
                }
            }
            return View();
        }
        public IActionResult Item()
        {
            return View();
        }
        public IActionResult Success()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
