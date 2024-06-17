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
    public class KhachHangController : Controller
    {
        private readonly QLLapTopContext _context;

        public KhachHangController(QLLapTopContext context)
        {
            _context = context;
        }

        [HttpGet("getAllKhachHang")]
        public async Task<ActionResult<IEnumerable<KhachHangDTO>>> GetKhachHangs()
        {
            var khachHangs = await _context.KhachHangs
                .Select(kh => new KhachHangDTO
                {
                    Makh = kh.Makh,
                    Hokh = kh.Hokh,
                    Tenkh = kh.Tenkh,
                    Phai = kh.Phai,
                    Ngaysinh = kh.Ngaysinh,
                    Dienthoai = kh.Dienthoai,
                    Diachi = kh.Diachi
                })
                .ToListAsync();

            return Ok(khachHangs);
        }
        [HttpPost("newKhachHang")]
        public async Task<ActionResult<KhachHangDTO>> PostKhachHang(KhachHangDTO khachHangDTO)
        {
            var khachHang = new KhachHang
            {
                Makh = khachHangDTO.Makh,
                Hokh = khachHangDTO.Hokh,
                Tenkh = khachHangDTO.Tenkh,
                Phai = khachHangDTO.Phai,
                Ngaysinh = khachHangDTO.Ngaysinh,
                Dienthoai = khachHangDTO.Dienthoai,
                Diachi = khachHangDTO.Diachi
            };

            _context.KhachHangs.Add(khachHang);
            await _context.SaveChangesAsync();

            return Ok(khachHang);
        }

        [HttpDelete("DeleteKhachHang/{id}")]
        public async Task<IActionResult> DeleteKhachHang(string id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            _context.KhachHangs.Remove(khachHang);
            await _context.SaveChangesAsync();

            return Ok("Xóa thành công");
        }
        [HttpPut("UpdateKhachHang")]
        public async Task<IActionResult> UpdateKhachHang(KhachHangDTO khachHangDTO)
        {
            var khachHang = await _context.KhachHangs.FindAsync(khachHangDTO.Makh);
            if (khachHang == null)
            {
                return NotFound();
            }

            // Update the KhachHang object with the new data
            khachHang.Hokh = khachHangDTO.Hokh;
            khachHang.Tenkh = khachHangDTO.Tenkh;
            khachHang.Phai = khachHangDTO.Phai;
            khachHang.Ngaysinh = khachHangDTO.Ngaysinh;
            khachHang.Dienthoai = khachHangDTO.Dienthoai;
            khachHang.Diachi = khachHangDTO.Diachi;

            // Mark entity as modified
            _context.Entry(khachHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception if necessary
                throw;
            }

            return Ok("Update thành công");
        }

    }
}


