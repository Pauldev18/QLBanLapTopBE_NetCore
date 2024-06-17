using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBanHangBE.DTO;
using QLBanHangBE.Models;
using BCrypt.Net;
using QLBanHangBE.Helper;
using Microsoft.Data.SqlClient;

namespace QLBanHangBE.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly QLLapTopContext _context;

        public TaiKhoanController(QLLapTopContext context)
        {
            _context = context;
        }

        [HttpGet("getAllTaiKhoan")]
        public ActionResult<List<TaiKhoan>> GetAllTaiKhoan()
        {
            var taiKhoans = _context.TaiKhoans.ToList();
            return Ok(taiKhoans);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<TaiKhoan>> Login(LoginDTO loginDTO)
        {
            try
            {
                var taiKhoan = await _context.TaiKhoans.SingleOrDefaultAsync(tk =>
                    tk.Taikhoan1 == loginDTO.TenDangNhap);

                if (taiKhoan == null)
                {
                    return NotFound("Invalid username or password.");
                }
                
                if (!PasswordHasher.VerifyPassword(loginDTO.MatKhau, taiKhoan.Matkhau))
                {
                    return BadRequest("Incorrect password for the provided username.");
                }

                return Ok(taiKhoan);
            }
            catch (Exception ex)
            {
                return BadRequest($"Login failed: {ex.Message}");
            }
        }


        [HttpPost("AddTaiKhoan")]
        public async Task<ActionResult> AddTaiKhoan(TaiKhoan tk)
        {
            try
            {
                // Hash mật khẩu trước khi lưu vào cơ sở dữ liệu
                tk.Matkhau = PasswordHasher.HashPassword(tk.Matkhau);

                _context.TaiKhoans.Add(tk);
                await _context.SaveChangesAsync();

                return Ok(tk); // Trả về thành công nếu lưu thành công
            }
            catch (DbUpdateException ex)
            {
                // Lỗi xảy ra khi cố gắng cập nhật cơ sở dữ liệu
                // Trích xuất inner exception để biết nguyên nhân chính xác
                Exception innerException = ex.InnerException;
                string errorMessage = "Error occurred while saving to database.";

                // Kiểm tra nếu là lỗi do vi phạm ràng buộc duy nhất (unique constraint)
                if (innerException is SqlException sqlEx && sqlEx.Number == 2627)
                {
                    errorMessage = "Username or email already exists.";
                }

                return BadRequest(errorMessage);
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi khác
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("UpdateTaiKhoan")]
        public async Task<IActionResult> UpdateTaiKhoan(TaiKhoan taiKhoan)
        {
           
            var existingTaiKhoan = await _context.TaiKhoans.FindAsync(taiKhoan.Taikhoan1);

            if (existingTaiKhoan == null)
            {
                return NotFound("TaiKhoan not found");
            }

            existingTaiKhoan.Matkhau = PasswordHasher.HashPassword(taiKhoan.Matkhau);
            existingTaiKhoan.Quyen = taiKhoan.Quyen;
            // Cập nhật các thông tin khác của tài khoản cần thiết

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Update successful");
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Update error");
            }
        }

        [HttpDelete("DeleteTaiKhoan/{username}")]
        public async Task<IActionResult> DeleteTaiKhoan(string username)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(username);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            _context.TaiKhoans.Remove(taiKhoan);
            await _context.SaveChangesAsync();

            return Ok("Xóa thành công");
        }
    }
}
