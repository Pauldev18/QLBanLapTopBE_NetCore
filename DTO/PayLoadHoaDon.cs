using System;
namespace QLBanHangBE.DTO
{
	public class PayLoadHoaDon
	{
        public string maHoaDon { get; set; }
        public string maKhachHang { get; set; }
        public string maNhanVien { get; set; }
        public int thanhTien { get; set; }
        public List<ChiTietHoaDonDTO> chiTietHoaDonss { get; set; }

    }
}

