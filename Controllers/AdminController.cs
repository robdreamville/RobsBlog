using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LetsChatFinal.Models;

namespace LetsChatFinal.Controllers
{
    [Route("a9z7b3x-manage-posts")]
    public class AdminController : Controller
    {
        private readonly PostContext _context;

        public AdminController(PostContext context)
        {
            _context = context;
        }

        // GET: Admin
        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts
                .OrderByDescending(p => p.Created)
                .ToListAsync();
            return View(posts);
        }

        // GET: Admin/Create
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content")] Post post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    post.Created = DateTime.Now;
                    post.UserName = "Rob";
                    
                    _context.Add(post);
                    await _context.SaveChangesAsync();
                    
                    // Redirect to the admin index page
                    return Redirect("/a9z7b3x-manage-posts");
                }
                
                // If we get here, there were validation errors
                return View(post);
            }
            catch (Exception ex)
            {
                // Log the error (in a real app, you'd use a proper logging framework)
                ModelState.AddModelError("", $"Error creating post: {ex.Message}");
                return View(post);
            }
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
            return Redirect("/a9z7b3x-manage-posts");
        }
    }
} 