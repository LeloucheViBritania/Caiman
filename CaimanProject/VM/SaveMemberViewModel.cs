using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaimanProject.VM
{
    public class SaveMemberViewModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }

        public string MemberPnom { get; set; }

        public string MemberSex { get; set; }

        public string MemberDescription { get; set; }

        public DateTime MemberNaissance { get; set; }

        public string MemberLieuNaissance { get; set; }

        public string MemberMail { get; set; }

        public int MemberPhone { get; set; }

        public string MemberStatus { get; set; }

        public string MemberCommune { get; set; }

        public string MemberQuartier { get; set; }

        [Required(ErrorMessage = "Entrer le moyen de transport")]
        public int TranportId { get; set; }

        [Required(ErrorMessage = "Entrer la specialite")]
        public int SpecialiteId { get; set; }


        //atttribute

        public string TranportName { get; set; }

        public string SpecialiteName { get; set; }

    }
}