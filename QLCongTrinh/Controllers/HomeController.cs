using QLCongTrinh.Helpers;
using QLCongTrinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCongTrinh.Controllers
{
    public class HomeController : Controller
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
            if (userCheck != null && userCheck.IdQuyen == 2)
            {
                Session["manager"] = userCheck;
                return RedirectToAction("Index", "CongTrinhs");

            }
            else
            {
                ViewBag.errLogin = "Tên đăng nhập hoặc mật khẩu sai";
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
            Session.Remove("manager");
            return RedirectToAction("Index");
        }



        public ActionResult Index()
        {
            if (Session["manager"] == null)
            {
                return RedirectToAction("Login");
            }
            return View("Index", "CongTrinhs");
        }
    }
}