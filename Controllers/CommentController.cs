using Microsoft.AspNetCore.Mvc;
using TheWall.Models;

namespace TheWall {
    public class CommentController : Controller {
        private readonly CommentFactory commentFactory;
        public CommentController () {
            //Instantiate a UserFactory object that is immutable (READONLY)
            //This establishes the initial DB connection for us.
            commentFactory = new CommentFactory ();
        }
        [HttpPost]
        [Route ("AddComment")]
        public IActionResult AddComment (Comment comment) {
                commentFactory.Add (comment);
                return RedirectToAction ("Dashboard", "Dashboard");
            }
            [HttpPost]
            [Route ("DeleteComment")]
        public IActionResult DeleteComment (Comment comment) {
                commentFactory.Delete (comment);
                return RedirectToAction ("Dashboard", "Dashboard");
            }
            [HttpPost]
            [Route ("EditComment")]
        public IActionResult EditComment (Comment comment) {
            commentFactory.Edit (comment);
            return RedirectToAction ("Dashboard", "Dashboard");
        }
    }
}