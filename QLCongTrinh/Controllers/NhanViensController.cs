using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;

using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QLCongTrinh.Models;

namespace QLCongTrinh.Controllers
{
    public class NhanViensController : Controller
    {
        private QLCongTrinhDb db = new QLCongTrinhDb();

        // GET: NhanViens
        //public ActionResult Index()
        //{
        //    var nhanViens = db.NhanViens.Include(n => n.TaiKhoan);
        //    return View(nhanViens.ToList());
        //}
        public ActionResult Index(int? page, int? pageSize, string searchString, string sortProperty, string sortOrder = "")
        {
            ViewBag.Keyword = searchString;
            int defaultPageSize = 10;
            if (pageSize.HasValue && pageSize > 0)
            {
                defaultPageSize = pageSize.Value;
            }
            var nhanViens = db.NhanViens.Include(c => c.TaiKhoan);
            if (!String.IsNullOrEmpty(searchString))
                nhanViens = nhanViens.Where(ct => ct.TenNV.Contains(searchString));
            switch (sortOrder)
            {
                case "asc":
                    ViewBag.SortOrder = "desc";
                    break;
                case "desc":
                    ViewBag.SortOrder = "";
                    break;
                case "":
                    ViewBag.SortOrder = "asc";
                    break;
            }
            if (String.IsNullOrEmpty(sortProperty)) sortProperty = "TenNV";
            if (sortOrder == "desc")
            {
                nhanViens = nhanViens.OrderBy(sortProperty + " desc");
            }
            else
            {
                nhanViens = nhanViens.OrderBy(sortProperty);
            }


            if (page == null) page = 1;
            int pageNumber = (page ?? 1);

            return View(nhanViens.ToPagedList(pageNumber, defaultPageSize));
        }
        // GET: NhanViens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public ActionResult Create()
        {
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "TenTaiKhoan");
            return View();
        }

        // POST: NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,TenNV,Luong,hinhanh,MaTaiKhoan,Mota, Sdt, Xa, Huyen, Tinh")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    nhanVien.hinhanh = "";
                    var f = Request.Files["fileUpload"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string filename = System.IO.Path.GetFileName(f.FileName);
                        string path = Server.MapPath("~/wwwroot/Image/NhanVien/" + filename);
                        f.SaveAs(path);
                        nhanVien.hinhanh = filename;
                    }
                    db.NhanViens.Add(nhanVien);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {

                    ViewBag.err = "Có lỗi xảy ra khi nhập " + ex.Message;

                }



            }

            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "TenTaiKhoan", nhanVien.MaTaiKhoan);
            return View(nhanVien);
        }

        // GET: NhanViens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "TenTaiKhoan", nhanVien.MaTaiKhoan);
            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,TenNV,Luong,hinhanh,MaTaiKhoan,Mota, Sdt, Xa, Huyen, Tinh")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var f = Request.Files["fileUpload"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string filename = System.IO.Path.GetFileName(f.FileName);
                        string path = Server.MapPath("~/wwwroot/Image/NhanVien/" + filename);
                        f.SaveAs(path);
                        nhanVien.hinhanh = filename;
                    }

                    db.Entry(nhanVien).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    ViewBag.err = "Có lỗi xảy ra khi nhập " + ex.Message;


                }
            }
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "TenTaiKhoan", nhanVien.MaTaiKhoan);
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
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
