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

namespace AppServiceLayer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmpInfoesController : ApiController
    {
        private BlogDbContext db = new BlogDbContext();

        // GET: api/EmpInfoes
        public IQueryable<EmpInfoModel> GetEmpInfos()
        {
            var employeList = db.EmpInfos;
            var convertToModelList = employeList.Select(emp => new EmpInfoModel
            {
                EmailId = emp.EmailId,
                Name = emp.Name,
                PassCode = emp.PassCode,
                DateOfJoining = emp.DateOfJoining,
                EmpInfoId = emp.EmpInfoId,

            });
            return convertToModelList;
        }

        // GET: api/EmpInfoes/5
        [ResponseType(typeof(EmpInfo))]
        public IHttpActionResult GetEmpInfo(int id)
        {
            EmpInfo emp = db.EmpInfos.Find(id);
            var convertToEmpInfoModel = new EmpInfoModel
            {
                EmailId = emp.EmailId,
                Name = emp.Name,
                PassCode = emp.PassCode,
                DateOfJoining = emp.DateOfJoining,
                EmpInfoId = emp.EmpInfoId,

            };
            if (convertToEmpInfoModel == null)
            {
                return NotFound();
            }

            return Ok(convertToEmpInfoModel);
        }

        // PUT: api/EmpInfoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmpInfo(int id, EmpInfoModel empInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empInfo.EmpInfoId)
            {
                return BadRequest();
            }
            var convertToEmpInfoModel = new EmpInfo
            {
                EmailId = empInfo.EmailId,
                Name = empInfo.Name,
                PassCode = empInfo.PassCode,
                DateOfJoining = empInfo.DateOfJoining,
                EmpInfoId = empInfo.EmpInfoId,

            };
            db.Entry(convertToEmpInfoModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpInfoExists(id))
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

        // POST: api/EmpInfoes
        [ResponseType(typeof(EmpInfo))]
        public IHttpActionResult PostEmpInfo(EmpInfoModel empInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var convertToEmpInfoModel = new EmpInfo
            {
                EmailId = empInfo.EmailId,
                Name = empInfo.Name,
                PassCode = empInfo.PassCode,
                DateOfJoining = empInfo.DateOfJoining,
                EmpInfoId = empInfo.EmpInfoId,

            };
            db.EmpInfos.Add(convertToEmpInfoModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = empInfo.EmpInfoId }, empInfo);
        }

        // DELETE: api/EmpInfoes/5
        [ResponseType(typeof(EmpInfo))]
        public IHttpActionResult DeleteEmpInfo(int id)
        {
            EmpInfo empInfo = db.EmpInfos.Find(id);
            if (empInfo == null)
            {
                return NotFound();
            }
            var convertToEmpInfoModel = new EmpInfoModel
            {
                EmailId = empInfo.EmailId,
                Name = empInfo.Name,
                PassCode = empInfo.PassCode,
                DateOfJoining = empInfo.DateOfJoining,
                EmpInfoId = empInfo.EmpInfoId,

            };

            db.EmpInfos.Remove(empInfo);
            db.SaveChanges();

            return Ok(convertToEmpInfoModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpInfoExists(int id)
        {
            return db.EmpInfos.Count(e => e.EmpInfoId == id) > 0;
        }
    }
}
