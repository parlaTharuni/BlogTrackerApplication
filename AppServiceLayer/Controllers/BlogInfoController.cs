using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AppServiceLayer.Models;
using BlogDAL;
//using AppServiceLayer.Models;

namespace AppServiceLayer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BlogInfoesController : ApiController
    {
        private BlogDbContext db = new BlogDbContext();

        // GET: api/BlogInfoes
        public IQueryable<BlogInfoMolel> GetBlogInfos()
        {
            var blogInfoModelList = db.BlogInfos;
            var convertToblogInfoModelList = blogInfoModelList.Select(blog => new BlogInfoMolel
            {
                BlogInfoId = blog.BlogInfoId,
                BlogUrl = blog.BlogUrl,
                Title = blog.Title,
                Subject = blog.Subject,
                DateOfCreation = blog.DateOfCreation,
                Employee = blog.Employee,
            });
            return convertToblogInfoModelList;
        }

        // GET: api/BlogInfoes/5
        [ResponseType(typeof(BlogInfo))]
        public IHttpActionResult GetBlogInfo(int id)
        {
            BlogInfo blog = db.BlogInfos.Find(id);
            var convertToblogInfoMode = new BlogInfoMolel
            {
                BlogInfoId = blog.BlogInfoId,
                BlogUrl = blog.BlogUrl,
                Title = blog.Title,
                Subject = blog.Subject,
                DateOfCreation = blog.DateOfCreation,
                Employee = blog.Employee,
            };
            if (convertToblogInfoMode == null)
            {
                return NotFound();
            }

            return Ok(convertToblogInfoMode);
        }

        // PUT: api/BlogInfoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBlogInfo(int id, BlogInfoMolel blogInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blogInfo.BlogInfoId)
            {
                return BadRequest();
            }
            var convertToblogInfoMode = new BlogInfo
            {
                BlogInfoId = blogInfo.BlogInfoId,
                BlogUrl = blogInfo.BlogUrl,
                Title = blogInfo.Title,
                Subject = blogInfo.Subject,
                DateOfCreation = blogInfo.DateOfCreation,
                Employee = blogInfo.Employee,
            };
            db.Entry(convertToblogInfoMode).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BlogInfoes
        [ResponseType(typeof(BlogInfo))]
        public IHttpActionResult PostBlogInfo(BlogInfoMolel blogInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var convertToblogInfoMode = new BlogInfo
            {
                BlogInfoId = blogInfo.BlogInfoId,
                BlogUrl = blogInfo.BlogUrl,
                Title = blogInfo.Title,
                Subject = blogInfo.Subject,
                DateOfCreation = blogInfo.DateOfCreation,
                Employee = blogInfo.Employee,
            };
            db.BlogInfos.Add(convertToblogInfoMode);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = blogInfo.BlogInfoId }, blogInfo);
        }

        // DELETE: api/BlogInfoes/5
        [ResponseType(typeof(BlogInfo))]
        public IHttpActionResult DeleteBlogInfo(int id)
        {
            BlogInfo blog = db.BlogInfos.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            var convertToblogInfoMode = new BlogInfoMolel
            {
                BlogInfoId = blog.BlogInfoId,
                BlogUrl = blog.BlogUrl,
                Title = blog.Title,
                Subject = blog.Subject,
                DateOfCreation = blog.DateOfCreation,
                Employee = blog.Employee,
            };
            db.BlogInfos.Remove(blog);
            db.SaveChanges();

            return Ok(convertToblogInfoMode);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlogInfoExists(int id)
        {
            return db.BlogInfos.Count(e => e.BlogInfoId == id) > 0;
        }
    }
}
