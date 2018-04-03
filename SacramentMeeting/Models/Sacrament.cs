using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeeting.Models
{
    public class Sacrament
    {
        int Id { get; set; }
        DateTime date { get; set; }

        public virtual ICollection<Member> Speaker { get; set; }
    }

    public class Member
    {
        int Id { get; set; }
        String FirstMiddleName { get; set; }
        String LastName { get; set; }

        public virtual ICollection<MemberCalling> MemberCalling { get; set; }
    }
    public class MemberCalling
    {
        int Id { get; set; }
        int MemberId { get; set; }
        int CallingId { get; set; }
        bool Active { get; set; }

        public virtual Member Member { get; set; }
        public virtual Calling Calling { get; set; }
    }
    public class Calling
    {
        int Id { get; set; }
        String CallingName { get; set; }

        public virtual ICollection<MemberCalling> MemberCalling { get; set; }
    }
}
