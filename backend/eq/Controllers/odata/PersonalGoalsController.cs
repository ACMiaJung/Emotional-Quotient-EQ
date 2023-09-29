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
    builder.EntitySet<PersonalGoal>("PersonalGoals");
    builder.EntitySet<AspNetUser>("AspNetUsers"); 
    builder.EntitySet<EQTrackingItemAnswer>("EQTrackingItemAnswers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PersonalGoalsController : ODataController
    {
        private EQEntities db = new EQEntities();

        // GET: odata/PersonalGoals
        [EnableQuery]
        public IQueryable<PersonalGoal> GetPersonalGoals()
        {
            return db.PersonalGoals;
        }

        // GET: odata/PersonalGoals(5)
        [EnableQuery]
        public SingleResult<PersonalGoal> GetPersonalGoal([FromODataUri] int key)
        {
            return SingleResult.Create(db.PersonalGoals.Where(personalGoal => personalGoal.Id == key));
        }

        // PUT: odata/PersonalGoals(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<PersonalGoal> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonalGoal personalGoal = db.PersonalGoals.Find(key);
            if (personalGoal == null)
            {
                return NotFound();
            }

            patch.Put(personalGoal);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalGoalExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(personalGoal);
        }

        // POST: odata/PersonalGoals
        public IHttpActionResult Post(PersonalGoal personalGoal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonalGoals.Add(personalGoal);
            db.SaveChanges();

            return Created(personalGoal);
        }

        // PATCH: odata/PersonalGoals(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<PersonalGoal> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonalGoal personalGoal = db.PersonalGoals.Find(key);
            if (personalGoal == null)
            {
                return NotFound();
            }

            patch.Patch(personalGoal);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalGoalExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(personalGoal);
        }

        // DELETE: odata/PersonalGoals(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            PersonalGoal personalGoal = db.PersonalGoals.Find(key);
            if (personalGoal == null)
            {
                return NotFound();
            }

            db.PersonalGoals.Remove(personalGoal);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/PersonalGoals(5)/AspNetUser
        [EnableQuery]
        public SingleResult<AspNetUser> GetAspNetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.PersonalGoals.Where(m => m.Id == key).Select(m => m.AspNetUser));
        }

        // GET: odata/PersonalGoals(5)/EQTrackingItemAnswer
        [EnableQuery]
        public SingleResult<EQTrackingItemAnswer> GetEQTrackingItemAnswer([FromODataUri] int key)
        {
            return SingleResult.Create(db.PersonalGoals.Where(m => m.Id == key).Select(m => m.EQTrackingItemAnswer));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonalGoalExists(int key)
        {
            return db.PersonalGoals.Count(e => e.Id == key) > 0;
        }
    }
}
