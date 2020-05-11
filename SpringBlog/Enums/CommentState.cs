using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpringBlog.Enums
{
    public enum CommentState
    {
        [Display(Name = "Pending")]
        statePending = 0,
        [Display(Name = "Approved")]
        stateApproved = 1,
        [Display(Name = "Rejected")]
        stateRejected = 2
    }
}