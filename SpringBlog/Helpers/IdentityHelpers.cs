using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

        public static void SeedRolesAndUsers()
        {
            var context = new ApplicationDbContext();
            //https://stackoverflow.com/questions/19280527/mvc-5-seed-users-and-roles

            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "osmansahin.eng@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "osmansahin.eng@gmail.com",
                    Email = "osmansahin.eng@gmail.com",
                    DisplayName = "Osman Ş.",
                    EmailConfirmed = true
                };

                manager.Create(user, "Password1!");
                manager.AddToRole(user.Id, "admin");

                #region Seed Categories and Posts
                if (!context.Categories.Any())
                {
                    context.Categories.Add(new Category
                    {
                        CategoryName = "Sample Category 1",
                        Slug = "sample-category-1",
                        Posts = new List<Post>
                        {
                            new Post
                            {
                                Title = "Sample Post1",
                                AuthorId = user.Id,
                                Content = "<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis aliquid atque, nulla? Quos cum ex quis soluta, a laboriosam. Dicta expedita corporis animi vero voluptate voluptatibus possimus, veniam magni quis!</p>",
                                Slug = "sample-post-1",
                                CreationTime = DateTime.Now,
                                ModificationTime = DateTime.Now
                            },
                            new Post
                            {
                                Title = "Sample Post2",
                                AuthorId = user.Id,
                                Content = "<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Reiciendis aliquid atque, nulla? Quos cum ex quis soluta, a laboriosam. Dicta expedita corporis animi vero voluptate voluptatibus possimus, veniam magni quis!</p>",
                                Slug = "sample-post-2",
                                CreationTime = DateTime.Now,
                                ModificationTime = DateTime.Now
                            }
                        }

                    });
                }
                #endregion
            }
            context.SaveChanges();
            context.Dispose();
        }
    }
}