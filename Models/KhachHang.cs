using System;
using System.Collections.Generic;

namespace QLBanHangBE.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public string Makh { get; set; } = null!;
        public string? Hokh { get; set; }
        public string? Tenkh { get; set; }
        public string? Phai { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public string? Dienthoai { get; set; }
        public string? Diachi { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
