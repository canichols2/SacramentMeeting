using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentMeeting.Models;

namespace SacramentMeeting.Controllers
{
    public class CallingsController : Controller
    {
        private readonly SacramentContext _context;

        public CallingsController(SacramentContext context)
        {
            _context = context;
        }

        // GET: Callings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Calling.ToListAsync());
        }

        // GET: Callings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calling = await _context.Calling
                .SingleOrDefaultAsync(m => m.Id == id);
            if (calling == null)
            {
                return NotFound();
            }

            return View(calling);
        }

        // GET: Callings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Callings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CallingName")] Calling calling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calling);
        }

        // GET: Callings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calling = await _context.Calling.SingleOrDefaultAsync(m => m.Id == id);
            if (calling == null)
            {
                return NotFound();
            }
            return View(calling);
        }

        // POST: Callings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CallingName")] Calling calling)
        {
            if (id != calling.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallingExists(calling.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(calling);
        }

        // GET: Callings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calling = await _context.Calling
                .SingleOrDefaultAsync(m => m.Id == id);
            if (calling == null)
            {
                return NotFound();
            }

            return View(calling);
        }

        // POST: Callings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calling = await _context.Calling.SingleOrDefaultAsync(m => m.Id == id);
            _context.Calling.Remove(calling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CallingExists(int id)
        {
            return _context.Calling.Any(e => e.Id == id);
        }
    }
}
