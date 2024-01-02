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
    public class TienDoesController : Controller
    {
        private QLCongTrinhDb db = new QLCongTrinhDb();

        // GET: TienDoes
        public ActionResult Index()
        {
            var tienDoes = db.TienDoes.Include(t => t.CongTrinh);
            return View(tienDoes.ToList());
        }

        // GET: TienDoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TienDo tienDo = db.TienDoes.Find(id);
            if (tienDo == null)
            {
                return HttpNotFound();
            }
            return View(tienDo);
        }

        // GET: TienDoes/Create
        public ActionResult Create()
        {
            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT");
            return View();
        }

        // POST: TienDoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCT,TrangThai,GhiChu")] TienDo tienDo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.TienDoes.Add(tienDo);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.err = "Có lỗi xảy ra khi nhập: " + ex.Message;
                }
            }
            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", tienDo.MaCT);
            return View(tienDo);

        }

        // GET: TienDoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TienDo tienDo = db.TienDoes.Find(id);
            if (tienDo == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", tienDo.MaCT);
            return View(tienDo);
        }

        // POST: TienDoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCT,TrangThai,GhiChu")] TienDo tienDo)
        {
            if (ModelState.IsValid)
            {
                if(tienDo.TrangThai == true)
                {
                    tienDo.GhiChu = "Đã hoàn thành";
                }else
                {
                    tienDo.GhiChu = "Chưa hoàn thành";

                }
                db.Entry(tienDo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCT = new SelectList(db.CongTrinhs, "MaCT", "TenCT", tienDo.MaCT);
            return View(tienDo);
        }

        // GET: TienDoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TienDo tienDo = db.TienDoes.Find(id);
            if (tienDo == null)
            {
                return HttpNotFound();
            }
            return View(tienDo);
        }

        // POST: TienDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TienDo tienDo = db.TienDoes.Find(id);
            db.TienDoes.Remove(tienDo);
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
