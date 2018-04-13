using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SacramentMeeting.Models;

namespace SacramentMeeting.Controllers
{
    public class HomeController : Controller
    {
        private readonly SacramentContext _context;
        public HomeController(SacramentContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Sacrament.Include(s=>s.Sacrament).ThenInclude(s=>s.Member).ToListAsync());
            //return View(await _context.Sacrament.ToListAsync());
            //return View(_context.Sacrament.ToList());


            var sacrament = await _context.Sacrament
                .Include(s => s.Speakers)
                    .ThenInclude(r => r.Member)
                .Include(m => m.Speakers)
                    .ThenInclude(s => s.Topic)
                .Include(l => l.Presiding)
                .Include(x => x.Conducting)
                .Include(s => s.Invocation).Include(S => S.Benediction)
                .OrderByDescending(r => r.date)
                .Take(2).ToListAsync();

            return View(sacrament);
        }




        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
