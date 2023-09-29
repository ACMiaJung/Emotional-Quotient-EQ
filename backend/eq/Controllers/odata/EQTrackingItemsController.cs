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
    builder.EntitySet<EQTrackingItem>("EQTrackingItems");
    builder.EntitySet<AspNetUser>("AspNetUsers"); 
    builder.EntitySet<EQArea>("EQAreas"); 
    builder.EntitySet<EQTrackingItemAnswer>("EQTrackingItemAnswers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class EQTrackingItemsController : ODataController
    {
        private EQEntities db = new EQEntities();

        // GET: odata/EQTrackingItems
        [EnableQuery]
        public IQueryable<EQTrackingItem> GetEQTrackingItems()
        {
            return db.EQTrackingItems;
        }

        // GET: odata/EQTrackingItems(5)
        [EnableQuery]
        public SingleResult<EQTrackingItem> GetEQTrackingItem([FromODataUri] int key)
        {
            return SingleResult.Create(db.EQTrackingItems.Where(eQTrackingItem => eQTrackingItem.Id == key));
        }

        // PUT: odata/EQTrackingItems(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<EQTrackingItem> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EQTrackingItem eQTrackingItem = db.EQTrackingItems.Find(key);
            if (eQTrackingItem == null)
            {
                return NotFound();
            }

            patch.Put(eQTrackingItem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQTrackingItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eQTrackingItem);
        }

        // POST: odata/EQTrackingItems
        public IHttpActionResult Post(EQTrackingItem eQTrackingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EQTrackingItems.Add(eQTrackingItem);
            db.SaveChanges();

            return Created(eQTrackingItem);
        }

        // PATCH: odata/EQTrackingItems(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<EQTrackingItem> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EQTrackingItem eQTrackingItem = db.EQTrackingItems.Find(key);
            if (eQTrackingItem == null)
            {
                return NotFound();
            }

            patch.Patch(eQTrackingItem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EQTrackingItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eQTrackingItem);
        }

        // DELETE: odata/EQTrackingItems(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            EQTrackingItem eQTrackingItem = db.EQTrackingItems.Find(key);
            if (eQTrackingItem == null)
            {
                return NotFound();
            }

            db.EQTrackingItems.Remove(eQTrackingItem);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/EQTrackingItems(5)/AspNetUser
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.EQTrackingItems.Where(m => m.Id == key).Select(m => m.AspNetUser));
        }

        // GET: odata/EQTrackingItems(5)/EQArea
        [EnableQuery]
        public SingleResult<EQArea> GetEQArea([FromODataUri] int key)
        {
            return SingleResult.Create(db.EQTrackingItems.Where(m => m.Id == key).Select(m => m.EQArea));
        }

        // GET: odata/EQTrackingItems(5)/EQTrackingItemAnswers
        [EnableQuery]
        public IQueryable<EQTrackingItemAnswer> GetEQTrackingItemAnswers([FromODataUri] int key)
        {
            return db.EQTrackingItems.Where(m => m.Id == key).SelectMany(m => m.EQTrackingItemAnswers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EQTrackingItemExists(int key)
        {
            return db.EQTrackingItems.Count(e => e.Id == key) > 0;
        }
    }
}
