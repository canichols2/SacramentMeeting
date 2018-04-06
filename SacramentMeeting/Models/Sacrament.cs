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

        public int? PresidingId { get; set; }
        public int? ConductingId { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Required]
        [Range(0, 341)]
        [Display(Name ="Opening Song")]
        public int OpeningSong { get; set; }

        [Required]
        [Range(0, 341)]
        [Display(Name ="Sacrament Song")]
        public int SacramentSong { get; set; }

        [Range(0, 341)]
        [Display(Name = "Intermediate Song")]
        public int? IntermediateSong { get; set; }

        [Required]
        [Range(0, 341)]
        [Display(Name ="Closing Song")]
        public int ClosingSong { get; set; }

        [Display(Name ="Presiding")]
        public Member Presiding { get; set; }

        [Display(Name = "Conducting")]
        public Member Conducting { get; set; }
        public virtual ICollection<Speakers> Speakers { get; set; }
    }


    public class Speakers
    {
        public int Id { get; set; }
        public int SacramentID { get; set; }
        public int MemberID { get; set; }
        public int? TopicID { get; set; }
        public Sacrament Sacrament { get; set; }
        public Member Member { get; set; }

        [Display(Name ="Speaker Topic")]
        public SpeakerTopic Topic { get; set; }
    }

    public class SpeakerTopic
    {
        public int Id { get; set; }

        [Display(Name = "Topic")]
        public String Topic { get; set; }
        ICollection<Speakers> Speakers { get; set; }
    }


    public class Member
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public String FirstMiddleName { get; set; }

        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Baptize Date")]
        public DateTime BaptizeDate { get; set; }

        public virtual ICollection<MemberCalling> MemberCalling { get; set; }

        [Display(Name = "Full Name")]
        public String FullName
        {
            get
            {
                if(LastName != "")
                    return LastName + ", " + FirstMiddleName;
                return FirstMiddleName;
            }
        }
    }
    public class MemberCalling
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int? CallingId { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        public virtual Member Member { get; set; }
        public virtual Calling Calling { get; set; }
    }
    public class Calling
    {
        public int Id { get; set; }

        [Display(Name = "Calling Name")]
        public String CallingName { get; set; }

        public virtual ICollection<MemberCalling> MemberCalling { get; set; }
    }
}
