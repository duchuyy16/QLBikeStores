namespace QLBikeStores.Models
{
    public class ThongKeModel
    {
        public class Input { }
        public class Output
        {
            public class ThongKeSanPhamTheoTheLoai
            {
                public int IdTheLoai { get; set; }
                public string TenTheLoai { get; set; }
                public int SoLuongSanPham { get; set; }
            }
        }
    }
}
