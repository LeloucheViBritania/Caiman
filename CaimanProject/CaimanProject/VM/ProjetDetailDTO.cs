﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaimanProject.VM
{
    public class ProjetDetailDTO
    {
        public int ProjetId { get; set; }

        public string ProjetName { get; set; }

        public bool IsArchieved { get; set; }

        public DateTime ProjetDateDebut { get; set; }
        public string ProjetDescription { get; set; }

        public int ProjetProgressBar { get; set; }
        public IList<MembersDTO> MembersDTOs { get; set; }

        public IList<NotePDTO> NotePDTOs { get; set; }
    }
}