using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBanHangBE.DTO;
using QLBanHangBE.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLBanHangBE.Controllers
{
    public class HoaDonController : Controller
    {
        private QLLapTopContext _context;
        public HoaDonController(QLLapTopContext context)
        {
            _context = context;
        }
        [HttpGet("getAllHoaDon")]
        public async Task<ActionResult<List<HoaDon>>> GetAllHoaDonAsync()
        {
            try
            {
                var hoaDons = await _context.HoaDons
                    .Include(hd => hd.ChiTietHoaDons)  // Eagerly load the ChiTietHoaDons for each HoaDon
                    .ToListAsync();
                return Ok(hoaDons);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("newHoaDon")]
        public async Task<ActionResult> addHoaDon(HoaDonDTO hoaDonDTO)
        {
            var newHD = new HoaDon
            {
                Mahd = hoaDonDTO.maHoaDon,
                Makh = hoaDonDTO.maKhachHang,
                Manv = hoaDonDTO.maNhanVien,
                Ngaylaphd = DateTime.Now,
                Status = false,
                Thanhtien = 0
            };
            _context.HoaDons.Add(newHD);
            await _context.SaveChangesAsync();

            return Ok(newHD);
        }
        [HttpPut("UpdateHoaDon")]
        public async Task<IActionResult> UpdateHoaDon([FromBody] PayLoadHoaDon hoaDonDTO)
        {
            Console.WriteLine(hoaDonDTO);
            try
            {
                // Tìm HoaDon theo mahd
                var hoaDon = await _context.HoaDons.FirstOrDefaultAsync(hd => hd.Mahd == hoaDonDTO.maHoaDon);

                if (hoaDon == null)
                {
                    return NotFound("Không tìm thấy HoaDon");
                }

                // Cập nhật thông tin HoaDon
                hoaDon.Makh = hoaDonDTO.maKhachHang;
                hoaDon.Manv = hoaDonDTO.maNhanVien;
                hoaDon.Thanhtien = hoaDonDTO.thanhTien;

                // Xóa các chi tiết hóa đơn cũ
                _context.ChiTietHoaDons.RemoveRange(_context.ChiTietHoaDons.Where(ct => ct.Mahd == hoaDonDTO.maHoaDon));

                // Thêm các chi tiết hóa đơn mới từ DTO
                foreach (var chiTietDTO in hoaDonDTO.chiTietHoaDonss)
                {
                    ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon
                    {
                        Mahd = chiTietDTO.mahd,
                        Malaptop = chiTietDTO.malaptop,
                        Dongia = chiTietDTO.dongia,
                        Soluong = chiTietDTO.soluong,
                        Thanhtien = chiTietDTO.thanhtien
                    };

                    _context.ChiTietHoaDons.Add(chiTietHoaDon);
                    await _context.SaveChangesAsync();
                }

                await _context.SaveChangesAsync();

                return Ok("Cập nhật thành công");
            }
            catch (DbUpdateConcurrencyException)
            {
                // Xử lý ngoại lệ khi có xung đột cập nhật cơ sở dữ liệu
                return StatusCode(500, "Lỗi xử lý dữ liệu");
            }
            catch (FormatException)
            {
                // Xử lý ngoại lệ khi chuyển đổi dữ liệu không hợp lệ
                return BadRequest("Dữ liệu không hợp lệ");
            }
        }
        [HttpPut("ThanhToan/{maHoaDon}")]
        public async Task<IActionResult> UpdateStatus(string maHoaDon)
        {
            try
            {
                var hoaDon = await _context.HoaDons.FirstOrDefaultAsync(hd => hd.Mahd == maHoaDon);

                if (hoaDon == null)
                {
                    return NotFound("Không tìm thấy HoaDon");
                }

                hoaDon.Status = true;

                await _context.SaveChangesAsync();

                return Ok(hoaDon.Thanhtien);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi xử lý dữ liệu");
            }
        }

    }
}

