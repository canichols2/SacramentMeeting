using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SacramentMeeting.Models;

namespace SacramentMeeting.Models
{
    public class SacramentContext : DbContext
    {
        public SacramentContext(DbContextOptions<SacramentContext> options) : base(options)
        {

        }
        public DbSet<Sacrament> Sacrament { get; set; }
        public DbSet<MemberCalling> MemberCalling { get; set; }
        public DbSet<Calling> Calling { get; set; }
        public DbSet<Member> Member{ get; set; }
        public DbSet<Speakers> Speakers { get; set; }
        public DbSet<SacramentMeeting.Models.SpeakerTopic> SpeakerTopic { get; set; }
        
    }
}
