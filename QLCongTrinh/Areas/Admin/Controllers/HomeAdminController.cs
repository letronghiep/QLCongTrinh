using QLCongTrinh.Helpers;
using QLCongTrinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCongTrinh.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        QLCongTrinhDb db = new QLCongTrinhDb();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string TenTaiKhoan, string MatKhau)
        {
            string password = Helper.GetMD5(MatKhau);
            var userCheck = db.TaiKhoans.FirstOrDefault(acc => acc.TenTaiKhoan.Equals(TenTaiKhoan) && acc.MatKhau.Equals(password));
            if (userCheck != null && userCheck.IdQuyen == 1)
            {
                Session["user"] = userCheck;
                return RedirectToAction("Index", "HomeAdmin");

            }
            else
            {
                ViewBag.errLogin = "Tên đăng nhập hoặc mật khẩu sai";
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Index");
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            else
                return View();
        }
        public ActionResult About()
        {
            return View();
        }
    }
}