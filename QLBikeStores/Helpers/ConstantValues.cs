namespace QLBikeStores.Helpers
{
    public class ConstantValues
    {
        public class Product
        {
            public const string DanhSachSanPham = "api/Product/DocDanhSachSanPham";
            public const string ChiTietSanPham = "api/Product/ChiTietSanPham/{id}";
            public const string DocDanhSachSanPhamTheoTheLoai = "api/Product/DocDanhSachSanPhamTheoTheLoai/{categoryId}";
            public const string DocDanhSachSanPhamTheoTheLoaiThuongHieu = "api/Product/DocDanhSachSanPhamTheoTheLoaiThuongHieu/{0}&{1}";
            public const string TimKiem = "api/Product/TiemKiem/{name}";
            public const string ThemSanPham = "api/Product/ThemSanPham";
            public const string CapNhatSanPham = "api/Product/CapNhatSanPham";
            public const string XoaSanPham = "api/Product/XoaSanPham";
        }

        public class Store
        {
            public const string DocDanhSachCuaHang = "api/Store/DanhSachCuaHang";
            public const string ChiTietCuaHang = "api/Store/ChiTietCuaHang/{id}";
            public const string ThemCuaHang = "api/Store/ThemCuaHang";
            public const string CapNhatCuaHang = "api/Store/CapNhatCuaHang";
            public const string XoaCuaHang = "api/Store/XoaCuaHang";
        }

        public class Stock
        {
            public const string DocDanhSach = "api/Stock/DocDanhSach";
            public const string ChiTiet = "api/Stock/ChiTiet/{productId}&{storeId}";
            public const string ThemKhoHang = "api/Stock/ThemKhoHang";
            public const string CapNhatKho = "api/Stock/CapNhatKho";
            public const string XoaKhoHang = "api/Stock/XoaKhoHang";
        }
        public class Staff
        {
            public const string DanhSachNhanVien = "api/Staff/DocDanhSachNhanVien";
            public const string ChiTietNhanVien = "api/Staff/ChiTietNhanVien/{id}";
            public const string ThemNhanVienMoi = "api/Staff/ThemNhanVienMoi";
            public const string CapNhatNhanVien = "api/Staff/CapNhatNhanVien";
            public const string XoaNhanVien = "api/Staff/XoaNhanVien";
        }
        public class Category
        {
            public const string DocThongTin = "api/Category/DocThongTin/{id}";
            public const string DanhSachTheLoai = "api/Category/DanhSachTheLoai";
            public const string ThemTheLoai = "api/Category/ThemTheLoai";
            public const string CapNhatTheLoai = "api/Category/CapNhatTheLoai";
            public const string XoaTheLoai = "api/Category/XoaTheLoai";
        }
        public class Customer
        {
            public const string DanhSachKhachHang = "api/Customer/DanhSachKhachHang";
            public const string ChiTietKhachHang = "api/Customer/ChiTietKhachHang/{id}";
            public const string ThemKhachHang = "api/Customer/ThemKhachHang";
            public const string CapNhatKhachHang = "api/Customer/CapNhatKhachHang";
            public const string XoaKhachHang = "api/Customer/XoaKhachHang";
        }
        public class Brand
        {
            public const string DanhSachKhachHang = "api/Brand/DanhSachNhanHieu";
            public const string ChiTietNhanHieu = "api/Brand/ChiTietNhanHieu/{id}";
            public const string ThemNhanHieu = "api/Brand/ThemNhanHieu";
            public const string CapNhatNhanHieu = "api/Brand/CapNhatNhanHieu";
            public const string XoaNhanHieu = "api/Brand/XoaNhanHieu";
        }
        public class Order
        {
            public const string DocDanhSachMuaHang = "api/Order/DocDanhSachMuaHang";
            public const string ChiTietMuaHang = "api/Order/ChiTietMuaHang/{id}";
            public const string ThemDonHang = "api/Order/ThemDonHang";
            public const string CapNhatDonHang = "api/Order/CapNhatDonHang";
            public const string XoaDonHang = "api/Order/XoaDonHang";
        }
        public class OrderItem
        {
            public const string DanhSachDonDatHang = "api/OrderItem/DanhSachDonDatHang";
            public const string ChiTietDonDatHang = "api/OrderItem/ChiTietDonDatHang/{orderId}&{itemId}";
            public const string ThemChiTietDonDatHang = "api/OrderItem/ThemChiTietDonDatHang";
            public const string CapNhatChiTietDonHang = "api/OrderItem/CapNhatChiTietDonHang";
            public const string XoaChiTietDonHang = "api/OrderItem/XoaChiTietDonHang";
        }
    }
}
