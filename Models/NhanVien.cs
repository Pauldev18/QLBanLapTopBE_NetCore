using System;
using System.Collections.Generic;

namespace QLBanHangBE.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public string Manv { get; set; } = null!;
        public string? Honv { get; set; }
        public string? Tennv { get; set; }
        public string? Phai { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public string? Dienthoai { get; set; }
        public string? Diachi { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
