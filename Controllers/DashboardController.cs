using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheWall.Factory;

namespace TheWall {

    public class DashboardController : Controller {
        private readonly UserFactory userFactory;
        private readonly CommentFactory commentFactory;
        private readonly MessageFactory messageFactory;
        public DashboardController () {
                //Instantiate a UserFactory object that is immutable (READONLY)
                //This establishes the initial DB connection for us.
                userFactory = new UserFactory ();
                messageFactory = new MessageFactory ();
                commentFactory = new CommentFactory ();
            }
            [HttpGetAttribute]
            [RouteAttribute ("Dashboard")]
        public IActionResult Dashboard (){
            // TODO:  Get Session Working!
            // int UserId = (int)HttpContext.Session.GetInt32("UserId");
            ViewBag.User = userFactory.FindByID(12);
            ViewBag.Messages = messageFactory.AllMessagesComplete();
            ViewBag.Comments = commentFactory.AllCommentsComplete();
            return View ();
        }
    }
}