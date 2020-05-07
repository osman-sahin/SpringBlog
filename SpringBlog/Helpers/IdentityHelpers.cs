using Microsoft.AspNet.Identity;
using SpringBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace SpringBlog.Helpers
{
    public static class IdentityHelpers  //helper yazıyosak extension method yazıyoruzdur. Static olmalıdır.
    {
        public static string DisplayName(this IIdentity identity)
        {
            var claims = ((ClaimsIdentity)identity).Claims;

            string displayName = claims.FirstOrDefault(x => x.Type == "DisplayName").Value;

            return displayName;
        }

        public static string UserImage(this IIdentity identity)
        {
            using (var db = new ApplicationDbContext())
            {
                string userId = identity.GetUserId();
                var user = db.Users.Find(userId);
                return user.UserImage;
            }

        }
    }
}