using CaimanProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaimanProject.VM
{
    public class NewMemberVM
    {
        public int MemberId { get; set; }

        public int MemberMissionActive { get; set; }
        public int SpecialiteId { get; set; }
        public string MemberName { get; set; }
        public bool IsChecked { get; set; }
 
    }
}