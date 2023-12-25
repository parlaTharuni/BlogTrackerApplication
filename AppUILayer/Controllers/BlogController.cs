using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppUILayer.Models;
using BlogDAL;

namespace AppUILayer.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogReposotiry _blogRepository;
        private readonly IEmpRepository empRepository;

        public BlogController(IBlogReposotiry blogReposotiry, IEmpRepository empRepository)
        {
            this._blogRepository = blogReposotiry;
            this.empRepository = empRepository;
        }
        public BlogController()
        {
            
        }
        public ActionResult Index()
        {

            var blogs = _blogRepository.GetAllBlogInfos();
            var blogViewModels = blogs.Select(blog => new BlogViewModel
            {

                Title = blog.Title,
                Subject = blog.Subject,
                DateOfCreation = blog.DateOfCreation,
                BlogUrl = blog.BlogUrl,
                EmployeeName = blog.Employee.Name
            }).ToList();

            return View(blogViewModels);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddBlogModel blogInfo)
        {
            if (ModelState.IsValid)
            {
                string loggedInEmployee = (string)Session["EmailId"];

                BlogInfo blog = new BlogInfo
                {
                    Title = blogInfo.Title,
                    Subject = blogInfo.Subject,
                    DateOfCreation = DateTime.Now,
                    BlogUrl = blogInfo.BlogUrl,
                };

                _blogRepository.AddBlogWithForeignKey(loggedInEmployee, blog);

                return RedirectToAction("Home", "Employee"); // Redirect to the blog list or another action
            }

            return View(blogInfo);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
