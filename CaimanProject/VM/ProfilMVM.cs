using CaimanProject.DTOs;
using CaimanProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaimanProject.VM
{
    public class ProfilMVM
    {
        public ProfilMemberDTO ProfilMemberDTO { get; set; }

        public List<Specialite> Specialites { get; set; }
        public List<Transport> Transports { get; set; }
    }
}