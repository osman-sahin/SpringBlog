using SpringBlog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpringBlog.Areas.Admin.Controllers
{
    public class CommentsController : AdminBaseController
    {
        // GET: Admin/Comments
        public ActionResult Index()
        {
            return View(db.Comments.OrderByDescending(x => x.CreationTime).ToList());
        }

        [HttpPost]
        public ActionResult ChangeState(int id, bool onAir)
        {
            var comment = db.Comments.Find(id);

            comment.State = onAir ? CommentState.stateApproved : CommentState.stateRejected;
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var comment = db.Comments.Find(id);

            if (comment == null)
            {
                TempData["ErrorMessage"] = "Comment is not exist or already deleted!";
                return RedirectToAction("Index");
            }

            db.Comments.RemoveRange(comment.Children);
            db.Comments.Remove(comment);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Comment has been deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}