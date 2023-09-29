using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using eq.model.entitymodel;

namespace eq.Controllers.odata
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using eq.model.entitymodel;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<PersonalTrackingLog>("PersonalTrackingLogs");
    builder.EntitySet<AspNetUser>("AspNetUsers"); 
    builder.EntitySet<EQTrackingItemAnswer>("EQTrackingItemAnswers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PersonalTrackingLogsController : ODataController
    {
        private EQEntities db = new EQEntities();

        // GET: odata/PersonalTrackingLogs
        [EnableQuery]
        public IQueryable<PersonalTrackingLog> GetPersonalTrackingLogs()
        {
            return db.PersonalTrackingLogs;
        }

        // GET: odata/PersonalTrackingLogs(5)
        [EnableQuery]
        public SingleResult<PersonalTrackingLog> GetPersonalTrackingLog([FromODataUri] int key)
        {
            return SingleResult.Create(db.PersonalTrackingLogs.Where(personalTrackingLog => personalTrackingLog.Id == key));
        }

        // PUT: odata/PersonalTrackingLogs(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<PersonalTrackingLog> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonalTrackingLog personalTrackingLog = db.PersonalTrackingLogs.Find(key);
            if (personalTrackingLog == null)
            {
                return NotFound();
            }

            patch.Put(personalTrackingLog);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalTrackingLogExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(personalTrackingLog);
        }

        // POST: odata/PersonalTrackingLogs
        public IHttpActionResult Post(PersonalTrackingLog personalTrackingLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonalTrackingLogs.Add(personalTrackingLog);
            db.SaveChanges();

            return Created(personalTrackingLog);
        }

        // PATCH: odata/PersonalTrackingLogs(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<PersonalTrackingLog> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonalTrackingLog personalTrackingLog = db.PersonalTrackingLogs.Find(key);
            if (personalTrackingLog == null)
            {
                return NotFound();
            }

            patch.Patch(personalTrackingLog);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalTrackingLogExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(personalTrackingLog);
        }

        // DELETE: odata/PersonalTrackingLogs(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            PersonalTrackingLog personalTrackingLog = db.PersonalTrackingLogs.Find(key);
            if (personalTrackingLog == null)
            {
                return NotFound();
            }

            db.PersonalTrackingLogs.Remove(personalTrackingLog);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/PersonalTrackingLogs(5)/AspNetUser
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.PersonalTrackingLogs.Where(m => m.Id == key).Select(m => m.AspNetUser));
        }

        // GET: odata/PersonalTrackingLogs(5)/EQTrackingItemAnswer
        [EnableQuery]
        public SingleResult<EQTrackingItemAnswer> GetEQTrackingItemAnswer([FromODataUri] int key)
        {
            return SingleResult.Create(db.PersonalTrackingLogs.Where(m => m.Id == key).Select(m => m.EQTrackingItemAnswer));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonalTrackingLogExists(int key)
        {
            return db.PersonalTrackingLogs.Count(e => e.Id == key) > 0;
        }
    }
}
