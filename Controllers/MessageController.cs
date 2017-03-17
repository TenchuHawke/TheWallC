using Microsoft.AspNetCore.Mvc;
namespace TheWall {
    using System.Collections.Generic;
    using TheWall.Models;
    public class MessageController : Controller {
        private readonly MessageFactory messageFactory;
        private readonly CommentFactory commentFactory;
        public MessageController () {
                //Instantiate a UserFactory object that is immutable (READONLY)
                //This establishes the initial DB connection for us.
                messageFactory = new MessageFactory ();
                commentFactory = new CommentFactory ();
            }
            [HttpPost]
            [Route ("AddMessage")]
        public IActionResult AddMessage (Message message) {              
                messageFactory.Add (message);
                return RedirectToAction ("Dashboard", "Dashboard");
            }
            [HttpPost]
            [Route ("DeleteMessage")]
        public IActionResult DeleteMessage (int id) {
                Message message = messageFactory.FindByMessageId (id);
                List<Comment> clearme = commentFactory.FindAllByMessageID (message);
                foreach (Comment comment in clearme) {
                    commentFactory.Delete (comment);
                }
                messageFactory.Delete (message);
                return RedirectToAction ("Dashboard", "Dashboard");
            }
            [HttpPost]
            [Route ("EditMesage")]
        public IActionResult EditMessage (Message message) {
            messageFactory.Edit (message);
            return RedirectToAction ("Dashboard", "Dashboard");
        }
    }
}