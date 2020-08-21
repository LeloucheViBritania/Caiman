using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaimanProject.Models
{
    public class NoteP
    {
        [Key]
        public int NotePId { get; set; }

        public string NotePDescription { get; set; }

        public DateTime NotePDate { get; set; }

        //recupere le projet dont la note est appliquee
        public Projet Projet { get; set; }
    }
}