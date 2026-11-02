using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DA_LTW.Models;
using System.Web.Security;

namespace DA_LTW.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        
        QL_NHAHANGEntities db = new QL_NHAHANGEntities();
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult XuLyDangKy(FormCollection form)
        {
            string username = form["username"];
            string password = form["password"];
            string repass = form["repassword"];

            // Kiểm tra trùng mật khẩu
            if (password != repass)
            {
                ViewBag.Error = "Mật khẩu nhập lại không khớp!";
                return View("DangKy");
            }

            // Kiểm tra tài khoản trùng
            var tk = db.TAIKHOANs.FirstOrDefault(t => t.TenDangNhap == username);
            if (tk != null)
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại!";
                return View("DangKy");
            }

            // Lưu tài khoản mới vào DB
            TAIKHOAN newUser = new TAIKHOAN();
            newUser.TenDangNhap = username;
            newUser.MatKhau = password;
            newUser.Quyen = "user"; // mặc định user

            db.TAIKHOANs.Add(newUser);
            db.SaveChanges();

            ViewBag.Success = "Đăng ký thành công! Hãy đăng nhập.";

            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        public ActionResult DangNhap()
        {
            ViewBag.Url = Request.UrlReferrer.ToString() ?? "/";
            return View();
        }

        [HttpPost]
        public ActionResult XuLyDangNhap(FormCollection form, string url)
        {
            string tenDN = form["username"];
            string matKhau = form["password"];

            // tìm user trong DB
            var kq = db.TAIKHOANs
                       .FirstOrDefault(t => t.TenDangNhap == tenDN && t.MatKhau == matKhau);

            if (kq != null)
            {
                // lưu cookie đăng nhập
                FormsAuthentication.SetAuthCookie(kq.TenDangNhap, false);

                // lưu session
                Session["username"] = kq.TenDangNhap;
                Session["password"] = kq.MatKhau;
                Session["UserRole"] = kq.Quyen;
                return Redirect(url);
            }
            else
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View("DangNhap");
            }
        }
        public ActionResult logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","TrangChu");
        }
        
    }
}