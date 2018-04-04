using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeeting.Models
{
    public class Sacrament
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Required]
        public int OpeningSong { get; set; }

        [Required]
        public int SacramentSong { get; set; }

        public int IntermediateSong { get; set; }

        [Required]
        public int ClosingSong { get; set; }
        


        public virtual ICollection<Speakers> Speakers { get; set; }
    }


    public class Speakers
    {
        //public Speakers(int SacramentID, int MemberID)
        //{
        //    this.SacramentID = SacramentID;
        //    this.MemberID = MemberID;
        //}
        //public Speakers(Sacrament SacramentID, Member MemberID)
        //{
        //    this.Sacrament = SacramentID;
        //    this.Member = MemberID;
        //}

        public int Id { get; set; }
        public int SacramentID { get; set; }
        public int MemberID { get; set; }
        public Sacrament Sacrament { get; set; }
        public Member Member { get; set; }
    }



    public class Member
    {
        public int Id { get; set; }
        public String FirstMiddleName { get; set; }
        public String LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BaptizeDate { get; set; }

        public virtual ICollection<MemberCalling> MemberCalling { get; set; }
    }
    public class MemberCalling
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int CallingId { get; set; }
        public bool Active { get; set; }

        public virtual Member Member { get; set; }
        public virtual Calling Calling { get; set; }
    }
    public class Calling
    {
        public int Id { get; set; }
        public String CallingName { get; set; }

        public virtual ICollection<MemberCalling> MemberCalling { get; set; }
    }
}
