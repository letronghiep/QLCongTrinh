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
    public class VatLieuxController : Controller
    {
        private QLCongTrinhDb db = new QLCongTrinhDb();

        // GET: VatLieux
        public ActionResult Index()
        {
            var vatLieux = db.VatLieux.Include(v => v.CongTrinh);
            return View(vatLieux.ToList());
        }

        // GET: VatLieux/Details/5
        public ActionResult Details(int? MaVL, int? MaCT)
        {
            if (MaVL == null || MaCT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VatLieu vatLieu = db.VatLieux.SingleOrDefault(e => e.MaVL == MaVL && e.MaCT == MaCT);
            if (vatLieu == null)
            {
                return HttpNotFound();
            }
            return View(vatLieu);
        }

        // GET: VatLieux/Create
        public ActionResult Create()
        {
            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT");
            return View();
        }

        // POST: VatLieux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaVL,MaCT, Ten, NgayCungCap,SoLuong,DonGia,hinhanh")] VatLieu vatLieu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var congTrinh = db.CongTrinhs.SingleOrDefault(ct => ct.MaCT == vatLieu.MaCT);
                    var nsdachi = congTrinh.VatLieux.Sum(e => e.ThanhTien);
                    var existsVL = db.VatLieux.SingleOrDefault(vl => vl.MaCT == vatLieu.MaCT && vl.Ten == vatLieu.Ten);

                    TienDo tiendo = new TienDo();
                    tiendo.MaCT = vatLieu.MaCT;
                    if (congTrinh != null)
                    {
                        if (congTrinh.NganSach.NganSachBD - nsdachi < 0)
                        {
                            ViewBag.err = "Ngân sách không đủ";
                            tiendo.TrangThai = false;
                            tiendo.GhiChu = "Chậm do thiếu ngân sách";
                            db.Entry(tiendo).State = EntityState.Modified;

                            db.SaveChanges();
                            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", vatLieu.MaCT);
                            return View(vatLieu);
                        }
                        else
                        {
                            if (congTrinh.NgayKetThuc == null || congTrinh.NgayKetThuc > DateTime.Now)
                            {
                                tiendo.TrangThai = false;
                                tiendo.GhiChu = "Chưa hoàn thành";
                                db.Entry(tiendo).State = EntityState.Modified;

                            }
                            else if (congTrinh.NgayKetThuc <= DateTime.Now)
                            {
                                tiendo.TrangThai = true;
                                tiendo.GhiChu = "Đã hoàn thành";
                                db.Entry(tiendo).State = EntityState.Modified;

                            }
                            if (existsVL != null)
                            {
                                existsVL.SoLuong += vatLieu.SoLuong;
                                db.Entry(existsVL).State = EntityState.Modified;
                            }else
                            {
                            db.VatLieux.Add(vatLieu);

                            }

                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        ViewBag.err = "Không có công trình";
                        ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", vatLieu.MaCT);

                        return View(vatLieu);


                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    ViewBag.err = "Có  lỗi xảy ra khi nhập " + ex.Message;
                }
            }

            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", vatLieu.MaCT);
            return View(vatLieu);
        }

        // GET: VatLieux/Edit/5
        public ActionResult Edit(int? MaVL, int? MaCT)
        {
            if (MaVL == null || MaCT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VatLieu vatLieu = db.VatLieux.SingleOrDefault(e => e.MaVL == MaVL && e.MaCT == MaCT);

            if (vatLieu == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", vatLieu.MaCT);
            return View(vatLieu);
        }

        // POST: VatLieux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaVL,MaCT, Ten, NgayCungCap,SoLuong,DonGia,hinhanh")] VatLieu vatLieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vatLieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", vatLieu.MaCT);
            return View(vatLieu);
        }

        // GET: VatLieux/Delete/5
        public ActionResult Delete(int? MaVL, int? MaCT)
        {
            if (MaVL == null || MaCT == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VatLieu vatLieu = db.VatLieux.SingleOrDefault(e => e.MaVL == MaVL && e.MaCT == MaCT);
            if (vatLieu == null)
            {
                return HttpNotFound();
            }
            return View(vatLieu);
        }

        // POST: VatLieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int MaVL, int MaCT)
        {
            VatLieu vatLieu = db.VatLieux.SingleOrDefault(e => e.MaVL == MaVL && e.MaCT == MaCT);

            db.VatLieux.Remove(vatLieu);
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
