using System;
using System.Collections.Generic;

namespace QLBanHangBE.Models
{
    public partial class ChiTietHoaDon
    {
        public string Mahd { get; set; } = null!;
        public string Malaptop { get; set; } = null!;
        public int? Dongia { get; set; }
        public int? Soluong { get; set; }
        public int? Thanhtien { get; set; }

        public virtual HoaDon MahdNavigation { get; set; } = null!;
        public virtual SanPham MalaptopNavigation { get; set; } = null!;
    }
}
