using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCongTrinh.Models;

namespace QLCongTrinh.Controllers
{
    public class NganSachesController : Controller
    {
        private QLCongTrinhDb db = new QLCongTrinhDb();

        // GET: NganSaches
        public ActionResult Index()
        {
            var nganSaches = db.NganSaches.Include(n => n.CongTrinh);
            return View(nganSaches.ToList());
        }

        // GET: NganSaches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NganSach nganSach = db.NganSaches.Find(id);
            if (nganSach == null)
            {
                return HttpNotFound();
            }
            return View(nganSach);
        }

        // GET: NganSaches/Create
        public ActionResult Create()
        {
            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT");
            return View();
        }

        // POST: NganSaches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCT,NganSachBD")] NganSach nganSach)
        {
            if (ModelState.IsValid)
            {
                db.NganSaches.Add(nganSach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", nganSach.MaCT);
            return View(nganSach);
        }

        // GET: NganSaches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NganSach nganSach = db.NganSaches.Find(id);
            if (nganSach == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", nganSach.MaCT);
            return View(nganSach);
        }

        // POST: NganSaches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCT,NganSachBD")] NganSach nganSach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nganSach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", nganSach.MaCT);
            return View(nganSach);
        }

        // GET: NganSaches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NganSach nganSach = db.NganSaches.Find(id);
            if (nganSach == null)
            {
                return HttpNotFound();
            }
            return View(nganSach);
        }

        // POST: NganSaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NganSach nganSach = db.NganSaches.Find(id);
            CongTrinh congTrinh = db.CongTrinhs.SingleOrDefault(ct=>ct.MaCT ==  id);
            var chiTietCT = db.ChiTietCTs.Where(ct => ct.MaCT == congTrinh.MaCT).ToList();
            var tiendo = db.TienDoes.Find(id);
            var vatlieu = db.VatLieux.Where(vl => vl.MaCT == congTrinh.MaCT).ToList();
          
            if (chiTietCT != null)
            {
                db.ChiTietCTs.RemoveRange(chiTietCT);

            }
            if (tiendo != null)
            {
                db.TienDoes.Remove(tiendo);
            }
            if (vatlieu != null)
            {
                db.VatLieux.RemoveRange(vatlieu);

            }
            db.CongTrinhs.Remove(congTrinh);
            db.NganSaches.Remove(nganSach);
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
