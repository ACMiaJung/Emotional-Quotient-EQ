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
    builder.EntitySet<EQArea>("EQAreas");
    builder.EntitySet<EQTrackingItem>("EQTrackingItems"); 
    builder.EntitySet<EQTrackingItemTemplate>("EQTrackingItemTemplates"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EQAreasController : ODataController
    {
        private EQEntities db = new EQEntities();

        // GET: odata/EQAreas
        [EnableQuery]
        public IQueryable<EQArea> GetEQAreas()
        {
            return db.EQAreas;
        }

        // GET: odata/EQAreas(5)
        [EnableQuery]
        public SingleResult<EQArea> GetEQArea([FromODataUri] int key)
        {
            return SingleResult.Create(db.EQAreas.Where(eQArea => eQArea.Id == key));
        }

        // PUT: odata/EQAreas(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<EQArea> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EQArea eQArea = db.EQAreas.Find(key);
            if (eQArea == null)
            {
                return NotFound();
            }

            patch.Put(eQArea);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQAreaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eQArea);
        }

        // POST: odata/EQAreas
        public IHttpActionResult Post(EQArea eQArea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EQAreas.Add(eQArea);
            db.SaveChanges();

            return Created(eQArea);
        }

        // PATCH: odata/EQAreas(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<EQArea> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EQArea eQArea = db.EQAreas.Find(key);
            if (eQArea == null)
            {
                return NotFound();
            }

            patch.Patch(eQArea);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQAreaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eQArea);
        }

        // DELETE: odata/EQAreas(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            EQArea eQArea = db.EQAreas.Find(key);
            if (eQArea == null)
            {
                return NotFound();
            }

            db.EQAreas.Remove(eQArea);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EQAreas(5)/EQTrackingItems
        [EnableQuery]
        public IQueryable<EQTrackingItem> GetEQTrackingItems([FromODataUri] int key)
        {
            return db.EQAreas.Where(m => m.Id == key).SelectMany(m => m.EQTrackingItems);
        }

        // GET: odata/EQAreas(5)/EQTrackingItemTemplates
        [EnableQuery]
        public IQueryable<EQTrackingItemTemplate> GetEQTrackingItemTemplates([FromODataUri] int key)
        {
            return db.EQAreas.Where(m => m.Id == key).SelectMany(m => m.EQTrackingItemTemplates);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EQAreaExists(int key)
        {
            return db.EQAreas.Count(e => e.Id == key) > 0;
        }
    }
}
