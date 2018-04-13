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
            return View(await _context.Sacrament.Include(i => i.Speakers).ThenInclude(s => s.Member).Include(s=>s.Presiding).Include(S=>S.Conducting).ToListAsync());
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
                .Include(m => m.Speakers)
                    .ThenInclude(s => s.Topic)
                .Include(s => s.Presiding).Include(S => S.Conducting)
                .Include(s => s.Invocation).Include(S => S.Benediction)
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
            string[] SpeakerTopic,
            string Presiding,
            string Conducting,
            string Invocation,
            string Benediction)
        {
            GetMembersForDropdown();
            _context.Add(sacrament);
            sacrament.Presiding = await getMember(Presiding);
            sacrament.Conducting = await getMember(Conducting);
            sacrament.Invocation = await getMember(Invocation);
            sacrament.Benediction = await getMember(Benediction);
            await UpdateSpeakers(sacrament, selectedSpeakers, SpeakerTopic);
            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (sacrament.Speakers == null)
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
                .Include(m => m.Speakers)
                    .ThenInclude(s => s.Member)
                .Include(m => m.Speakers)
                    .ThenInclude(s => s.Topic)
                .Include(s => s.Presiding).Include(S => S.Conducting)
                .Include(s => s.Invocation).Include(S => S.Benediction)
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
            String[] SpeakerTopic,
            string Presiding,
            string Conducting,
            string Invocation,
            string Benediction
            )
        {
            if (id != sacrament.Id)
            {
                return NotFound();
            }
            GetMembersForDropdown();
            sacrament.Presiding = await getMember(Presiding);
            sacrament.Conducting = await getMember(Conducting);
            sacrament.Invocation = await getMember(Invocation);
            sacrament.Benediction = await getMember(Benediction);
            if (ModelState.IsValid)
            {
                _context.Attach(sacrament);
                ;
                await UpdateSpeakers(sacrament, selectedSpeakers, SpeakerTopic);
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

        private async 
        Task
UpdateSpeakers(Sacrament sacrament, String[] selectedSpeakers, String[] SpeakerTopic)
        {
            GetMembersForDropdown();

            ICollection<Speakers> selectedSpeakersList = new List<Speakers>();
            ICollection<Speakers> allSpeakers = _context.Speakers
                .Include(s => s.Member).ToList();
            ICollection<SpeakerTopic> allTopics = _context.SpeakerTopic.ToList();

            //_context.Member.find
            if (selectedSpeakers[0] != null)
                for (int i = 0; i < selectedSpeakers.Length; i++)
                {
                    Member mem = await getMember(selectedSpeakers[i]);
                    SpeakerTopic top = getTopic(SpeakerTopic[i]);
                    if (mem != null)
                        selectedSpeakersList.Add(new Speakers { Sacrament = sacrament, Member = mem, Topic = top });

                    //Remove old speaker references.
                    foreach (var aSpeak in allSpeakers)
                    {
                        if (aSpeak != null && sacrament.Speakers != null && sacrament.Speakers.Contains(aSpeak))
                            sacrament.Speakers.Remove(aSpeak);
                    }
                    if (sacrament.Speakers == null)
                        sacrament.Speakers = new List<Speakers>();
                    //Add new speaker references (If Not Exist)
                    foreach (var speaker in selectedSpeakersList)
                    {
                        sacrament.Speakers.Add(speaker);
                    }
                    //_context.SaveChanges();
                }
        }

        private SpeakerTopic getTopic(string v)
        {
            if (v == null || v == "")
                return null;
            SpeakerTopic top;
            if (_context.SpeakerTopic.Any(T => T.Topic == v && v.Trim() != ""))
            {
                top = _context.SpeakerTopic.SingleOrDefault(T => T.Topic == v.Trim());
                //mem.Topic = top;
            }
            else
            {
                top = new SpeakerTopic();
                top.Topic = v.Trim();
                _context.Add(top);
                _context.SaveChanges();
                //mem.Topic = top;
            }
            return top;
        }

        private async Task<Member> getMember(string v)
        {
            if (v == null || v == "")
                return null;
            Member mem;
            var firstName = "";
            var lastName = "";
            if (v != null) //continue;
                if (v.Contains(","))
                {
                    var splitName = v.Split(",");
                    firstName = splitName[1].Trim();
                    lastName = splitName[0].Trim();
                }
                else
                {
                    firstName = v.Trim();
                }



            if (firstName == "")
            {
                return null;
            }
            if (_context.Member.Any(m => m.FirstMiddleName == firstName && m.LastName == lastName))
            {
                mem = _context.Member.SingleOrDefault(m => m.FirstMiddleName == firstName && m.LastName == lastName);
            }
            else
            {
                //Create Entity and add it to speakerList....
                var member = new Member();
                member.FirstMiddleName = firstName;
                member.LastName = lastName;
                member.BaptizeDate = DateTime.Now;
                _context.Add(member);
                mem = _context.Member.SingleOrDefault(m => m.FirstMiddleName == firstName && m.LastName == lastName);

            }
            _context.SaveChanges();
            return mem;
        }


        // GET: Sacraments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sacrament = await _context.Sacrament
                .Include(m => m.Speakers)
                    .ThenInclude(s => s.Member)
                .Include(m => m.Speakers)
                    .ThenInclude(s => s.Topic)
                .Include(s => s.Presiding).Include(S => S.Conducting)
                .Include(s => s.Invocation).Include(S => S.Benediction)
                .SingleOrDefaultAsync(m => m.Id == id)
                ;
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
