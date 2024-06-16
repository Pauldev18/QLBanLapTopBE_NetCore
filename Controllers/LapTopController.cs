using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QLBanHangBE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLBanHangBE.Controllers
{
    public class LapTopController : Controller
    {
        private readonly QLLapTopContext _context;
        public LapTopController(QLLapTopContext context)
        {
            this._context = context;
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
    }
}

