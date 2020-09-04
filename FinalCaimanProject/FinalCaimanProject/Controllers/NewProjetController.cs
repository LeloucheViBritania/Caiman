using FinalCaimanProject.DAL;
using FinalCaimanProject.Models;
using FinalCaimanProject.VM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalCaimanProject.Controllers
{
    [Authorize]
    public class NewProjetController : Controller
    {
        public ActionResult Index()
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
                    MemberImageName = c.MemberImageName,
                    IsChecked = false
                }).ToList(),
                Specialites = _context.Specialites.ToList()
            };
            return View(viewModel);
        }

        /* public ActionResult ProjetDetail(int id)
         {
             var _context = new DbCaimanContext();


             var projetDetail = _context.Projets.Include(c => c.ProjetMembers)
                                                     .ThenInclude(m => m.Member)
                                                 .Include(no => no.NotePs)
                                                 .SingleOrDefault(c => c.ProjetId == id);


             var projetDe = Mapper.Map<Projet, ProjetDetailDTO>(projetDetail);
             ProjetDetailDTO projetDetailDTO = new ProjetDetailDTO();
             projetDetailDTO = projetDe;
             return View(projetDetailDTO);
         }*/

        //Post d'un nouveau projet 
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

            List<string> lisMembers = new List<string>();
            List<Member> memberSelect = new List<Member>();
            
                
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
                 _context.SaveChanges();

            var file = Request.Files[0];
            if (file != null)
            {
                string projetCahier = "";
                var fileName = Path.GetFileName(file.FileName); //Récupération du nom du fichier;
                var ext = Path.GetExtension(fileName);
                if (ext == ".pdf" || ext == ".PDF" || ext == ".docx")
                {
                    var path = Path.Combine(Server.MapPath("/Fichier"), fileName);//Enregistrement du fichier dans le dossier Fichier
                    file.SaveAs(path);
                     projetCahier = fileName;
                }
                else
                {
                    ViewData["Error"] = "Le cahier de charge doit etre un fichier PDF ou WORD";
                    return View(viewModel);

                }
                
                //une fois le model est valide

                if (ModelState.IsValid)
                {

                    var addProjetMembers = new Projet
                    {
                        ProjetCahierCharge = projetCahier,
                        ProjetName = fromEnnui["ProjetName"],
                        ProjetDateDebut = DateTime.Now,
                        ProjetDescription = fromEnnui["ProjetDescription"],
                        ProjetMembers = new List<ProjetMember>()
                    };


                    var contextNoTrack = new DbCaimanContext();
                    contextNoTrack.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                    var listMemberIsSelected = contextNoTrack.Members.Where(c => c.IsChecked == true).ToList();

                    foreach (var selectMember in listMemberIsSelected)
                    {
                        var member = new Member
                        {
                            MemberId = selectMember.MemberId

                        };

                       var ds =  _context.Members.SingleOrDefault(s => s.MemberId == member.MemberId);
                       var up =  _context.Members.Find(ds.MemberId);
                        up.MemberMissionActive += 1;
                        _context.Members.Update(up);

                         _context.SaveChanges();
                        contextNoTrack.Members.Attach(member);
                        var projetMember = new ProjetMember
                        {
                            Member = member
                        };
                      addProjetMembers.ProjetMembers.Add(projetMember);
                    }
                   
                    contextNoTrack.Projets.Add(addProjetMembers);
                    contextNoTrack.SaveChanges();
                    contextNoTrack.Dispose();


                    //remet tout les memebres a false 


                    foreach (var item in lisMembers)
                    {
                        Member addMem = new Member();
                        addMem = _context.Members.FirstOrDefault(c => c.MemberId == int.Parse(item));
                        addMem.IsChecked = false;
                        memberSelect.Add(addMem);
                        _context.Members.UpdateRange(memberSelect);
                    }
                    _context.SaveChanges();

                    ViewBag.Message = "Succès";

                    RedirectToAction("Index", "Home");

                }
            }
            return View(viewModel);
        }


    }
}