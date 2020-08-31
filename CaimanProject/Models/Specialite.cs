using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaimanProject.Models
{
    public class Specialite
    {
        [Key]
       
        public int SpecialiteId { get; set; }

        public string SpecialiteName { get; set; }

        public string SpecialiteColor { get; set; }

        public string Url_Image { get; set; }
        public string ImageSpecialité { get; set; }
        //Recupere tous les membre d'une specialite

        public virtual ICollection<Member> Members { get; set; }
    }
}