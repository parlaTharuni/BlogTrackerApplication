using AppUILayer.Models;
using BlogDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;

namespace AppUILayer.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IAuthService authService;
        private readonly IBlogReposotiry blogReposotiry;
        private readonly IEmpRepository empRepository;

        // GET: Employee
        public EmployeeController(IEmpRepository empRepository, IAuthService authService, IBlogReposotiry blogReposotiry)
        {
            this.empRepository = empRepository;
            this.authService = authService;
            this.blogReposotiry = blogReposotiry;
        }
        public EmployeeController()
        {
            
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            // Add authentication logic for Employee login
            if (ModelState.IsValid)
            {
                bool Authres = authService.Authenticate(model.EmailId, model.Password, UserRole.Employee);
                if (Authres)
                {
                    var employee = empRepository.GetEmpInfoByEmialId(model.EmailId);
                    Session["EmailId"] = model.EmailId;
                    Session["UserName"] = employee.Name;
                    Session["UserId"] = employee.EmpInfoId;
                    return RedirectToAction("Home", "Employee");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    // Add a flag to indicate unsuccessful login attempt
                    ModelState.AddModelError("LoginFailed", "true");
                }
            }
            return View(model);
        }

        public ActionResult Home()
        {
            string email = (string)Session["EmailId"];
            if (email != null)
            {
                var blogWrtieByLoggedInEmployee = blogReposotiry.GetBlogInfoByEmployeeId(email);
                var blogViewModels = blogWrtieByLoggedInEmployee.Select(blog => new BlogViewModel
                {
                    Id = blog.BlogInfoId,
                    Title = blog.Title,
                    Subject = blog.Subject,
                    DateOfCreation = blog.DateOfCreation,
                    BlogUrl = blog.BlogUrl,
                    EmployeeName = blog.Employee.Name
                }).ToList();
                return View(blogViewModels);
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Blog");
        }
        public ActionResult Edit(int id)
        {
            var editBlog = blogReposotiry.GetBlogInfoById(id);
            if (editBlog != null)
            {
                var blog = new BlogViewModel
                {
                    Id = editBlog.BlogInfoId,
                    Title = editBlog.Title,
                    Subject = editBlog.Subject,
                    DateOfCreation = editBlog.DateOfCreation,
                    BlogUrl = editBlog.BlogUrl
                };
                return View(blog);
            }
            else
            {
                TempData["ErrorMessage"] = "Something went wrong. Blog not found.";
                return RedirectToAction("Home", "Employee");
            }
        }

        [HttpPost]
        public ActionResult Edit(BlogViewModel blogView)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing blog from the database
                var existingBlog = blogReposotiry.GetBlogInfoById(blogView.Id);

                if (existingBlog != null)
                {
                    existingBlog.Title = blogView.Title;
                    existingBlog.Subject = blogView.Subject;
                    existingBlog.DateOfCreation = blogView.DateOfCreation;
                    existingBlog.BlogUrl = blogView.BlogUrl;
                    blogReposotiry.UpdateBlogInfo(existingBlog);
                    return RedirectToAction("Home", "Employee");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong. Blog not updated.";
                    return RedirectToAction("Edit", "Employee");
                }
            }

            // If ModelState is not valid, return the view with validation errors
            return View(blogView);
        }

        public ActionResult Delete(int id)
        {
            var blogToDelete = blogReposotiry.GetBlogInfoById(id);

            if (blogToDelete != null)
            {
                var blog = new BlogViewModel
                {
                    Id = blogToDelete.BlogInfoId,
                    Title = blogToDelete.Title,
                    Subject = blogToDelete.Subject,
                    DateOfCreation = blogToDelete.DateOfCreation,
                    BlogUrl = blogToDelete.BlogUrl,
                    EmployeeName = blogToDelete.Employee.Name
                };
                return View(blog);
            }
            else
            {
                // If the blog is not found, you might want to handle it accordingly
                TempData["ErrorMessage"] = "Blog not found.";
                return RedirectToAction("Home", "Employee");
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(BlogViewModel blogViewModel)
        {
            // Retrieve the blog information by ID
            var blogToDelete = blogReposotiry.GetBlogInfoById(blogViewModel.Id);

            if (blogToDelete != null)
            {

                blogReposotiry.DeleteBlogInfo(blogToDelete.BlogInfoId);
                TempData["SuccessMessage"] = "Blog deleted successfully.";

                return RedirectToAction("Home", "Employee");
            }
            else
            {
                TempData["ErrorMessage"] = "Blog not found.";
                return RedirectToAction("Home", "Employee");
            }
        }
    }
}