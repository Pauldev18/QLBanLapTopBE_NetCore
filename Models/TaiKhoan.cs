using System;
using System.Collections.Generic;

namespace QLBanHangBE.Models
{
    public partial class TaiKhoan
    {
        public string Taikhoan1 { get; set; } = null!;
        public string? Matkhau { get; set; }
        public int? Quyen { get; set; }
    }
}
