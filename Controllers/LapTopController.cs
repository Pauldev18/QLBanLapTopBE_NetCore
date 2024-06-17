using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QLBanHangBE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using QLBanHangBE.DTO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLBanHangBE.Controllers
{
    public class LapTopController : Controller
    {
        private readonly Cloudinary _cloudinary;
        private readonly QLLapTopContext _context;
        public LapTopController(QLLapTopContext context, IConfiguration configuration)
        {
            this._context = context;
            var cloudName = configuration["Cloudinary:CloudName"];
            var apiKey = configuration["Cloudinary:ApiKey"];
            var apiSecret = configuration["Cloudinary:ApiSecret"];

            Account account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }
        [HttpGet("SanPham")]
        public ActionResult<List<SanPham>> Get()
        {

            var sp = _context.SanPhams.Select(sp => new
            {
                sp.Malaptop,
                sp.Tenlaptop,
                sp.Tenhang,
                sp.Soluong,
                sp.Hinhanh,
                sp.Dongia
            });
            return Ok(sp);
        }
        [HttpPost("AddNewLapTop")]
        public async Task<ActionResult> PostSanPham(LapTopDTO lapTopDTO)
        {
            if (lapTopDTO.file == null || lapTopDTO.file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(lapTopDTO.file.FileName, lapTopDTO.file.OpenReadStream())
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                return BadRequest(uploadResult.Error.Message);
            var laptop = new SanPham
            {
                Malaptop = lapTopDTO.MaLapTop,
                Tenlaptop = lapTopDTO.TenLapTop,
                Dongia = lapTopDTO.DonGia,
                Soluong = lapTopDTO.SoLuong,
                Tenhang = lapTopDTO.TenHang,
                Hinhanh = uploadResult.SecureUrl.ToString()
            };

            _context.SanPhams.Add(laptop);
            await _context.SaveChangesAsync();

            return Ok(laptop);
        }
        [HttpPost("UpdateLapTop")]
        public async Task<ActionResult> UpdateSanPham(LapTopDTO lapTopDTO)
        {
            // Tìm sản phẩm cần cập nhật trong database
            var existingLaptop = await _context.SanPhams.FindAsync(lapTopDTO.MaLapTop);

            if (existingLaptop == null)
            {
                return NotFound("Không tìm thấy sản phẩm");
            }

            // Kiểm tra xem có file được tải lên hay không
            if (lapTopDTO.file != null && lapTopDTO.file.Length > 0)
            {
                // Nếu có file được tải lên, thực hiện upload lên Cloudinary
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(lapTopDTO.file.FileName, lapTopDTO.file.OpenReadStream())
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    return BadRequest(uploadResult.Error.Message);
                }

                // Cập nhật đường dẫn hình ảnh mới
                existingLaptop.Hinhanh = uploadResult.SecureUrl.ToString();
            }

            // Cập nhật các thông tin khác của sản phẩm
            existingLaptop.Tenlaptop = lapTopDTO.TenLapTop;
            existingLaptop.Dongia = lapTopDTO.DonGia;
            existingLaptop.Soluong = lapTopDTO.SoLuong;
            existingLaptop.Tenhang = lapTopDTO.TenHang;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Xử lý các lỗi ngoài ý muốn khi lưu vào cơ sở dữ liệu
                return BadRequest("Lỗi khi cập nhật sản phẩm: " + ex.Message);
            }

            return Ok(existingLaptop);
        }
        [HttpDelete("DeleteLapTop/{MaLapTop}")]
        public async Task<ActionResult> DeleteSanPham(string MaLapTop)
        {
            // Tìm sản phẩm cần xóa trong database
            var laptopToDelete = await _context.SanPhams.FindAsync(MaLapTop);

            if (laptopToDelete == null)
            {
                return NotFound("Không tìm thấy sản phẩm để xóa");
            }

            try
            {
                // Xóa sản phẩm khỏi database
                _context.SanPhams.Remove(laptopToDelete);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Xử lý các lỗi ngoài ý muốn khi xóa từ cơ sở dữ liệu
                return BadRequest("Lỗi khi xóa sản phẩm: " + ex.Message);
            }

            return Ok("Đã xóa sản phẩm thành công");
        }

    }
}

