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
    builder.EntitySet<EQTrackingItemTemplate>("EQTrackingItemTemplates");
    builder.EntitySet<AspNetUser>("AspNetUsers"); 
    builder.EntitySet<EQArea>("EQAreas"); 
    builder.EntitySet<EQTrackingItemAnswerTemplate>("EQTrackingItemAnswerTemplates"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EQTrackingItemTemplatesController : ODataController
    {
        private EQEntities db = new EQEntities();

        // GET: odata/EQTrackingItemTemplates
        [EnableQuery]
        public IQueryable<EQTrackingItemTemplate> GetEQTrackingItemTemplates()
        {
            return db.EQTrackingItemTemplates;
        }

        // GET: odata/EQTrackingItemTemplates(5)
        [EnableQuery]
        public SingleResult<EQTrackingItemTemplate> GetEQTrackingItemTemplate([FromODataUri] int key)
        {
            return SingleResult.Create(db.EQTrackingItemTemplates.Where(eQTrackingItemTemplate => eQTrackingItemTemplate.Id == key));
        }

        // PUT: odata/EQTrackingItemTemplates(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<EQTrackingItemTemplate> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EQTrackingItemTemplate eQTrackingItemTemplate = db.EQTrackingItemTemplates.Find(key);
            if (eQTrackingItemTemplate == null)
            {
                return NotFound();
            }

            patch.Put(eQTrackingItemTemplate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQTrackingItemTemplateExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eQTrackingItemTemplate);
        }

        // POST: odata/EQTrackingItemTemplates
        public IHttpActionResult Post(EQTrackingItemTemplate eQTrackingItemTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EQTrackingItemTemplates.Add(eQTrackingItemTemplate);
            db.SaveChanges();

            return Created(eQTrackingItemTemplate);
        }

        // PATCH: odata/EQTrackingItemTemplates(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<EQTrackingItemTemplate> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EQTrackingItemTemplate eQTrackingItemTemplate = db.EQTrackingItemTemplates.Find(key);
            if (eQTrackingItemTemplate == null)
            {
                return NotFound();
            }

            patch.Patch(eQTrackingItemTemplate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQTrackingItemTemplateExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eQTrackingItemTemplate);
        }

        // DELETE: odata/EQTrackingItemTemplates(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            EQTrackingItemTemplate eQTrackingItemTemplate = db.EQTrackingItemTemplates.Find(key);
            if (eQTrackingItemTemplate == null)
            {
                return NotFound();
            }

            db.EQTrackingItemTemplates.Remove(eQTrackingItemTemplate);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EQTrackingItemTemplates(5)/AspNetUser
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.EQTrackingItemTemplates.Where(m => m.Id == key).Select(m => m.AspNetUser));
        }

        // GET: odata/EQTrackingItemTemplates(5)/EQArea
        [EnableQuery]
        public SingleResult<EQArea> GetEQArea([FromODataUri] int key)
        {
            return SingleResult.Create(db.EQTrackingItemTemplates.Where(m => m.Id == key).Select(m => m.EQArea));
        }

        // GET: odata/EQTrackingItemTemplates(5)/EQTrackingItemAnswerTemplates
        [EnableQuery]
        public IQueryable<EQTrackingItemAnswerTemplate> GetEQTrackingItemAnswerTemplates([FromODataUri] int key)
        {
            return db.EQTrackingItemTemplates.Where(m => m.Id == key).SelectMany(m => m.EQTrackingItemAnswerTemplates);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EQTrackingItemTemplateExists(int key)
        {
            return db.EQTrackingItemTemplates.Count(e => e.Id == key) > 0;
        }
    }
}
