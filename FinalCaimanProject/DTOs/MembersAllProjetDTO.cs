using FinalCaimanProject.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalCaimanProject.DTOs
{
    public class MembersAllProjetDTO
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberPnom { get; set; }
        public int MemberNote { get; set; }
        public IList<ProjetsDTO> Alls { get; set; }
    }
}