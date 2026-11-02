# Website Quản Lý & Đặt Bàn Nhà Hàng (ASP.NET MVC & Entity Framework)

Hệ thống Website quản lý và đặt bàn / gọi món nhà hàng trực tuyến được phát triển bằng **ASP.NET MVC**, **C#**, **Entity Framework 6** và cơ sở dữ liệu **Microsoft SQL Server**.

---

## 📌 Các Tính Năng Chính

### Phân Hệ Khách Hàng (Client)
- **Trang chủ & Thực đơn**: Xem danh mục món ăn theo từng phân loại (món khai vị, món chính, đồ uống, tráng miệng).
- **Xem chi tiết món ăn**: Xem thông tin mô tả, giá tiền và hình ảnh món ăn.
- **Giỏ hàng & Đặt hàng**: Thêm món ăn vào giỏ hàng trực tuyến, chỉnh sửa số lượng và đặt món giao tận nơi hoặc tại bàn.
- **Đặt bàn trực tuyến (`DATBAN`)**: Đặt trước bàn ăn theo ngày, giờ, số lượng khách và ghi chú yêu cầu riêng.
- **Tài khoản người dùng**: Đăng ký, đăng nhập và quản lý lịch sử đơn hàng.

### Phân Hệ Quản Trị & Vận Hành (Admin & Staff)
- **Quản lý thực đơn & nguyên liệu**: Thêm, sửa, xóa món ăn (`MONAN`), định mức nguyên liệu tiêu hao cho từng món (`MONAN_NGUYENLIEU`), quản lý danh mục nguyên liệu (`NGUYENLIEU`).
- **Quản lý nhập kho**: Quản lý phiếu nhập kho nguyên liệu (`PHIEUNHAPKHO`), chi tiết nhập kho (`CHITIET_NHAPKHO`).
- **Quản lý hóa đơn & bàn ăn**: Tiếp nhận yêu cầu đặt bàn, quản lý trạng thái bàn ăn (`BANAN`), xuất hóa đơn (`HOADON`).
- **Quản lý nhân viên & Phân quyền**: Quản lý thông tin nhân viên (`NHANVIEN`), phân quyền chức năng và theo dõi nhật ký đăng nhập hệ thống (`NHATKY_DANGNHAP`).

---

## 🛠️ Công Nghệ Sử Dụng
- **Framework**: ASP.NET MVC 5 (.NET Framework 4.7.2)
- **Ngôn ngữ**: C#
- **ORM**: Entity Framework 6 (Database First / Model First)
- **Cơ sở dữ liệu**: Microsoft SQL Server
- **Frontend**: Razor View Engine, Bootstrap, HTML5/CSS3, jQuery

---

## 🚀 Hướng Dẫn Cài Đặt & Chạy Dự Án

### 1. Yêu cầu hệ thống
- Visual Studio 2019 / 2022 (đã cài đặt workload **ASP.NET and web development**)
- Microsoft SQL Server / SQL Server Express / LocalDB

### 2. Cấu hình Cơ Sở Dữ Liệu
1. Mở file cấu hình `Web.config`.
2. Kiểm tra chuỗi kết nối `connectionStrings` (`DB_NhaHangEntities`) và chỉnh sửa `data source`, `Initial Catalog`, thông tin đăng nhập phù hợp với máy của bạn.

### 3. Khởi chạy
1. Mở giải pháp `DA_LTW.sln` bằng Visual Studio.
2. Chuột phải vào Solution -> chọn **Restore NuGet Packages**.
3. Nhấn **F5** (hoặc `Ctrl + F5`) để biên dịch và chạy trên trình duyệt.
