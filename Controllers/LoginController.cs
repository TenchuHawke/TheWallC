using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheWall.Factory;
using TheWall.Models;
namespace TheWall {
    public class LoginController : Controller {
        private readonly UserFactory userFactory;
        public LoginController () {
                //Instantiate a UserFactory object that is immutable (READONLY)
                //This establishes the initial DB connection for us.
                userFactory = new UserFactory ();
            }
            [HttpGet]
            [Route ("Login")]
        public IActionResult Login () {
                List<string> Errors = new List<string> ();
                try {
                    List<string> Results = HttpContext.Session.GetObjectFromJson<List<string>> ("Errors");
                    foreach (object error in Results) {
                        Errors.Add (error.ToString ());
                    }
                } catch {

                }
                ViewBag.Errors = Errors;
                return View ("loginreg");
            }
            [HttpPost]
            [Route ("Register")]
        public IActionResult Register (UserValidation user) {
                List<string> Errors = new List<string> ();
                if (ModelState.IsValid == true) {
                    user.Email = user.Email.ToLower ();
                    User Results = userFactory.FindByEmail (user.Email);
                    if (Results == null) {
                        userFactory.Add (user as User);
                        Results = userFactory.FindByEmail (user.Email);
                        HttpContext.Session.SetInt32 ("UserID", (int)Results.UId);
                        return RedirectToAction ("Dashboard", "Dashboard", false);
                    } else {
                        System.Console.WriteLine ("User exists");
                        Errors.Add ("User already exists, try a different E-Mail");
                    }
                } else {
                    Dictionary<string, string> Error = new Dictionary<string, string> ();
                    foreach (string key in ViewData.ModelState.Keys) {
                        foreach (ModelError error in ViewData.ModelState[key].Errors) {
                            Errors.Add (error.ErrorMessage);
                        }
                    }
                }
                HttpContext.Session.SetObjectAsJson ("Errors", Errors);
                return RedirectToAction ("Login");
            }
            [HttpPost]
            [Route ("Login")]
        public IActionResult Login (UserValidation user) {
            List<string> Errors = new List<string> ();
            if (user.Email == null) {
                Errors.Add ("E-Mail can not be blank.");
            }
            if (user.Password == null) {
                Errors.Add ("Password can not be blank.");
            }
            if (Errors.Count == 0) {
                user.Email = user.Email.ToLower ();
                User Results = userFactory.FindByEmail (user.Email);
                if (Results != null) {
                    if (Results.Password == user.Password) {
                        HttpContext.Session.SetInt32 ("UserID", (int)Results.UId);
                        return RedirectToAction ("Dashboard", "Dashboard", false);
                    }
                }
                Errors.Add ("Invalid Email / Password Combination.");
            }
            HttpContext.Session.SetObjectAsJson ("Errors", Errors);
            return RedirectToAction ("Login");
        }
    }
}