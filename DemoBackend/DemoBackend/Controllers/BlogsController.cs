using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly ILogger<BlogsController> _logger;

        public BlogsController(ILogger<BlogsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Blog> Get()
        {
            using var db = new BloggingContext();
            return db.Blogs.ToList();
        }

        [HttpPost]
        public Blog Post()
        {
            using var db = new BloggingContext();
            var blog = new Blog { Url = "http://sample.com" };
            db.Blogs.Add(blog);
            db.SaveChanges();
            return blog;
        }
    }
}
