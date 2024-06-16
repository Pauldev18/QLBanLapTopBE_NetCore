using System;
using System.Collections.Generic;

namespace QLBanHangBE.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public string Malaptop { get; set; } = null!;
        public string? Tenlaptop { get; set; }
        public string? Tenhang { get; set; }
        public int? Dongia { get; set; }
        public int? Soluong { get; set; }
        public string? Hinhanh { get; set; }

        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
