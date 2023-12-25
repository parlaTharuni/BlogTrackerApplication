using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using BlogDAL;
using AppServiceLayer;
using System.Web.Http;
using AppServiceLayer.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Net;
using System.Web.Http.Description;

namespace AppServiceLayer.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AdminInfoesController : ApiController
    {
        private BlogDbContext db = new BlogDbContext();

        // GET: api/AdminInfoes
        public IQueryable<AdminInfoModel> GetAdminInfos()
        {
            var listofAdmin = db.AdminInfos;
            var convertToModelList = listofAdmin.Select(admin => new AdminInfoModel
            {
                AdminInfoId = admin.AdminInfoId,
                EmailId = admin.EmailId,
                Password = admin.Password
            });
            return convertToModelList;
        }

        // GET: api/AdminInfoes/5
        [ResponseType(typeof(AdminInfo))]
        public IHttpActionResult GetAdminInfo(int id)
        {
            AdminInfo adminInfo = db.AdminInfos.Find(id);
            var convertoAdminInfo = new AdminInfoModel
            {
                AdminInfoId = adminInfo.AdminInfoId,
                EmailId = adminInfo.EmailId,
                Password = adminInfo.Password
            };
            if (convertoAdminInfo == null)
            {
                return NotFound();
            }

            return Ok(convertoAdminInfo);
        }

        // PUT: api/AdminInfoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdminInfo(int id, AdminInfoModel adminInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adminInfo.AdminInfoId)
            {
                throw new ArgumentException("Invalid ID");

            }
            var convertoAdminInfo = new AdminInfo
            {

                AdminInfoId = adminInfo.AdminInfoId,
                EmailId = adminInfo.EmailId,
                Password = adminInfo.Password
            };
            db.Entry(convertoAdminInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminInfoExists(id))
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

        // POST: api/AdminInfoes
        [ResponseType(typeof(AdminInfo))]
        public IHttpActionResult PostAdminInfo(AdminInfoModel adminInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var convertoAdminInfo = new AdminInfo
            {

                AdminInfoId = adminInfo.AdminInfoId,
                EmailId = adminInfo.EmailId,
                Password = adminInfo.Password
            };
            db.AdminInfos.Add(convertoAdminInfo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = adminInfo.AdminInfoId }, adminInfo);
        }

        // DELETE: api/AdminInfoes/5
        [ResponseType(typeof(AdminInfo))]
        public IHttpActionResult DeleteAdminInfo(int id)
        {
            // Find the existing AdminInfo by ID
            AdminInfo adminInfo = db.AdminInfos.Find(id);
            var convertoAdminInfo = new AdminInfo
            {

                AdminInfoId = adminInfo.AdminInfoId,
                EmailId = adminInfo.EmailId,
                Password = adminInfo.Password
            };
            if (adminInfo == null)
            {
                return NotFound();
            }
            // Remove the existing AdminInfo directly (no need to create a new instance)
            db.AdminInfos.Remove(adminInfo);

            // Save changes to the database
            db.SaveChanges();

            // Return OK with the removed AdminInfo
            return Ok(convertoAdminInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminInfoExists(int id)
        {
            return db.AdminInfos.Count(e => e.AdminInfoId == id) > 0;
        }
    }
}