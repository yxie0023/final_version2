using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using final_version2.Models;

namespace final_version2.Controllers
{
    public class RestsController : Controller
    {
        private final_version2Entities1 db = new final_version2Entities1();

        // GET: Rests
        public ActionResult Index()
        {
            return View(db.Rests.ToList());
        }

        // GET: Rests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rest rest = db.Rests.Find(id);
            if (rest == null)
            {
                return HttpNotFound();
            }
            return View(rest);
        }

        // GET: Rests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RestaurantId,Name,Description,Longitude,Latitude")] Rest rest, HttpPostedFileBase postedFile)
        {
            ModelState.Clear();
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            rest.Description = myUniqueFileName;
            TryValidateModel(rest);

            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~/Uploads/");
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string filePath = rest.Description + fileExtension;
                rest.Description = filePath;
                postedFile.SaveAs(serverPath + rest.Description);

                db.Rests.Add(rest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rest);
        }


        // GET: Rests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rest rest = db.Rests.Find(id);
            if (rest == null)
            {
                return HttpNotFound();
            }
            return View(rest);
        }

        // POST: Rests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RestaurantId,Name,Description,Longitude,Latitude")] Rest rest, HttpPostedFileBase postedFile)
        {
            ModelState.Clear();
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            rest.Description = myUniqueFileName;
            TryValidateModel(rest);

            if (ModelState.IsValid) 
            {
                string serverPath = Server.MapPath("~/Uploads/");
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string filePath = rest.Description + fileExtension;
                rest.Description = filePath;
                postedFile.SaveAs(serverPath + rest.Description);

                db.Entry(rest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rest);
        }

        // GET: Rests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rest rest = db.Rests.Find(id);
            if (rest == null)
            {
                return HttpNotFound();
            }
            return View(rest);
        }

        // POST: Rests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rest rest = db.Rests.Find(id);
            db.Rests.Remove(rest);
            db.SaveChanges();
            return RedirectToAction("Index");
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
