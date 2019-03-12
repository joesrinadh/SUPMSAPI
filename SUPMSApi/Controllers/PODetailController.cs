using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SUPMSApi.Models;

namespace SUPMSApi.Controllers
{
    public class PODetailController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/PODetail
        public IQueryable<PODetail> GetPODetails()
        {
            return db.PODetails;
        }

        // GET: api/PODetail/5
        [ResponseType(typeof(PODetail))]
        public IHttpActionResult GetPODetail(int id)
        {
            PODetail pODetail = db.PODetails.Find(id);
            if (pODetail == null)
            {
                return NotFound();
            }

            return Ok(pODetail);
        }

        // PUT: api/PODetail/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPODetail(int id, PODetail pODetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pODetail.PODetailsId)
            {
                return BadRequest();
            }

            db.Entry(pODetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PODetailExists(id))
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

        // POST: api/PODetail
        [ResponseType(typeof(PODetail))]
        public IHttpActionResult PostPODetail(PODetail pODetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PODetails.Add(pODetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pODetail.PODetailsId }, pODetail);
        }

        // DELETE: api/PODetail/5
        [ResponseType(typeof(PODetail))]
        public IHttpActionResult DeletePODetail(int id)
        {
            PODetail pODetail = db.PODetails.Find(id);
            if (pODetail == null)
            {
                return NotFound();
            }

            db.PODetails.Remove(pODetail);
            db.SaveChanges();

            return Ok(pODetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PODetailExists(int id)
        {
            return db.PODetails.Count(e => e.PODetailsId == id) > 0;
        }
    }
}