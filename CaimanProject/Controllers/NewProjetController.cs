using CaimanProject.DAL;
using CaimanProject.Models;
using CaimanProject.VM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaimanProject.Controllers
{
    public class NewProjetController : Controller
    {
        public bool IsChecked { get; private set; }

       
        // GET: NewProjet


        public ActionResult Index()
        {
            var _context = new DbCaimanContext();
            var viewModel = new ProVm
            {
                Members = _context.Members.Select(c=> new NewMemberVM { 
                MemberId = c.MemberId,
                MemberName = c.MemberName,
                MemberMissionActive =c.MemberMissionActive,
                SpecialiteId = c.SpecialiteId,
                IsChecked = false
                }).ToList(),
                Specialites = _context.Specialites.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(FormCollection fromEnnui)
        {
            var _context = new DbCaimanContext();
            var viewModel = new ProVm
            {
                Members = _context.Members.Select(c => new NewMemberVM
                {
                    MemberId = c.MemberId,
                    MemberName = c.MemberName,
                    MemberMissionActive = c.MemberMissionActive,
                    SpecialiteId = c.SpecialiteId,
                    IsChecked = false
                }).ToList(),
                Specialites = _context.Specialites.ToList(),
                memerBySpe = _context.Members.ToList()
            };
            List<string>lisMembers = new List<string>();
            List <Member> memberSelect= new List<Member>();
            var file = Request.Files["ProjetCahierCharge"];
            if (file != null)
            {
             
                string projetCahier = file.FileName;
                for (int i = 0; i < fromEnnui.Count; i++)
                {
                    if (i >= 2)
                        lisMembers.Add(fromEnnui[i]);
                }

                foreach (var item in lisMembers)
                {
                    Member addMem = new Member();
                    addMem = _context.Members.FirstOrDefault(c => c.MemberId == int.Parse(item));
                    addMem.IsChecked = true;
                    memberSelect.Add(addMem);
                    
                }
                _context.Members.UpdateRange(memberSelect);
            }



            if (ModelState.IsValid)
            {
                var addProjetMembers = new Projet
                {

                    ProjetName = fromEnnui["ProjeName"],
                    ProjetDateDebut = DateTime.Now,
                    ProjetDescription = fromEnnui["ProjetDescripion"],
                    MemberProjets = new List<MembersProjet>()
                };
                var noEntitContext = new DbCaimanContext();
                foreach (var selectMember in _context.Members.Where(c => c.IsChecked == true).ToList())
                {
                    var member = new Member { MemberId = selectMember.MemberId };
                    noEntitContext.Members.Attach(member);
                    var projetMember = new MembersProjet
                    {
                        Member = member
                    };

                    addProjetMembers.MemberProjets.Add(projetMember);
                }
                noEntitContext.Projets.Add(addProjetMembers);
                noEntitContext.SaveChanges();
                RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }
    }
}