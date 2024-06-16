using System;
using System.Collections.Generic;

namespace QLBanHangBE.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public string Mahd { get; set; } = null!;
        public DateTime? Ngaylaphd { get; set; }
        public string? Manv { get; set; }
        public string? Makh { get; set; }
        public int? Thanhtien { get; set; }
        public bool? Status { get; set; }

        public virtual KhachHang? MakhNavigation { get; set; }
        public virtual NhanVien? ManvNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
