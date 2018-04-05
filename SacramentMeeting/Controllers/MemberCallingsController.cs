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
    public class MemberCallingsController : Controller
    {
        private readonly SacramentContext _context;

        public MemberCallingsController(SacramentContext context)
        {
            _context = context;
        }

        // GET: MemberCallings
        public async Task<IActionResult> Index()
        {
            var sacramentContext = _context.MemberCalling.Include(m => m.Calling).Include(m => m.Member);
            return View(await sacramentContext.ToListAsync());
        }

        // GET: MemberCallings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberCalling = await _context.MemberCalling
                .Include(m => m.Calling)
                .Include(m => m.Member)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (memberCalling == null)
            {
                return NotFound();
            }

            return View(memberCalling);
        }

        // GET: MemberCallings/Create
        public IActionResult Create()
        {
            ViewData["CallingId"] = new SelectList(_context.Calling, "Id", "CallingName");
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "FullName");
            return View();
        }

        // POST: MemberCallings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemberId,CallingId,Active")] MemberCalling memberCalling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberCalling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CallingId"] = new SelectList(_context.Calling, "Id", "Id", memberCalling.CallingId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", memberCalling.MemberId);
            return View(memberCalling);
        }

        // GET: MemberCallings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberCalling = await _context.MemberCalling.SingleOrDefaultAsync(m => m.Id == id);
            if (memberCalling == null)
            {
                return NotFound();
            }
            ViewData["CallingId"] = new SelectList(_context.Calling, "Id", "Id", memberCalling.CallingId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", memberCalling.MemberId);
            return View(memberCalling);
        }

        // POST: MemberCallings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemberId,CallingId,Active")] MemberCalling memberCalling)
        {
            if (id != memberCalling.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberCalling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberCallingExists(memberCalling.Id))
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
            ViewData["CallingId"] = new SelectList(_context.Calling, "Id", "Id", memberCalling.CallingId);
            ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", memberCalling.MemberId);
            return View(memberCalling);
        }

        // GET: MemberCallings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberCalling = await _context.MemberCalling
                .Include(m => m.Calling)
                .Include(m => m.Member)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (memberCalling == null)
            {
                return NotFound();
            }

            return View(memberCalling);
        }

        // POST: MemberCallings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberCalling = await _context.MemberCalling.SingleOrDefaultAsync(m => m.Id == id);
            _context.MemberCalling.Remove(memberCalling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberCallingExists(int id)
        {
            return _context.MemberCalling.Any(e => e.Id == id);
        }
    }
}
