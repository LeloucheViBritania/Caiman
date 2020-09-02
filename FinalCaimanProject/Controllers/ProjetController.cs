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
    [Authorize]
    public class ProjetController : Controller
    {
        // GET: Projet

        public ActionResult ProjetDetail(int id)
        {
            var _context = new DbCaimanContext();
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ProDetailVm proDetailVm = new ProDetailVm();

            var projetDetail = _context.Projets.Include(c => c.ProjetMembers)
                                                    .ThenInclude(m => m.Member)
                                                .Include(no => no.NotePs)
                                                .SingleOrDefault(c => c.ProjetId == id);

            ViewBag.Specialites = _context.Specialites.ToList();
            var Members = _context.Members.ToList();
            var projetDe = Mapper.Map<Projet, ProjetDetailDTO>(projetDetail);
            ProjetDetailDTO projetDetailDTO = new ProjetDetailDTO();
            projetDetailDTO = projetDe;


            List<Member> MemberNoINPro = new List<Member>();
            foreach (var item in projetDe.MembersDTOs)
            {

                Member monMApp = Mapper.Map<MembersDTO, Member>(item);
                MemberNoINPro.Add(monMApp);

            }

            List<int> tempIdList = MemberNoINPro.Select(q => q.MemberId).ToList();
            var temp = _context.Members.Where(q => !tempIdList.Contains(q.MemberId));


            /* var meminNO = _context.Members.Intersect(MemberNoINPro);*/
            proDetailVm.projetDetailDTO = projetDetailDTO;
            proDetailVm.Members = temp;
            return View(proDetailVm);
        }

        private Projet GetProDetails(int? idPro)
        {
            var _context = new DbCaimanContext();
            var projet = _context.Projets
                .Where(pro => pro.ProjetId == idPro)
                .Include(no => no.NotePs
                    .OrderByDescending(noId => noId.NotePId)
                    .Take(2))
                .Include(mPro => mPro.ProjetMembers)
                    .ThenInclude(mem => mem.Member)
                        .ThenInclude(sp => sp.Specialite)
                .SingleOrDefault();
            return projet;
        }


        [HttpPost]
        public ActionResult ProjetDetail(int id, FormCollection formCollectionMember, string ProjetProgressBar)
        {

            var _context = new DbCaimanContext();
            ProDetailVm proDetailVm = new ProDetailVm();

            var projetDetail = _context.Projets.Include(c => c.ProjetMembers)
                                                    .ThenInclude(m => m.Member)
                                                .Include(no => no.NotePs)
                                                .SingleOrDefault(c => c.ProjetId == id);

            ViewBag.Specialites = _context.Specialites.ToList();
            var Members = _context.Members.ToList();
            var projetDe = Mapper.Map<Projet, ProjetDetailDTO>(projetDetail);
            ProjetDetailDTO projetDetailDTO = new ProjetDetailDTO();
            projetDetailDTO = projetDe;


            List<Member> MemberNoINPro = new List<Member>();
            foreach (var item in projetDe.MembersDTOs)
            {

                Member monMApp = Mapper.Map<MembersDTO, Member>(item);
                MemberNoINPro.Add(monMApp);

            }

            List<int> tempIdList = MemberNoINPro.Select(q => q.MemberId).ToList();
            var temp = _context.Members.Where(q => !tempIdList.Contains(q.MemberId));

            proDetailVm.projetDetailDTO = projetDetailDTO;
            proDetailVm.Members = temp;

            /* var meminNO = _context.Members.Intersect(MemberNoINPro);*/

            /* _context.Dispose();*/

            List<string> lisMembers = new List<string>();
            List<Member> memberSelect = new List<Member>();

            if (formCollectionMember != null && id != 0)
            {
                if (ProjetProgressBar == null)
                {
                    for (int i = 0; i < formCollectionMember.Count; i++)
                    {
                        lisMembers.Add(formCollectionMember[i]);
                    }
                    foreach (var item in lisMembers)
                    {
                        Member addMem = new Member();
                        addMem = _context.Members.FirstOrDefault(c => c.MemberId == int.Parse(item));
                        addMem.IsChecked = false;
                        memberSelect.Add(addMem);
                    }
                }
                var contextNoTrack = new DbCaimanContext();
                contextNoTrack.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                var listMemberIsSelected = contextNoTrack.Projets.FirstOrDefault(c => c.ProjetId == id);
                if (ProjetProgressBar != null)
                {
                    int progress = int.Parse(ProjetProgressBar);
                    if (listMemberIsSelected.ProjetProgressBar < progress && progress < 97)
                        listMemberIsSelected.ProjetProgressBar = progress;
                    contextNoTrack.Projets.Update(listMemberIsSelected);
                }
                   
                listMemberIsSelected.ProjetMembers = new List<ProjetMember>();
                if (memberSelect!=null)
                {
                    foreach (var mem in memberSelect)
                    {
                        var member = new Member { MemberId = mem.MemberId };


                        contextNoTrack.Members.Attach(member);
                        var projetMember = new ProjetMember
                        {
                            Member = member
                        };

                        listMemberIsSelected.ProjetMembers.Add(projetMember);
                    }
                }
                
                contextNoTrack.Projets.Update(listMemberIsSelected);
                contextNoTrack.SaveChanges();
                return RedirectToAction("ProjetDetail", "Projet", new { id = id });
            }


            return View(proDetailVm);

        }

        public ActionResult Directive(int id)
        {
            var _context = new DbCaimanContext();
            var proDirective = _context.Projets.FirstOrDefault(p => p.ProjetId == id);
            
            if (proDirective != null)
            {
                var mapProjetDirect = Mapper.Map<Projet, ProjetDirectiveDTO>(proDirective);
                return View(mapProjetDirect);
            }
            else
                return RedirectToAction("Index", "NotFound");
        }
    }
}