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
    builder.EntitySet<AspNetUser>("AspNetUsers");
    builder.EntitySet<AspNetUserClaim>("AspNetUserClaims"); 
    builder.EntitySet<AspNetUserLogin>("AspNetUserLogins"); 
    builder.EntitySet<EQTrackingItem>("EQTrackingItems"); 
    builder.EntitySet<EQTrackingItemTemplate>("EQTrackingItemTemplates"); 
    builder.EntitySet<PersonalGoal>("PersonalGoals"); 
    builder.EntitySet<PersonalTrackingLog>("PersonalTrackingLogs"); 
    builder.EntitySet<AspNetRole>("AspNetRoles"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AspNetUsersController : ODataController
    {
        private EQEntities db = new EQEntities();

        // GET: odata/AspNetUsers
        [EnableQuery]
        public IQueryable<AspNetUser> GetAspNetUsers()
        {
            return db.AspNetUsers;
        }

        // GET: odata/AspNetUsers(5)
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser([FromODataUri] string key)
        {
            return SingleResult.Create(db.AspNetUsers.Where(aspNetUser => aspNetUser.Id == key));
        }

        // PUT: odata/AspNetUsers(5)
        public IHttpActionResult Put([FromODataUri] string key, Delta<AspNetUser> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AspNetUser aspNetUser = db.AspNetUsers.Find(key);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            patch.Put(aspNetUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(aspNetUser);
        }

        // POST: odata/AspNetUsers
        public IHttpActionResult Post(AspNetUser aspNetUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AspNetUsers.Add(aspNetUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AspNetUserExists(aspNetUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(aspNetUser);
        }

        // PATCH: odata/AspNetUsers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] string key, Delta<AspNetUser> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AspNetUser aspNetUser = db.AspNetUsers.Find(key);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            patch.Patch(aspNetUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(aspNetUser);
        }

        // DELETE: odata/AspNetUsers(5)
        public IHttpActionResult Delete([FromODataUri] string key)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(key);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            db.AspNetUsers.Remove(aspNetUser);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AspNetUsers(5)/AspNetUserClaims
        [EnableQuery]
        public IQueryable<AspNetUserClaim> GetAspNetUserClaims([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.AspNetUserClaims);
        }

        // GET: odata/AspNetUsers(5)/AspNetUserLogins
        [EnableQuery]
        public IQueryable<AspNetUserLogin> GetAspNetUserLogins([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.AspNetUserLogins);
        }

        // GET: odata/AspNetUsers(5)/EQTrackingItems
        [EnableQuery]
        public IQueryable<EQTrackingItem> GetEQTrackingItems([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.EQTrackingItems);
        }

        // GET: odata/AspNetUsers(5)/EQTrackingItemTemplates
        [EnableQuery]
        public IQueryable<EQTrackingItemTemplate> GetEQTrackingItemTemplates([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.EQTrackingItemTemplates);
        }

        // GET: odata/AspNetUsers(5)/PersonalGoals
        [EnableQuery]
        public IQueryable<PersonalGoal> GetPersonalGoals([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.PersonalGoals);
        }

        // GET: odata/AspNetUsers(5)/PersonalTrackingLogs
        [EnableQuery]
        public IQueryable<PersonalTrackingLog> GetPersonalTrackingLogs([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.PersonalTrackingLogs);
        }

        // GET: odata/AspNetUsers(5)/AspNetRoles
        [EnableQuery]
        public IQueryable<AspNetRole> GetAspNetRoles([FromODataUri] string key)
        {
            return db.AspNetUsers.Where(m => m.Id == key).SelectMany(m => m.AspNetRoles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AspNetUserExists(string key)
        {
            return db.AspNetUsers.Count(e => e.Id == key) > 0;
        }
    }
}
