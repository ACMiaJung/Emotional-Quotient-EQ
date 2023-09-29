using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eq.model.entitymodel;
using System.Data.Entity.Validation;
using james.utils.database;


namespace eq.Controllers
{
    public class ManageTrackingItemController : Controller
    {
        private EQEntities db = new EQEntities();

        // GET: ManageTrackingItem
        public ActionResult Index()
        {
            return View();
        }

        // GET: ManageTrackingItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQTrackingItemTemplate eQTrackingItemTemplate = db.EQTrackingItemTemplates.Find(id);
            if (eQTrackingItemTemplate == null)
            {
                return HttpNotFound();
            }
            return View(eQTrackingItemTemplate);
        }

        // GET: ManageTrackingItem/Create
        public ActionResult Create()
        {
            EQTrackingItemTemplate eQTrackingItemTemplate = new EQTrackingItemTemplate();

            // #### set default values

			GetSelectLists(eQTrackingItemTemplate);

            return View(eQTrackingItemTemplate);
        }

        // POST: ManageTrackingItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AreaId,Title,Question,OwnerId")] EQTrackingItemTemplate eQTrackingItemTemplate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.EQTrackingItemTemplates.Add(eQTrackingItemTemplate);
                    db.SaveChanges();
                    return Redirect("/ManageTrackingItem/Edit/" + eQTrackingItemTemplate.Id + "?reload=&pageid=" + Request["pageid"]);
                }
                catch (DbEntityValidationException _exp)
                {
                    string _msg = DBUtils.GetDBValidationExceptionMessage(_exp);
                    ViewBag.ErrorMsg = _msg;
                }
                catch (Exception _exp)
                {
                    ViewBag.ErrorMsg = DBUtils.GetNormalExceptionMessage(_exp);
                }
            }
			GetSelectLists(eQTrackingItemTemplate);
            return View(eQTrackingItemTemplate);
        }

        // GET: ManageTrackingItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQTrackingItemTemplate eQTrackingItemTemplate = db.EQTrackingItemTemplates.Find(id);
            if (eQTrackingItemTemplate == null)
            {
                return HttpNotFound();
            }
			GetSelectLists(eQTrackingItemTemplate);
            return View(eQTrackingItemTemplate);
        }

        // POST: ManageTrackingItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AreaId,Title,Question,OwnerId")] EQTrackingItemTemplate eQTrackingItemTemplate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(eQTrackingItemTemplate).State = EntityState.Modified;
                    db.SaveChanges();
                    return Redirect("/ManageTrackingItem/Edit/" + eQTrackingItemTemplate.Id + "?reload=&pageid=" + Request["pageid"]);
                }
                catch (DbEntityValidationException _exp)
                {
                    string _msg = DBUtils.GetDBValidationExceptionMessage(_exp);
                    ViewBag.ErrorMsg = _msg;
                }
                catch (Exception _exp)
                {
                    ViewBag.ErrorMsg = DBUtils.GetNormalExceptionMessage(_exp);
                }
            }
			GetSelectLists(eQTrackingItemTemplate);
            return View(eQTrackingItemTemplate);
        }

        // GET: ManageTrackingItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQTrackingItemTemplate eQTrackingItemTemplate = db.EQTrackingItemTemplates.Find(id);
            if (eQTrackingItemTemplate == null)
            {
                return HttpNotFound();
            }
            return View(eQTrackingItemTemplate);
        }

        // POST: ManageTrackingItem/DeleteConfirmed/5
        public ActionResult DeleteConfirmed(int id)
        {
            EQTrackingItemTemplate eQTrackingItemTemplate = db.EQTrackingItemTemplates.Find(id);
            db.EQTrackingItemTemplates.Remove(eQTrackingItemTemplate);
            //eQTrackingItemTemplate.IsDeleted = true;
            db.SaveChanges();
            // return RedirectToAction("Index");
            return Redirect("/home/Deleted?javascriptcommand=reloadmanagetrackingitemgrid" + Request["pageid"]);
        }

		private void GetSelectLists(EQTrackingItemTemplate eQTrackingItemTemplate)
        {
            // ViewBag.OwnerId = new SelectList(db.AspNetUsers, "Id", "Hometown", eQTrackingItemTemplate.OwnerId);
            // ViewBag.AreaId = new SelectList(db.EQAreas, "Id", "Title", eQTrackingItemTemplate.AreaId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
