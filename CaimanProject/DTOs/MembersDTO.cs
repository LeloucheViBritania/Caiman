using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaimanProject.VM
{
    public class MembersDTO
    {
        public int MemberId { get; set; }

        public string MemberName { get; set; }

        public string MemberImageName { get; set; }

        public int SpecialiteId { get; set; }

        public string SpecialiteColor { get; set; }
    }
}