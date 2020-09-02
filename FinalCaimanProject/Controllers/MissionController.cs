using AutoMapper;
using FinalCaimanProject.DAL;
using FinalCaimanProject.DTOs;
using FinalCaimanProject.Models;
using FinalCaimanProject.VM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalCaimanProject.Controllers
{
    public class MissionController : Controller
    {
        // GET: Mission
        public ActionResult MissionTerminee(int id)
        {
            var _context = new DbCaimanContext();
           
            var VmAllP = new AllProjets();
            var idallPr = _context.Projets.ToList();

            List<MembersAllProjetDTO> allProjetMembers = new List<MembersAllProjetDTO>();
            var ctx2 = _context.Members.Include(pm => pm.ProjetMembers).ThenInclude(pr => pr.Projet).ThenInclude(pm => pm.ProjetMembers).ThenInclude(mem => mem.Member).SingleOrDefault(c => c.MemberId == id);
              
                if (ctx2 != null)
                {
                   /* ProjetsDTO monMApp = Mapper.Map<Member, MembersAllProjetDTO>(ctx2);
                    allProjetMembers.Add(monMApp);*/
                }

           
            VmAllP.Specialites = GetSpecilites();

            return View(VmAllP);
        }

        private List<Specialite> GetSpecilites()
        {
            var _context = new DbCaimanContext();
            return _context.Specialites.ToList();
        }
    }
}