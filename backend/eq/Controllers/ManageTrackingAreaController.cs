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
    [Authorize(Roles ="admin")]
    public class ManageTrackingAreaController : Controller
    {
        private EQEntities db = new EQEntities();

        // GET: ManageTrackingArea
        public ActionResult Index()
        {
            return View();
        }

        // GET: ManageTrackingArea/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQArea eQArea = db.EQAreas.Find(id);
            if (eQArea == null)
            {
                return HttpNotFound();
            }
            return View(eQArea);
        }

        // GET: ManageTrackingArea/Create
        public ActionResult Create()
        {
            EQArea eQArea = new EQArea();

            // #### set default values

			GetSelectLists(eQArea);

            return View(eQArea);
        }

        // POST: ManageTrackingArea/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title")] EQArea eQArea)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.EQAreas.Add(eQArea);
                    db.SaveChanges();
                    return Redirect("/ManageTrackingArea/Edit/" + eQArea.Id + "?reload=&pageid=" + Request["pageid"]);
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
			GetSelectLists(eQArea);
            return View(eQArea);
        }

        // GET: ManageTrackingArea/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQArea eQArea = db.EQAreas.Find(id);
            if (eQArea == null)
            {
                return HttpNotFound();
            }
			GetSelectLists(eQArea);
            return View(eQArea);
        }

        // POST: ManageTrackingArea/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title")] EQArea eQArea)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(eQArea).State = EntityState.Modified;
                    db.SaveChanges();
                    return Redirect("/ManageTrackingArea/Edit/" + eQArea.Id + "?reload=&pageid=" + Request["pageid"]);
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
			GetSelectLists(eQArea);
            return View(eQArea);
        }

        // GET: ManageTrackingArea/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQArea eQArea = db.EQAreas.Find(id);
            if (eQArea == null)
            {
                return HttpNotFound();
            }
            return View(eQArea);
        }

        // POST: ManageTrackingArea/DeleteConfirmed/5
        public ActionResult DeleteConfirmed(int id)
        {
            EQArea eQArea = db.EQAreas.Find(id);
            db.EQAreas.Remove(eQArea);
            //eQArea.IsDeleted = true;
            db.SaveChanges();
            // return RedirectToAction("Index");
            return Redirect("/home/Deleted?javascriptcommand=reloadmanagetrackingareagrid" + Request["pageid"]);
        }

		private void GetSelectLists(EQArea eQArea)
        {
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
