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
    public class ChiTietCTsController : Controller
    {
        private QLCongTrinhDb db = new QLCongTrinhDb();

        // GET: ChiTietCTs
        public ActionResult Index()
        {
            var chiTietCTs = db.ChiTietCTs.Include(c => c.CongTrinh).Include(c => c.NhanVien).Include(c => c.ViTri);
            return View(chiTietCTs.ToList());
        }

        // GET: ChiTietCTs/Details/5
        public ActionResult Details(int? employee_id, int? project_id)
        {
            if (employee_id == null || project_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietCT chiTietCT = db.ChiTietCTs.SingleOrDefault(e => e.MaNV == employee_id && e.MaCT == project_id);
            if (chiTietCT == null)
            {
                return HttpNotFound();
            }
            return View(chiTietCT);
        }

        // GET: ChiTietCTs/Create
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                var congtrinh = db.CongTrinhs.Find(id);
                if (congtrinh != null)
                    ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", congtrinh.MaCT);
                else
                    ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT");


            }
            else
            {
                ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT");
            }
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV");
            ViewBag.MaVT = new SelectList(db.ViTris, "MaVT", "TenVT");
            return View();
        }

        // POST: ChiTietCTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(ChiTietCT chiTietCT)
        {
            //if (ModelState.IsValid)
            //{
            try
            {
                bool checkChuThau = db.ChiTietCTs.Any(ct => ct.MaCT == chiTietCT.MaCT && chiTietCT.MaVT == 1);
                bool checkNhanVien = db.ChiTietCTs.Any(ct => ct.MaCT == chiTietCT.MaCT && ct.MaNV == chiTietCT.MaNV);
                if (checkChuThau)
                {
                   
                    return Json(new { result = false, error = "Đã có chủ thầu trong công trình này" });
                }
                else if (checkNhanVien)
                {
                    return Json(new { result = false, error = "Nhân viên đã được thêm vào công trình này" });

                }
                else
                {
                    db.ChiTietCTs.Add(chiTietCT);
                    var congtrinh = db.CongTrinhs.SingleOrDefault(ct => ct.MaCT == chiTietCT.MaCT && chiTietCT.MaVT == 1);
                    if (congtrinh != null)
                    {
                            congtrinh.MaChuThau = chiTietCT.MaNV;
                    }
                    db.SaveChanges();
                    return Json(new { result = true, JsonRequestBehavior.AllowGet, success = "Thêm thành công" });

                }
            }
            catch (Exception ex)
            {
                return Json(new { result = false, error = "Có lỗi xảy ra khi nhập " + ex.Message });
            }
        }
        // GET: ChiTietCTs/Edit/5
        public ActionResult Edit(int? employee_id, int? project_id)
        {
            if (employee_id == null || project_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietCT chiTietCT = db.ChiTietCTs.SingleOrDefault(e => e.MaNV == employee_id && e.MaCT == project_id);

            if (chiTietCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", chiTietCT.MaCT);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", chiTietCT.MaNV);
            ViewBag.MaVT = new SelectList(db.ViTris, "MaVT", "TenVT", chiTietCT.MaVT);
            return View(chiTietCT);
        }

        // POST: ChiTietCTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCT,MaNV,MaVT")] ChiTietCT chiTietCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", chiTietCT.MaCT);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "TenNV", chiTietCT.MaNV);
            ViewBag.MaVT = new SelectList(db.ViTris, "MaVT", "TenVT", chiTietCT.MaVT);
            return View(chiTietCT);
        }

        // GET: ChiTietCTs/Delete/5
        public ActionResult Delete(int? employee_id, int? project_id)
        {
            if (employee_id == null || project_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietCT chiTietCT = db.ChiTietCTs.SingleOrDefault(e => e.MaNV == employee_id && e.MaCT == project_id);
            if (chiTietCT == null)
            {
                return HttpNotFound();
            }
            return View(chiTietCT);
        }

        // POST: ChiTietCTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int employee_id, int project_id)
        {
            ChiTietCT chiTietCT = db.ChiTietCTs.FirstOrDefault(c => c.MaNV == employee_id && c.MaCT == project_id);
            db.ChiTietCTs.Remove(chiTietCT);
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
