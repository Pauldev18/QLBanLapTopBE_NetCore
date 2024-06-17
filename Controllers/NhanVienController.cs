using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLBanHangBE.DTO;
using QLBanHangBE.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLBanHangBE.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly QLLapTopContext _context;
        public NhanVienController(QLLapTopContext context)
        {
            this._context = context;
        }
        [HttpGet("getAllNhanVien")]
        public ActionResult<List<NhanVien>> GetAllNhanVien()
        {

            var nv = _context.NhanViens.Select(nv => new
            {
                nv.Manv,
                nv.Tennv,
                nv.Honv,
                nv.Phai,
                nv.Ngaysinh,
                nv.Diachi,
                nv.Dienthoai
            });
            return Ok(nv);
        }
        [HttpPost("AddNhanVien")]
        public async Task<ActionResult> addNhanVien(NhanVienDTO nhanVienDTO)
        {
            var newNv = new NhanVien
            {
                Manv = nhanVienDTO.MaNV,
                Tennv = nhanVienDTO.Tennv,
                Diachi = nhanVienDTO.Diachi,
                Dienthoai = nhanVienDTO.Dienthoai,
                Honv = nhanVienDTO.Honv,
                Ngaysinh = nhanVienDTO.Ngaysinh,
                Phai = nhanVienDTO.Phai
            };
            _context.NhanViens.Add(newNv);
            await _context.SaveChangesAsync();

            return Ok(newNv);
        }
        [HttpPut("UpdateNhanVien")]
        public async Task<ActionResult> UpdateNhanVien(NhanVienDTO nhanVienDTO)
        {
            var existingNhanVien = await _context.NhanViens.FindAsync(nhanVienDTO.MaNV);

            if (existingNhanVien == null)
            {
                return NotFound("Không tìm thấy nhân viên");
            }

            // Cập nhật thông tin từ DTO vào đối tượng nhân viên hiện có
            existingNhanVien.Tennv = nhanVienDTO.Tennv;
            existingNhanVien.Honv = nhanVienDTO.Honv;
            existingNhanVien.Phai = nhanVienDTO.Phai;
            existingNhanVien.Ngaysinh = nhanVienDTO.Ngaysinh;
            existingNhanVien.Diachi = nhanVienDTO.Diachi;
            existingNhanVien.Dienthoai = nhanVienDTO.Dienthoai;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(existingNhanVien);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi cập nhật nhân viên: " + ex.Message);
            }
        }
        [HttpDelete("DeleteNhanVien/{MaNhanVien}")]
        public async Task<ActionResult> DeleteNhanVien(string MaNhanVien)
        {
            var nhanVienToDelete = await _context.NhanViens.FindAsync(MaNhanVien);

            if (nhanVienToDelete == null)
            {
                return NotFound("Không tìm thấy nhân viên để xóa");
            }

            try
            {
                _context.NhanViens.Remove(nhanVienToDelete);
                await _context.SaveChangesAsync();
                return Ok("Đã xóa nhân viên thành công");
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi xóa nhân viên: " + ex.Message);
            }
        }
    }
}

