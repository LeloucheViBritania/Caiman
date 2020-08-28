﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaimanProject.VM
{
    public class NoteAddProDetailDTO
    {
        public int ProjetId { get; set; }

        public string ProjetName { get; set; }
        public string ProjetDescription { get; set; }

        public IList<NotePDTO> NotePDTOs { get; set; }

    }
}