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
    public class SacramentsController : Controller
    {
        private readonly SacramentContext _context;

        public SacramentsController(SacramentContext context)
        {
            _context = context;
        }

        // GET: Sacraments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sacrament.Include(i=>i.Speakers).ThenInclude(s=>s.Member).ToListAsync());
        }

        // GET: Sacraments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacrament = await _context.Sacrament
                .Include(m => m.Speakers)
                    .ThenInclude(s => s.Member)
                .SingleOrDefaultAsync(m => m.Id == id)
                ;
            if (sacrament == null)
            {
                return NotFound();
            }

            return View(sacrament);
        }

        // GET: Sacraments/Create
        public IActionResult Create()
        {
            var sac = new Sacrament { Speakers = new List<Speakers>(), date = DateTime.Now };
            GetMembersForDropdown();
            //ViewData["Callings"] = new SelectList(_context.MemberCalling.Where(c=>c.Active==true).Include(c=>c.Member), "Id", "Member.FullName");
            return View(sac);
        }

        private void GetMembersForDropdown()
        {
            ViewData["Members"] = new SelectList(_context.Member, "Id", "FullName");
            ViewData["Topics"] = new SelectList(_context.SpeakerTopic, "Id", "Topic");
            ViewData["Callings"] = new SelectList(_context.Calling, "Id", "CallingName");
        }

        // POST: Sacraments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,date,OpeningSong,SacramentSong,IntermediateSong,ClosingSong")] Sacrament sacrament, 
            string[] selectedSpeakers,
            string[] SpeakerTopic)
        {
            GetMembersForDropdown();
            _context.Add(sacrament);
            UpdateSpeakers(sacrament, selectedSpeakers,SpeakerTopic);
            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if(sacrament.Speakers == null)
                sacrament.Speakers = new List<Speakers>();
            return View(sacrament);
        }

        // GET: Sacraments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            GetMembersForDropdown();

            var sacrament = await _context.Sacrament
                .Include(m=>m.Speakers)
                    .ThenInclude(s=>s.Member)
                .SingleOrDefaultAsync(m => m.Id == id)
                ;
            if (sacrament == null)
            {
                return NotFound();
            }
            return View(sacrament);
        }

        // POST: Sacraments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id, 
            [Bind("Id,date,OpeningSong,SacramentSong,IntermediateSong,ClosingSong")] Sacrament sacrament,
            string[] selectedSpeakers,
            String[] SpeakerTopic
            )
        {
            if (id != sacrament.Id)
            {
                return NotFound();
            }
            GetMembersForDropdown();

            if (ModelState.IsValid)
            {
                _context.Attach(sacrament);
                ;
                UpdateSpeakers(sacrament, selectedSpeakers, SpeakerTopic);
                try
                {
                    _context.Update(sacrament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SacramentExists(sacrament.Id))
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
            return View(sacrament);
        }

        private void UpdateSpeakers(Sacrament sacrament, String[] selectedSpeakers, String[] SpeakerTopic)
        {
            GetMembersForDropdown();

            ICollection<Member> selectedSpeakersList = new List<Member>();
            ICollection<Speakers> allSpeakers = _context.Speakers
                .Include(s=>s.Member).ToList();
        
            //_context.Member.find
            if(selectedSpeakers[0] != null)
            for(int i = 0; i < selectedSpeakers.Length;i++)
           {
                var firstName = "";
                var lastName = "";
                    if (selectedSpeakers[i] != null) //continue;
                if(selectedSpeakers[i].Contains(","))
                {
                    var splitName = selectedSpeakers[i].Split(",");
                    firstName = splitName[1].Trim();
                    lastName = splitName[0].Trim();
                }
                else
                {
                    firstName = selectedSpeakers[i].Trim();
                    // Was going to be for first *SPACE* last,
                    // // but since you can have spaces in the FirstMiddleName, 
                    // // it wasn't going to work
                    //var splitName = selectedSpeakers[i].Split(" ");
                    //firstName = splitName[0];
                    //if(splitName.Length > 1)
                    //    lastName = splitName[splitName.Length-1];
                }
                


                if(firstName == "")
                {
                    continue;
                }

                if (_context.Member.Any(m => m.FirstMiddleName == firstName && m.LastName == lastName))
                {
                    Member mem = _context.Member.SingleOrDefault(m => m.FirstMiddleName == firstName && m.LastName == lastName);

                    selectedSpeakersList.Add(mem);
                }
                else
                {
                    //Create Entity and add it to speakerList....
                    var member = new Member();
                    member.FirstMiddleName = firstName;
                    member.LastName = lastName;
                    member.BaptizeDate = DateTime.Now;
                    _context.Add(member);
                    selectedSpeakersList.Add(member);
                }
            }
            _context.SaveChanges();

            //Remove old speaker references.
            foreach (var aSpeak in allSpeakers)
            {
                if (!selectedSpeakersList.Contains(aSpeak.Member) && aSpeak.Sacrament == sacrament)
                {
                    sacrament.Speakers.Remove(aSpeak);
                }
            }
            if (sacrament.Speakers == null)
                sacrament.Speakers = new List<Speakers>();
            //Add new speaker references (If Not Exist)
            foreach (var member in selectedSpeakersList)
            {
                if(!sacrament.Speakers.Any(s=>s.Member == member))
                    sacrament.Speakers.Add(new Speakers { SacramentID = sacrament.Id, MemberID = member.Id, Member = member });
            }
            //_context.SaveChanges();
        }

        // GET: Sacraments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacrament = await _context.Sacrament
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sacrament == null)
            {
                return NotFound();
            }

            return View(sacrament);
        }

        // POST: Sacraments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sacrament = await _context.Sacrament.SingleOrDefaultAsync(m => m.Id == id);
            _context.Sacrament.Remove(sacrament);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SacramentExists(int id)
        {
            return _context.Sacrament.Any(e => e.Id == id);
        }
    }
}
