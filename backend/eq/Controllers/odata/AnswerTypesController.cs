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
    builder.EntitySet<AnswerType>("AnswerTypes");
    builder.EntitySet<EQTrackingItemAnswer>("EQTrackingItemAnswers"); 
    builder.EntitySet<EQTrackingItemAnswerTemplate>("EQTrackingItemAnswerTemplates"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AnswerTypesController : ODataController
    {
        private EQEntities db = new EQEntities();

        // GET: odata/AnswerTypes
        [EnableQuery]
        public IQueryable<AnswerType> GetAnswerTypes()
        {
            return db.AnswerTypes;
        }

        // GET: odata/AnswerTypes(5)
        [EnableQuery]
        public SingleResult<AnswerType> GetAnswerType([FromODataUri] int key)
        {
            return SingleResult.Create(db.AnswerTypes.Where(answerType => answerType.Id == key));
        }

        // PUT: odata/AnswerTypes(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<AnswerType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AnswerType answerType = db.AnswerTypes.Find(key);
            if (answerType == null)
            {
                return NotFound();
            }

            patch.Put(answerType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(answerType);
        }

        // POST: odata/AnswerTypes
        public IHttpActionResult Post(AnswerType answerType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AnswerTypes.Add(answerType);
            db.SaveChanges();

            return Created(answerType);
        }

        // PATCH: odata/AnswerTypes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<AnswerType> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AnswerType answerType = db.AnswerTypes.Find(key);
            if (answerType == null)
            {
                return NotFound();
            }

            patch.Patch(answerType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerTypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(answerType);
        }

        // DELETE: odata/AnswerTypes(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            AnswerType answerType = db.AnswerTypes.Find(key);
            if (answerType == null)
            {
                return NotFound();
            }

            db.AnswerTypes.Remove(answerType);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AnswerTypes(5)/EQTrackingItemAnswers
        [EnableQuery]
        public IQueryable<EQTrackingItemAnswer> GetEQTrackingItemAnswers([FromODataUri] int key)
        {
            return db.AnswerTypes.Where(m => m.Id == key).SelectMany(m => m.EQTrackingItemAnswers);
        }

        // GET: odata/AnswerTypes(5)/EQTrackingItemAnswerTemplates
        [EnableQuery]
        public IQueryable<EQTrackingItemAnswerTemplate> GetEQTrackingItemAnswerTemplates([FromODataUri] int key)
        {
            return db.AnswerTypes.Where(m => m.Id == key).SelectMany(m => m.EQTrackingItemAnswerTemplates);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnswerTypeExists(int key)
        {
            return db.AnswerTypes.Count(e => e.Id == key) > 0;
        }
    }
}
