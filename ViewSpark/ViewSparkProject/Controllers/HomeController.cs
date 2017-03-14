using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewSparkProject.Models;
using System.Web.Security;

namespace ViewSparkProject.Controllers
{
    public class HomeController : ViewSparkController
    {
        public ActionResult Index()
        {
            RegisterModel model = new RegisterModel();

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult CompleteRegister()
        {
            return View();
        }

        public ActionResult Register()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult LogOn()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public ActionResult _RegistrationForm()
        {
            RegisterModel model = new RegisterModel();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult _RegistrationForm(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.UserExists = "Username taken, please input another username";
                    User existingUser = db.Users.Single(euser => euser.Username == model.Username);
                    return View(model);
                }
                catch
                {
                    // Put statements for saving data 

                    User u = new User();
                    u.Username = model.Username;
                    u.LastLoginDate = DateTime.Now;
                    u.RegistrationDate = DateTime.Now;
                    u.AuthorizationID = "viewspark_" + model.Username;
                    u.Authorizer = "viewspark";
                    u.Email = model.Email;
                    u.Password = model.Password;
                    u.Gender = model.Gender;
                    u.Birthdate = model.BirthDate.ToShortDateString();
                    u.Role = "user";

                    db.Users.AddObject(u);

                    db.SaveChanges();
                    // For redirection when registration is completed
                    return JavaScript("window.location = '../Home/CompleteRegister'");
                }
            }
            else
            {
                // Show Server Validation Errors
                return PartialView(model);
            }
        }

        [HttpGet]
        public ActionResult _LogOnForm()
        {
            LogOnModel model = new LogOnModel();
            return PartialView(model);            
        }

        [HttpPost]
        public ActionResult _LogOnForm(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                // Put statements for validating log on information here
                try
                {
                    User user = db.Users.Single(u => u.Username == model.Username);
                    if (model.Password == user.Password)
                    {
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket (
                        1,                      // Version
                        model.Username,         // Username
                        DateTime.Now,           // Creation
                        DateTime.Now.AddMinutes(30),   // Expiration
                        false,                         // Persistent
                        user.Role);                     // Roles
                            
                        string encTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                        return JavaScript("window.location = '../Home/'");
                    }
                    else 
                    {
                        ViewBag.PasswordState = "Invalid username or password.";
                        return PartialView();
                    }
                }
                catch
                {
                    ViewBag.PasswordState = "Invalid username or password.";
                    return PartialView();
                }
                
            }
            else
            {
                // Show Server Validation Errors
                ViewBag.PasswordState = "";
                return PartialView(model);
            }
        }

        public ActionResult _Connect()
        {
            return PartialView();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AccountSettings(int id = -1)
        {
            if (id != -1)
            {
                try
                {
                    User user = db.Users.Single(p => p.ID == id);
                    return View(User);
                }
                catch
                {
                    return View();

                }               
            }
            return View();
        }
        //
        // POST: /Presentation/EditDetails/5

        [HttpPost]
        public ActionResult AccountSettings(User user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
