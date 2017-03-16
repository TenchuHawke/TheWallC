using System.Collections.Generic;
using System.Linq;
using DbConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TheWall {
    public class LoginController : Controller {
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
        public IActionResult Register (User user) {
            List<string> Errors = new List<string> ();
            if (ModelState.IsValid == true) {
                string Query = $"SELECT * FROM wall.users where email = '" + user.Email.ToLower () + "'";
                Dictionary<string, object> Results = DbConnector.Query (Query).SingleOrDefault ();
                if (Results == null) {
                    Query = $"INSERT INTO `wall`.`users` (`first_name`, `last_name`, `email`, `password`) VALUES ('{user.FirstName}', '{user.LastName}', '" + user.Email.ToLower () + "', '{user.Password}');";
                    DbConnector.Execute (Query);
                    Query = $"SELECT * FROM `wall`.`users` WHERE `email` = '{user.Email}'";
                    Dictionary<string, object> results = DbConnector.Query (Query).SingleOrDefault ();
                    HttpContext.Session.SetInt32 ("UserID", (int) Results["ID"]);
                    return RedirectToRoute ("Success");
                } else {
                    System.Console.WriteLine ("User exists");
                    Errors.Add ("User already exists, try a different E-Mail");
                }
            } else {
                Dictionary<string, string> Error = new Dictionary<string, string> ();
                System.Console.WriteLine (ViewData.ModelState["Email"].Errors[0].ErrorMessage);
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
        public IActionResult Login (User user) {
            List<string> Errors = new List<string> ();
            if (user.Email == null) {
                Errors.Add ("E-Mail can not be blank.");
            }
            if (user.Password == null) {
                Errors.Add ("Password can not be blank.");
            }
            if (Errors.Count == 0) {
                string Query = $"SELECT * FROM wall.users where email = '" + user.Email.ToLower () + "'";
                Dictionary<string, object> Results = DbConnector.Query (Query).SingleOrDefault ();
                if (Results != null) {
                    if (Results["Password"].ToString () == user.Password) {
                        HttpContext.Session.SetInt32 ("UserID", (int) Results["ID"]);
                        return RedirectToRoute ("Success");
                    }
                }
                Errors.Add ("Invalid Email / Password Combination.");
            }
            HttpContext.Session.SetObjectAsJson ("Errors", Errors);
            return RedirectToAction ("Login");
        }
    }
}