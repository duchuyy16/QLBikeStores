namespace QLBikeStores.Helpers
{
    public class ConstantValues
    {
        public class Product
        {
            public const string DanhSachSanPham = "api/Product/DanhSachSanPham";
            public const string ChiTietSanPham = "api/Product/ChiTietSanPham/{0}";
            public const string DocDanhSachSanPhamTheoTheLoai = "api/Product/DocDanhSachSanPhamTheoTheLoai/{0}";
            public const string DanhSachSanPhamTheoTheLoaiThuongHieu = "api/Product/DocDanhSachSanPhamTheoTheLoaiThuongHieu/{0}&{1}";
            public const string TimKiem = "api/Product/TimKiem/{0}";
            public const string ThemSanPham = "api/Product/ThemSanPham";
            public const string CapNhatSanPham = "api/Product/CapNhatSanPham";
            public const string XoaSanPham = "api/Product/XoaSanPham";
            public const string Find = "api/Product/Find/{0}";
            public const string ProductExists = "api/Product/ProductExists/{0}";
            public const string Search = "api/Product/Search?name={0}&to={1}&from={2}";
           
        }

        public class Store
        {
            public const string DocDanhSachCuaHang = "api/Store/DanhSachCuaHang";
            public const string ChiTietCuaHang = "api/Store/ChiTietCuaHang/{0}";
            public const string ThemCuaHang = "api/Store/ThemCuaHang";
            public const string CapNhatCuaHang = "api/Store/CapNhatCuaHang";
            public const string XoaCuaHang = "api/Store/XoaCuaHang";
            public const string StoreExists = "api/Store/StoreExists/{0}";
            public const string TimKiem = "api/Store/TimKiem/{0}";
        }

        public class Stock
        {
            public const string DocDanhSach = "api/Stock/DocDanhSach";
            public const string ChiTiet = "api/Stock/ChiTiet/{0}&{1}";
            public const string ThemKhoHang = "api/Stock/ThemKhoHang";
            public const string CapNhatKho = "api/Stock/CapNhatKho";
            public const string XoaKhoHang = "api/Stock/XoaKhoHang";
            public const string StockExists = "api/Stock/StockExists/{0}&{1}";
            public const string TimKiem = "api/Stock/TimKiem/{0}&{1}";
        }
        public class Staff
        {
            public const string DanhSachNhanVien = "api/Staff/DocDanhSachNhanVien";
            public const string ChiTietNhanVien = "api/Staff/ChiTietNhanVien/{0}";
            public const string ThemNhanVienMoi = "api/Staff/ThemNhanVienMoi";
            public const string CapNhatNhanVien = "api/Staff/CapNhatNhanVien";
            public const string XoaNhanVien = "api/Staff/XoaNhanVien";
            public const string StaffExists = "api/Staff/StoreExists/{0}";
            public const string TimKiem = "api/Staff/TimKiem/{0}";
            public const string DangNhap = "api/Staff/DangNhap";
        }
        public class Category
        {
            public const string ChiTietTheLoai = "api/Category/DocThongTin/{0}";
            public const string DanhSachTheLoai = "api/Category/DanhSachTheLoai";
            public const string ThemTheLoai = "api/Category/ThemTheLoai";
            public const string CapNhatTheLoai = "api/Category/CapNhatTheLoai";
            public const string XoaTheLoai = "api/Category/XoaTheLoai";
            public const string TimKiem = "api/Category/TimKiem/{0}";
            public const string CategoryExists = "api/Category/CategoryExists/{0}";
            public const string ThongKeSanPhamTheoTheLoai = "api/ThongKe/ThongKeSanPhamTheoTheLoai";
        }
        public class Customer
        {
            public const string DanhSachKhachHang = "api/Customer/DanhSachKhachHang";
            public const string ChiTietKhachHang = "api/Customer/ChiTietKhachHang/{0}";
            public const string ThemKhachHang = "api/Customer/ThemKhachHang";
            public const string CapNhatKhachHang = "api/Customer/CapNhatKhachHang";
            public const string XoaKhachHang = "api/Customer/XoaKhachHang";
            public const string CustomerExists = "api/Customer/CustomerExists/{0}";
            public const string KiemTraUsername = "api/Customer/KiemTraUsername/{0}";
            public const string TimKiem = "api/Customer/TimKiem/{0}";
            public const string DangNhap = "api/Customer/DangNhap";
            public const string DangKy = "api/Customer/DangKy";
        }
        public class Brand
        {
            public const string DanhSachNhanHieu = "api/Brand/DanhSachNhanHieu";
            public const string ChiTietNhanHieu = "api/Brand/ChiTietNhanHieu/{0}";
            public const string ThemNhanHieu = "api/Brand/ThemNhanHieu";
            public const string CapNhatNhanHieu = "api/Brand/CapNhatNhanHieu";
            public const string XoaNhanHieu = "api/Brand/XoaNhanHieu";
            public const string TimKiem = "api/Brand/TimKiem/{0}"; 
            public const string BrandExists = "api/Brand/BrandExists/{0}";
        }
        public class Order
        {
            public const string DocDanhSachMuaHang = "api/Order/DocDanhSachMuaHang";
            public const string ChiTietMuaHang = "api/Order/ChiTietMuaHang/{0}";
            public const string ThemDonHang = "api/Order/ThemDonHang";
            public const string CapNhatDonHang = "api/Order/CapNhatDonHang";
            public const string XoaDonHang = "api/Order/XoaDonHang";
            public const string TimKiem = "api/Order/TimKiem/{0}";
            public const string OrderExists = "api/Order/OrderExists/{0}";
        }
        public class OrderItem
        {
            public const string DanhSachDonDatHang = "api/OrderItem/DanhSachDonDatHang";
            public const string ChiTietDonDatHang = "api/OrderItem/ChiTietDonDatHang/{0}&{1}";
            public const string ThemChiTietDonDatHang = "api/OrderItem/ThemChiTietDonDatHang";
            public const string CapNhatChiTietDonHang = "api/OrderItem/CapNhatChiTietDonHang";
            public const string XoaChiTietDonHang = "api/OrderItem/XoaChiTietDonHang";
            public const string OrderItemExists = "api/OrderItem/OrderItemExists/{0}&{1}";
            public const string TimKiem = "api/OrderItem/TimKiem/{0}&{1}";
        }
        public class Role
        {
            public const string DanhSachQuyen = "api/Role/DanhSachQuyen";
            public const string ChiTietQuyen = "api/Role/ChiTietQuyen/{0}";
            public const string ThemQuyen = "api/Role/ThemQuyen";
            public const string CapNhatQuyen = "api/Role/CapNhatQuyen";
            public const string XoaQuyen = "api/Role/XoaQuyen";
            public const string TimKiem = "api/Role/TimKiem/{0}";
            public const string RoleExists = "api/Role/RoleExists/{0}";
        }
        public class Contact
        {
            public const string ThemLienLac = "api/Contact/ThemLienLac";
            public const string DanhSachLienLac = "api/Contact/DanhSachLienLac";
            public const string ChiTietLienLac = "api/Contact/ChiTiet/{0}";
            public const string CapNhatLienLac = "api/Contact/CapNhatLienLac";
            public const string XoaLienLac = "api/Contact/XoaLienLac";
            public const string TimKiem = "api/Contact/TimKiem/{0}";
            public const string ContactExists = "api/Contact/ContactExists/{0}";
        }
    }
}
