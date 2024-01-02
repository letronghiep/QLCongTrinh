using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QLCongTrinh.Models;

using OfficeOpenXml;
using System.IO;
using System.IO.Packaging;
using OfficeOpenXml.Style;

namespace QLCongTrinh.Controllers
{
    //[Authorize]
    public class CongTrinhsController : Controller
    {
        private QLCongTrinhDb db = new QLCongTrinhDb();

        // GET: CongTrinhs
        public CongTrinhsController()
        {

        }
        public ActionResult Index(int? page, int? pageSize, string searchString, string sortProperty, string sortOrder = "")
        {
            ViewBag.Keyword = searchString;
            int defaultPageSize = 10;
            if (pageSize.HasValue && pageSize > 0)
            {
                defaultPageSize = pageSize.Value;
            }
            var congTrinhs = db.CongTrinhs.Include(c => c.TaiKhoan);
            if (!String.IsNullOrEmpty(searchString))
                congTrinhs = congTrinhs.Where(ct => ct.TenCT.Contains(searchString)).OrderByDescending(ct => ct.created_at);
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
            if (String.IsNullOrEmpty(sortProperty)) sortProperty = "TenCT";
            if (sortOrder == "desc")
            {
                congTrinhs = congTrinhs.OrderBy(sortProperty + " desc");
            }
            else
            {
                congTrinhs = congTrinhs.OrderBy(sortProperty);
            }


            if (page == null) page = 1;
            int pageNumber = (page ?? 1);

            return View(congTrinhs.ToPagedList(pageNumber, defaultPageSize));
        }

        // GET: CongTrinhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongTrinh congTrinh = db.CongTrinhs.Find(id);
            var nsdachi = congTrinh.VatLieux.Sum(e => e.ThanhTien);
            TienDo tiendo = new TienDo();
            tiendo.MaCT = congTrinh.MaCT;
            if (congTrinh.NganSach.NganSachBD - nsdachi <= 0)
            {
                tiendo.TrangThai = false;
                tiendo.GhiChu = "Chậm do thiếu ngân sách";
                db.Entry(tiendo).State = EntityState.Modified;
                db.SaveChanges()
                    ;
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
                db.SaveChanges();
            }
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
        public ActionResult Create([Bind(Include = "MaCT,TenCT,MaTaiKhoan,NgayBatDau,NgayKetThuc, NganSach, HinhAnh, created_at")] CongTrinh congTrinh)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    congTrinh.HinhAnh = "";
                    var f = Request.Files["fileUpload"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string filename = System.IO.Path.GetFileName(f.FileName);
                        string path = Server.MapPath("~/wwwroot/Image/" + filename);
                        f.SaveAs(path);
                        congTrinh.HinhAnh = filename;


                    }
                    if (congTrinh.NgayBatDau > congTrinh.NgayKetThuc)
                    {
                        ViewBag.err = "Ngày bắt đầu phải trước ngày kết thúc";
                        ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "TenTaiKhoan", congTrinh.MaTaiKhoan);

                        return View(congTrinh);
                    }
                    congTrinh.created_at = DateTime.Now;
                    var tiendo = new TienDo();
                    tiendo.MaCT = congTrinh.MaCT;
                    if (congTrinh.NgayKetThuc < DateTime.Now)
                    {
                        tiendo.TrangThai = true;
                        tiendo.GhiChu = "Đã hoàn thành";
                    }
                    if (congTrinh.NgayKetThuc > DateTime.Now || (congTrinh.NgayBatDau <= DateTime.Now && congTrinh.NgayKetThuc == null))
                    {
                        tiendo.TrangThai = false;
                        tiendo.GhiChu = "Chưa hoàn thành";
                    }
                    if (congTrinh.NgayBatDau > DateTime.Now)
                    {
                        tiendo.TrangThai = false;
                        tiendo.GhiChu = "Chưa thi công";
                    }
                    if (congTrinh.NganSach.NganSachBD <= 0 && (congTrinh.NgayKetThuc < DateTime.Now || congTrinh.NgayKetThuc == null))
                    {
                        tiendo.TrangThai = false;
                        tiendo.GhiChu = "Chậm tiến độ do hết ngân sách";
                    }
                    db.TienDoes.Add(tiendo);
                    db.CongTrinhs.Add(congTrinh);

