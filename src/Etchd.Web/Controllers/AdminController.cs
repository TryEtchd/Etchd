using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Etchd.Web.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace Etchd.Web.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
		private ApplicationDbContext db { get; set; }
		private UserManager<Author> UserManager { get; set; }

		public AdminController(ApplicationDbContext injectedDb, UserManager<Author> userManager)
		{
			db = injectedDb;
			UserManager = userManager;
		}

		public IActionResult Index()
		{
			//TODO: Consider wrapping our DB access in a repository pattern.
			// We should do this for two reasons, 1. we could move from EF to dapper later if we wish
			// 2. We can and should cache the results of the db queries so that we can improve perf
			var posts = db.BlogPosts.ToList();
			return View(posts);
		}

		public async Task<IActionResult> EditPost(int id = 0)
		{
			var post = await db.BlogPosts.FirstOrDefaultAsync(a => a.id == id);
			if(post == null)
			{
				post = new BlogPost { Author = db.Users.First(a => a.Id == HttpContext.User.GetUserId()) };
			}
			return View(post);
		}

		[HttpPost]
		public async Task<IActionResult> EditPost(BlogPost post)
		{
			post.Author = await db.Users.FirstAsync(a => a.Id == HttpContext.User.GetUserId());
			if(post.id != 0)
			{
				var existingPost = db.BlogPosts.FirstOrDefault(a => a.id == post.id);
				if(existingPost != null)
				{
					existingPost.SetPost(post);
					await db.SaveChangesAsync();
				}
			}
			else
			{
				//todo: confirm model is valid, for now leave it alone (debugging and what not)
				db.BlogPosts.Add(post);
				await db.SaveChangesAsync();
			}

			return RedirectToAction("EditPost", new { id = post.id });
		}
	}
}