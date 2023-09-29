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
    builder.EntitySet<EQTrackingItemAnswer>("EQTrackingItemAnswers");
    builder.EntitySet<AnswerType>("AnswerTypes"); 
    builder.EntitySet<EQTrackingItem>("EQTrackingItems"); 
    builder.EntitySet<PersonalGoal>("PersonalGoals"); 
    builder.EntitySet<PersonalTrackingLog>("PersonalTrackingLogs"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EQTrackingItemAnswersController : ODataController
    {
        private EQEntities db = new EQEntities();

        // GET: odata/EQTrackingItemAnswers
        [EnableQuery]
        public IQueryable<EQTrackingItemAnswer> GetEQTrackingItemAnswers()
        {
            return db.EQTrackingItemAnswers;
        }

        // GET: odata/EQTrackingItemAnswers(5)
        [EnableQuery]
        public SingleResult<EQTrackingItemAnswer> GetEQTrackingItemAnswer([FromODataUri] int key)
        {
            return SingleResult.Create(db.EQTrackingItemAnswers.Where(eQTrackingItemAnswer => eQTrackingItemAnswer.Id == key));
        }

        // PUT: odata/EQTrackingItemAnswers(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<EQTrackingItemAnswer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EQTrackingItemAnswer eQTrackingItemAnswer = db.EQTrackingItemAnswers.Find(key);
            if (eQTrackingItemAnswer == null)
            {
                return NotFound();
            }

            patch.Put(eQTrackingItemAnswer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQTrackingItemAnswerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eQTrackingItemAnswer);
        }

        // POST: odata/EQTrackingItemAnswers
        public IHttpActionResult Post(EQTrackingItemAnswer eQTrackingItemAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EQTrackingItemAnswers.Add(eQTrackingItemAnswer);
            db.SaveChanges();

            return Created(eQTrackingItemAnswer);
        }

        // PATCH: odata/EQTrackingItemAnswers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<EQTrackingItemAnswer> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EQTrackingItemAnswer eQTrackingItemAnswer = db.EQTrackingItemAnswers.Find(key);
            if (eQTrackingItemAnswer == null)
            {
                return NotFound();
            }

            patch.Patch(eQTrackingItemAnswer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQTrackingItemAnswerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eQTrackingItemAnswer);
        }

        // DELETE: odata/EQTrackingItemAnswers(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            EQTrackingItemAnswer eQTrackingItemAnswer = db.EQTrackingItemAnswers.Find(key);
            if (eQTrackingItemAnswer == null)
            {
                return NotFound();
            }

            db.EQTrackingItemAnswers.Remove(eQTrackingItemAnswer);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EQTrackingItemAnswers(5)/AnswerType
        [EnableQuery]
        public SingleResult<AnswerType> GetAnswerType([FromODataUri] int key)
        {
            return SingleResult.Create(db.EQTrackingItemAnswers.Where(m => m.Id == key).Select(m => m.AnswerType));
        }

        // GET: odata/EQTrackingItemAnswers(5)/EQTrackingItem
        [EnableQuery]
        public SingleResult<EQTrackingItem> GetEQTrackingItem([FromODataUri] int key)
        {
            return SingleResult.Create(db.EQTrackingItemAnswers.Where(m => m.Id == key).Select(m => m.EQTrackingItem));
        }

        // GET: odata/EQTrackingItemAnswers(5)/PersonalGoals
        [EnableQuery]
        public IQueryable<PersonalGoal> GetPersonalGoals([FromODataUri] int key)
        {
            return db.EQTrackingItemAnswers.Where(m => m.Id == key).SelectMany(m => m.PersonalGoals);
        }

        // GET: odata/EQTrackingItemAnswers(5)/PersonalTrackingLogs
        [EnableQuery]
        public IQueryable<PersonalTrackingLog> GetPersonalTrackingLogs([FromODataUri] int key)
        {
            return db.EQTrackingItemAnswers.Where(m => m.Id == key).SelectMany(m => m.PersonalTrackingLogs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EQTrackingItemAnswerExists(int key)
        {
            return db.EQTrackingItemAnswers.Count(e => e.Id == key) > 0;
        }
    }
}
