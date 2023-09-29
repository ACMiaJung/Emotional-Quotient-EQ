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
    builder.EntitySet<EQTrackingItemAnswerTemplate>("EQTrackingItemAnswerTemplates");
    builder.EntitySet<AnswerType>("AnswerTypes"); 
    builder.EntitySet<EQTrackingItemTemplate>("EQTrackingItemTemplates"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EQTrackingItemAnswerTemplatesController : ODataController
    {
        private EQEntities db = new EQEntities();

        // GET: odata/EQTrackingItemAnswerTemplates
        [EnableQuery]
        public IQueryable<EQTrackingItemAnswerTemplate> GetEQTrackingItemAnswerTemplates()
        {
            return db.EQTrackingItemAnswerTemplates;
        }

        // GET: odata/EQTrackingItemAnswerTemplates(5)
        [EnableQuery]
        public SingleResult<EQTrackingItemAnswerTemplate> GetEQTrackingItemAnswerTemplate([FromODataUri] int key)
        {
            return SingleResult.Create(db.EQTrackingItemAnswerTemplates.Where(eQTrackingItemAnswerTemplate => eQTrackingItemAnswerTemplate.Id == key));
        }

        // PUT: odata/EQTrackingItemAnswerTemplates(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<EQTrackingItemAnswerTemplate> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EQTrackingItemAnswerTemplate eQTrackingItemAnswerTemplate = db.EQTrackingItemAnswerTemplates.Find(key);
            if (eQTrackingItemAnswerTemplate == null)
            {
                return NotFound();
            }

            patch.Put(eQTrackingItemAnswerTemplate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQTrackingItemAnswerTemplateExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eQTrackingItemAnswerTemplate);
        }

        // POST: odata/EQTrackingItemAnswerTemplates
        public IHttpActionResult Post(EQTrackingItemAnswerTemplate eQTrackingItemAnswerTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EQTrackingItemAnswerTemplates.Add(eQTrackingItemAnswerTemplate);
            db.SaveChanges();

            return Created(eQTrackingItemAnswerTemplate);
        }

        // PATCH: odata/EQTrackingItemAnswerTemplates(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<EQTrackingItemAnswerTemplate> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EQTrackingItemAnswerTemplate eQTrackingItemAnswerTemplate = db.EQTrackingItemAnswerTemplates.Find(key);
            if (eQTrackingItemAnswerTemplate == null)
            {
                return NotFound();
            }

            patch.Patch(eQTrackingItemAnswerTemplate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQTrackingItemAnswerTemplateExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eQTrackingItemAnswerTemplate);
        }

        // DELETE: odata/EQTrackingItemAnswerTemplates(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            EQTrackingItemAnswerTemplate eQTrackingItemAnswerTemplate = db.EQTrackingItemAnswerTemplates.Find(key);
            if (eQTrackingItemAnswerTemplate == null)
            {
                return NotFound();
            }

            db.EQTrackingItemAnswerTemplates.Remove(eQTrackingItemAnswerTemplate);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EQTrackingItemAnswerTemplates(5)/AnswerType
        [EnableQuery]
        public SingleResult<AnswerType> GetAnswerType([FromODataUri] int key)
        {
            return SingleResult.Create(db.EQTrackingItemAnswerTemplates.Where(m => m.Id == key).Select(m => m.AnswerType));
        }

        // GET: odata/EQTrackingItemAnswerTemplates(5)/EQTrackingItemTemplate
        [EnableQuery]
        public SingleResult<EQTrackingItemTemplate> GetEQTrackingItemTemplate([FromODataUri] int key)
        {
            return SingleResult.Create(db.EQTrackingItemAnswerTemplates.Where(m => m.Id == key).Select(m => m.EQTrackingItemTemplate));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EQTrackingItemAnswerTemplateExists(int key)
        {
            return db.EQTrackingItemAnswerTemplates.Count(e => e.Id == key) > 0;
        }
    }
}
