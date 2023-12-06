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
    public class CongTrinhsController : Controller
    {
        private QLCongTrinhDb db = new QLCongTrinhDb();

        // GET: CongTrinhs
        public ActionResult Index()
        {
            var congTrinhs = db.CongTrinhs.Include(c => c.TaiKhoan);
            return View(congTrinhs.ToList());
        }

        // GET: CongTrinhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongTrinh congTrinh = db.CongTrinhs.Find(id);
            if (congTrinh == null)
            {
                return HttpNotFound();
            }
            return View(congTrinh);
        }

        // GET: CongTrinhs/Create
        public ActionResult Create()
        {
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "TenTaiKhoan");
            return View();
        }

        // POST: CongTrinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCT,TenCT,MaTaiKhoan,NgayBatDau,NgayKetThuc,ChuThau,ThoChinh,ThoPhu,SoDu")] CongTrinh congTrinh)
        {
            if (ModelState.IsValid)
            {
                db.CongTrinhs.Add(congTrinh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "TenTaiKhoan", congTrinh.MaTaiKhoan);
            return View(congTrinh);
        }

        // GET: CongTrinhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongTrinh congTrinh = db.CongTrinhs.Find(id);
            if (congTrinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "TenTaiKhoan", congTrinh.MaTaiKhoan);
            return View(congTrinh);
        }

        // POST: CongTrinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCT,TenCT,MaTaiKhoan,NgayBatDau,NgayKetThuc,ChuThau,ThoChinh,ThoPhu,SoDu")] CongTrinh congTrinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(congTrinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "TenTaiKhoan", congTrinh.MaTaiKhoan);
            return View(congTrinh);
        }

        // GET: CongTrinhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongTrinh congTrinh = db.CongTrinhs.Find(id);
            if (congTrinh == null)
            {
                return HttpNotFound();
            }
            return View(congTrinh);
        }

        // POST: CongTrinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CongTrinh congTrinh = db.CongTrinhs.Find(id);
            db.CongTrinhs.Remove(congTrinh);
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
