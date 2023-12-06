using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCongTrinh.Helpers;
using QLCongTrinh.Models;

namespace QLCongTrinh.Areas.Admin.Controllers
{
    public class TaiKhoanAdminsController : Controller
    {
        private QLCongTrinhDb db = new QLCongTrinhDb();

        // GET: Admin/TaiKhoanAdmins
        public ActionResult Index()
        {
            var taiKhoans = db.TaiKhoans.Include(t => t.Quyen);
            return View(taiKhoans.ToList());
        }

        // GET: Admin/TaiKhoanAdmins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoanAdmins/Create
        public ActionResult Create()
        {
            ViewBag.IdQuyen = new SelectList(db.Quyens, "IdQuyen", "TenQuyen");
            return View();
        }

        // POST: Admin/TaiKhoanAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTaiKhoan,TenTaiKhoan,MatKhau,TenNguoiDung,IdQuyen,ImageUrl,mota")] TaiKhoan taiKhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    taiKhoan.ImageUrl = "";
                    var f = Request.Files["ImageUpload"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(f.FileName);
                        string filePath = Server.MapPath("~/wwwroot/Images/Accounts/" + fileName);
                        f.SaveAs(filePath);
                        taiKhoan.ImageUrl = fileName;
                    }
                    taiKhoan.MatKhau = Helper.GetMD5(taiKhoan.MatKhau);
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.IdQuyen = new SelectList(db.Quyens, "IdQuyen", "TenQuyen", taiKhoan.IdQuyen);
                ViewBag.err = "Có lỗi xảy ra khi nhập";
                return View(taiKhoan);
            }
        }

        // GET: Admin/TaiKhoanAdmins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdQuyen = new SelectList(db.Quyens, "IdQuyen", "TenQuyen", taiKhoan.IdQuyen);
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoanAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTaiKhoan,TenTaiKhoan,MatKhau,TenNguoiDung,IdQuyen,ImageUrl,mota")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdQuyen = new SelectList(db.Quyens, "IdQuyen", "TenQuyen", taiKhoan.IdQuyen);
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoanAdmins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoanAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(taiKhoan);
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
