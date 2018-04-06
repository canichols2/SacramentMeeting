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
    public class SpeakerTopicsController : Controller
    {
        private readonly SacramentContext _context;

        public SpeakerTopicsController(SacramentContext context)
        {
            _context = context;
        }

        // GET: SpeakerTopics
        public async Task<IActionResult> Index()
        {
            return View(await _context.SpeakerTopic.ToListAsync());
        }

        // GET: SpeakerTopics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakerTopic = await _context.SpeakerTopic
                .SingleOrDefaultAsync(m => m.Id == id);
            if (speakerTopic == null)
            {
                return NotFound();
            }

            return View(speakerTopic);
        }

        // GET: SpeakerTopics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SpeakerTopics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Topic")] SpeakerTopic speakerTopic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(speakerTopic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(speakerTopic);
        }

        // GET: SpeakerTopics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakerTopic = await _context.SpeakerTopic.SingleOrDefaultAsync(m => m.Id == id);
            if (speakerTopic == null)
            {
                return NotFound();
            }
            return View(speakerTopic);
        }

        // POST: SpeakerTopics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Topic")] SpeakerTopic speakerTopic)
        {
            if (id != speakerTopic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speakerTopic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakerTopicExists(speakerTopic.Id))
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
            return View(speakerTopic);
        }

        // GET: SpeakerTopics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speakerTopic = await _context.SpeakerTopic
                .SingleOrDefaultAsync(m => m.Id == id);
            if (speakerTopic == null)
            {
                return NotFound();
            }

            return View(speakerTopic);
        }

        // POST: SpeakerTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speakerTopic = await _context.SpeakerTopic.SingleOrDefaultAsync(m => m.Id == id);
            _context.SpeakerTopic.Remove(speakerTopic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerTopicExists(int id)
        {
            return _context.SpeakerTopic.Any(e => e.Id == id);
        }
    }
}
