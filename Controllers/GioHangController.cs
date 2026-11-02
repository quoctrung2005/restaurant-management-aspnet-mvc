using DA_LTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA_LTW.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                // Nếu giỏ hàng chưa tồn tại thì khởi tạo list mới
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        // Sửa int id thành string id
        [HttpPost]
        public ActionResult ThemVaoGioAjax(string id)
        {
            // 1. Lấy giỏ hàng từ Session
            List<GioHang> lstGioHang = LayGioHang();

            // 2. Kiểm tra sản phẩm
            // So sánh string với string
            GioHang sanPham = lstGioHang.Find(n => n.iMaMon == id);

            if (sanPham == null)
            {
                sanPham = new GioHang(id); // Class GioHang cũng phải nhận string
                lstGioHang.Add(sanPham);
            }
            else
            {
                sanPham.iSoLuong++;
            }
            // 3. Tính tổng số lượng

            int tongSoLuong = lstGioHang.Sum(n => n.iSoLuong);

            // 4. Trả về JSON chứa số lượng mới để Script cập nhật

            return Json(new { success = true, count = tongSoLuong });
        }

        // Hàm này được gọi khi người dùng nhấn nút "Thanh Toán" hoặc "Đặt Hàng"
        public ActionResult DatHang()
        {
            // 1. Kiểm tra giỏ hàng có hàng không
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }

            List<GioHang> gh = LayGioHang();
            if (gh.Count == 0) // Kiểm tra thêm trường hợp list rỗng
            {
                return RedirectToAction("Index", "TrangChu");
            }

            // 2. Kiểm tra đăng nhập
            if (Session["UserRole"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            // 3. Xử lý lưu vào Database
            using (var db = new QL_NHAHANGEntities())
            {
                /* --- A. TẠO DỮ LIỆU ĐẶT BÀN --- */
                DATBAN ddh = new DATBAN();
                // Tạo mã tự động: DB + ngày tháng năm giờ phút giây (ví dụ: DB1122103059)
                ddh.MaDatBan = "DB" + DateTime.Now.ToString("MMddHHmmss");

                // Lấy MaKH từ tài khoản đang đăng nhập (Giả sử Session["Username"] lưu TenDangNhap)
                // Cần truy vấn ngược lại bảng TAIKHOAN để lấy MaKH
                string tenDangNhap = Session["Username"].ToString();
                var taiKhoan = db.TAIKHOANs.FirstOrDefault(t => t.TenDangNhap == tenDangNhap);

                if (taiKhoan != null && taiKhoan.MaKH != null)
                {
                    ddh.MaKH = taiKhoan.MaKH; // Gán mã khách hàng nếu là khách quen
                }
                else
                {
                    ddh.MaKH = null; // Khách vãng lai (nếu DB cho phép null)
                                     // Hoặc gán mã khách mặc định: ddh.MaKH = "KHVL"; 
                }

                ddh.NgayDat = DateTime.Now;
                ddh.GioDat = DateTime.Now.TimeOfDay;
                ddh.TrangThai = "Chờ xác nhận";
                ddh.MaBan = null; // Đặt online mang về nên không cần bàn (DB phải cho phép null cột này)

                db.DATBANs.Add(ddh);

                /* --- B. TẠO HÓA ĐƠN --- */
                HOADON hd = new HOADON();
                hd.MaHD = "HD" + DateTime.Now.ToString("MMddHHmmss");
                hd.MaDatBan = ddh.MaDatBan;
                hd.NgayLap = DateTime.Now;
                hd.TongTien = gh.Sum(n => n.dThanhTien);
                hd.PhuongThucTT = "Tiền mặt";
                // hd.MaNV = null; // Đơn online chưa có nhân viên phụ trách

                db.HOADONs.Add(hd);

                /* --- C. TẠO CHI TIẾT HÓA ĐƠN --- */
                foreach (var item in gh)
                {
                    CHITIETHOADON ct = new CHITIETHOADON();
                    ct.MaHD = hd.MaHD;
                    ct.MaMon = item.iMaMon;
                    ct.SoLuong = item.iSoLuong;
                    ct.DonGia = item.dDonGia;
                    // Không gán ThanhTien vì trong DB cột này là Computed Column (tự tính)

                    db.CHITIETHOADONs.Add(ct);
                }

                // --- LƯU TẤT CẢ MỘT LẦN ---
                // Code sẽ chạy Trigger TRG_TruKhoSauKhiBan bạn đã viết trong SQL tại bước này
                db.SaveChanges();
            }

            // 4. Xóa giỏ hàng sau khi đặt thành công
            Session["GioHang"] = null;

            // 5. Chuyển hướng
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }

        // Hàm thêm vào giỏ bình thường (Load lại trang)
        public ActionResult ThemVaoGio(string id, string strURL)
        {
            // 1. Lấy giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();

            // 2. Kiểm tra món ăn
            GioHang sanPham = lstGioHang.Find(n => n.iMaMon == id);
            if (sanPham == null)
            {
                sanPham = new GioHang(id);
                lstGioHang.Add(sanPham);
            }
            else
            {
                sanPham.iSoLuong++;
            }

            // 3. Quay lại đúng trang người dùng vừa đứng (Trang danh sách hoặc trang chi tiết)
            if (string.IsNullOrEmpty(strURL))
            {
                return RedirectToAction("Index", "TrangChu");
            }
            return Redirect(strURL);
        }
    }
}