                    db.SaveChanges();

                }
                return RedirectToAction("Create", "ChiTietCTs", new { id = congTrinh.MaCT });
            }
            catch (Exception e)
            {
                ViewBag.err = "Có lỗi xảy ra khi nhập " + e.Message;
                ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "TenTaiKhoan", congTrinh.MaTaiKhoan);
                return View(congTrinh);
            }
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
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCT,TenCT,MaTaiKhoan,NgayBatDau,NgayKetThuc, NganSach, HinhAnh")] CongTrinh congTrinh)
        {
            var existCongTrinh = db.CongTrinhs.Include(c => c.NganSach).SingleOrDefault(c => c.MaCT == congTrinh.MaCT);
            if (ModelState.IsValid)
            {
                try
                {
                    if (existCongTrinh != null)
                    {
                        existCongTrinh.TenCT = congTrinh.TenCT.ToString();
                        existCongTrinh.MaTaiKhoan = congTrinh.MaTaiKhoan;
                        existCongTrinh.NgayBatDau = congTrinh.NgayBatDau;
                        existCongTrinh.NgayKetThuc = congTrinh.NgayKetThuc;
                        //existCongTrinh.HinhAnh = "";
                        var f = Request.Files["fileUpload"];
                        if (f != null && f.ContentLength > 0)
                        {
                            string filename = System.IO.Path.GetFileName(f.FileName);
                            string path = Server.MapPath("~/wwwroot/Image/" + filename);
                            f.SaveAs(path);
                            existCongTrinh.HinhAnh = filename;


                        }
                        if (existCongTrinh.NganSach == null)
                        {
                            NganSach newNganSach = new NganSach();
                            newNganSach.NganSachBD = existCongTrinh.NganSach.NganSachBD;
                            existCongTrinh.NganSach = newNganSach;
                            db.Entry(newNganSach).State = EntityState.Added; // Đánh dấu đối tượng NganSach là mới
                        }
                        else // Nếu đã có NganSach, chỉ cập nhật giá trị NganSachBD
                        {
                            existCongTrinh.NganSach.NganSachBD = congTrinh.NganSach.NganSachBD;
                        }
                        db.Entry(existCongTrinh).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.err = "Có lỗi xảy ra khi thay đổi trường này " + ex.Message;
                }
            }
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "TenTaiKhoan", existCongTrinh.MaTaiKhoan);
            return View(existCongTrinh);
        }
        //Search
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

        //GET: CongTrinh/TaoBaoCao/5
        public ActionResult TaoBaoCao(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongTrinh congTrinh = db.CongTrinhs.Find(id);
            if (congTrinh == null) { return HttpNotFound(); }
            return View(congTrinh);
        }


        // POST: CongTrinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var nganSach = db.NganSaches.Find(id);
                var chiTietCT = db.ChiTietCTs.Where(ct => ct.MaCT == id).ToList();
                var tiendo = db.TienDoes.Find(id);
                var vatlieu = db.VatLieux.Where(vl => vl.MaCT == id).ToList();
                if (nganSach != null)
                {
                    db.NganSaches.Remove(nganSach);
                }
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
                CongTrinh congTrinh = db.CongTrinhs.Find(id);
                db.CongTrinhs.Remove(congTrinh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                ViewBag.err = "Bạn chưa thể xóa bản ghi này " + e.Message;
                return View();
            }
        }

        //Xuất file excel

        public ActionResult ExportToExcel(int? id)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                var congTrinh = db.CongTrinhs.Find(id);
                var vatlieux = db.VatLieux.Where(ct => ct.MaCT == id).ToList();

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                //Define the header customize
                worksheet.Cells["A1"].Style.Font.Name = "Time New Roman";
                //worksheet.Cells["D1:R1"].Merge = true;
                worksheet.Cells["D1"].Value = "TẠO BÁO CÁO";
                worksheet.Cells["D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["D1"].Style.Font.Size = 18;
                worksheet.Cells["D1"].Style.Font.Bold = true;

                worksheet.Cells["A2"].Value = "Tên công trình: ";
                worksheet.Cells["A2"].Style.Font.Bold = true;
                worksheet.Cells["B2"].Value = congTrinh.TenCT;

                worksheet.Cells["A3"].Value = "Ngày bắt đầu: ";
                worksheet.Cells["A3"].Style.Font.Bold = true;
                worksheet.Cells["B3"].Value = congTrinh.NgayBatDau;
                worksheet.Cells["B3"].Style.Numberformat.Format = "MM/dd/yyyy";


                worksheet.Cells["A4"].Value = "Ngày hoàn thành: ";
                worksheet.Cells["A4"].Style.Font.Bold = true;
                worksheet.Cells["B4"].Value = congTrinh.NgayKetThuc;
                worksheet.Cells["B4"].Style.Numberformat.Format = "MM/dd/yyyy";

                worksheet.Cells["A5"].Value = "Chủ thầu: ";
                worksheet.Cells["A5"].Style.Font.Bold = true;
                worksheet.Cells["B5"].Value = congTrinh.ChiTietCTs.FirstOrDefault(e => e.MaVT == congTrinh.MaChuThau).NhanVien.TenNV;

                worksheet.Cells["A6"].Value = "Nhân viên: ";
                worksheet.Cells["A6"].Style.Font.Bold = true;
                worksheet.Cells["B6"].Value = congTrinh.ChiTietCTs.Count();

                worksheet.Cells["I2"].Value = "Ngân sách ban đầu: ";
                worksheet.Cells["I2"].Style.Font.Bold = true;
                worksheet.Cells["J2"].Value = congTrinh.NganSach.NganSachBD;

                worksheet.Cells["I3"].Value = "Số dư: ";
                worksheet.Cells["I3"].Style.Font.Bold = true;
                worksheet.Cells["J3"].Value = congTrinh.NganSach.NganSachBD - congTrinh.VatLieux.Sum(e => e.ThanhTien);

                worksheet.Cells["I4"].Value = "Tiến độ: ";
                worksheet.Cells["I4"].Style.Font.Bold = true;
                worksheet.Cells["J4"].Value = congTrinh.TienDo.GhiChu;


                worksheet.Cells["A8:B8"].Merge = true;
                worksheet.Cells["A8"].Value = "Tên vật liệu";
                worksheet.Cells["A8"].Style.Font.Bold = true;
                worksheet.Cells["C8"].Value = "Ngày cung cấp";
                worksheet.Cells["C8"].Style.Font.Bold = true;
                worksheet.Cells["D8"].Value = "Số lượng";
                worksheet.Cells["D8"].Style.Font.Bold = true;
                worksheet.Cells["E8"].Value = "Đơn giá";
                worksheet.Cells["E8"].Style.Font.Bold = true;
                worksheet.Cells["F8"].Value = "Thành tiền";
                worksheet.Cells["F8"].Style.Font.Bold = true;

                worksheet.Cells[$"A8:F8"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[$"A8:F8"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
                worksheet.Cells[$"A8:F8"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                int startRow = 9; // Hàng bắt đầu
                int endRow = startRow + vatlieux.Count - 1; // Số hàng cần merge

                int row = startRow;
                for (int i = 0; i < vatlieux.Count; i++)
                {
                    worksheet.Cells[row, 1, row, 2].Merge = true;

                    worksheet.Cells[row, 1].Value = vatlieux[i].Ten;
                    worksheet.Cells[row, 3].Value = vatlieux[i].NgayCungCap;
                    worksheet.Cells[row, 4].Value = vatlieux[i].SoLuong;
                    worksheet.Cells[row, 5].Value = vatlieux[i].DonGia;
                    worksheet.Cells[row, 6].Value = vatlieux[i].ThanhTien;
                    worksheet.Cells[row, 3].Style.Numberformat.Format = "MM/dd/yyyy";

                    row++;
                }
                worksheet.Cells[row, 1, row, 2].Merge = true;

                worksheet.Cells[row, 1].Value = "Tổng tiền";

                worksheet.Cells[row, 6].Value = vatlieux.Sum(e => e.ThanhTien);

                worksheet.Cells[$"A1:J8"].AutoFitColumns();
                worksheet.Cells[$"A1:J6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "baocao.xlsx");



            }
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
