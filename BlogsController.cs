using DynamoEnterprises.DTOs;
using DynamoEnterprises.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DynamoEnterprises.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogsRepository _blogsRepository;

        public BlogsController(IBlogsRepository blogsRepository)
        {
            _blogsRepository = blogsRepository;
        }

        public IActionResult Index()
        {
            var blogs = _blogsRepository.GetAllBlogs();
            return View(blogs);
        }
        public IActionResult ContentDetails()
        {
            
            return View();
        }


        public IActionResult Details(int id)
        {
            var blog = _blogsRepository.GetBlogById(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }
    }
}
