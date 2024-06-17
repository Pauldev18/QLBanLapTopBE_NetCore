using System;
namespace QLBanHangBE.DTO
{
	public class LapTopDTO
	{
		public string MaLapTop { get; set; }
		public string TenLapTop { get; set; }
		public string TenHang { get; set; }
		public int DonGia { get; set; }
		public int SoLuong { get; set; }
		public IFormFile file { get; set; }
    }
}

