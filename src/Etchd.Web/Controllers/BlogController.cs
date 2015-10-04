using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Etchd.Web.Framework.Attributes;
using Etchd.Web.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace Etchd.Web.Controllers
{
    public class BlogController : Controller
    {
        private ApplicationDbContext db { get; set; }
        private UserManager<Author> UserManager { get; set; }

        public BlogController(ApplicationDbContext injectedDb)
        {
            db = injectedDb;
        }

        //TODO: Attribute routes...
        [Todo(FutureMilestone.Themeing)]
        public async Task<IActionResult> Index(int page = 1, string tag = "")
        {
            if(page <= 0)
            {
                return new HttpNotFoundResult();
            }
            IQueryable<BlogPost> query = db.BlogPosts;
            if(!string.IsNullOrWhiteSpace(tag))
            {
                query = query.Where(a => a.Tags.Any(b => b == tag));
            }
            query = query.Skip(page - 1 * BlogSettings.NumberOfPosts).Take(BlogSettings.NumberOfPosts);

            var posts = await query.ToListAsync();

            //TODO: Figure out Themeing directory (and add such dir as part of the view resolver), and make 1 theme and test this
            if(posts.Count > 0)
            {
                return View("Index", posts);
            }

            return new HttpNotFoundResult();
        }

        //TODO: Attribute routes...
        [Todo(FutureMilestone.Themeing)]
        public async Task<IActionResult> Blog(string url)
        {
            if(string.IsNullOrWhiteSpace(url))
            {
                return new HttpNotFoundResult();
            }
            var post = db.BlogPosts.FirstOrDefaultAsync(a => a.url == url);

            //TODO: Figure out Themeing directory (and add such dir as part of the view resolver), and make 1 theme and test this
            return View("Blog", post);
        }
    }
}