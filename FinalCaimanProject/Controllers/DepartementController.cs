using AutoMapper;
using FinalCaimanProject.DAL;
using FinalCaimanProject.DTOs;
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
    public class DepartementController : Controller
    {
        DbCaimanContext db = new DbCaimanContext();
        // GET: Departement
        public ActionResult Departement()
        {
            var bd = db.Specialites.ToList();
            ViewBag.Spe = db.Specialites.ToList();
            return View();
        }
        public ActionResult Ajouter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ajouter(Specialite specialite)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)//Vérifie que le fichier existe
                    {
                        var fileName = Path.GetFileName(file.FileName); //Récupération du nom du fichier
                        specialite.Url_Image = "/Fichier";
                        var path = Path.Combine(Server.MapPath(specialite.Url_Image), fileName);//Enregistrement du fichier dans le dossier Fichier
                        file.SaveAs(path);

                        specialite.ImageSpecialite = fileName;

                    }
                }

                
                db.Specialites.Add(specialite);
                db.SaveChanges();
            }
            return RedirectToAction("Departement");
        }

        public ActionResult VueDepartement(int id)
        {

            var bd = db.Specialites.Find(id);
            ViewBag.Membern = db.Members.Where(s => s.SpecialiteId == bd.SpecialiteId);
            ViewBag.Respo = db.Members.Where(s => s.MemberStatus == "Chef de groupe" && s.SpecialiteId == bd.SpecialiteId).OrderByDescending(x => x.MemberId);
            ViewBag.Adj = db.Members.Where(s => s.MemberStatus != "Chef de groupe" && s.SpecialiteId == bd.SpecialiteId);
            return View(bd);
        }


        [HttpPost]
        public ActionResult VueDepartement(Member member, int id)
        {

            var bd = from s in db.Members select s;
            foreach (var item in bd)
            {
                //fait le verification s'ils sont identiques
                if (member.MemberId == item.MemberId)
                {
                    if (member.MemberStatus == "Chef de groupe" && member.MemberStatus == "Membre simple")
                    {
                        item.MemberStatus = "Adjoint";
                    }
                    else if (member.MemberStatus != "Chef de groupe" && member.MemberStatus != "Membre simple")
                    {
                        item.MemberStatus = "Chef de groupe";
                    }
                    else
                    {
                        item.MemberStatus = "Adjoint";
                    }
                    member = item;

                }

            }
            db.Members.Update(member);
            db.SaveChanges();

            return RedirectToAction("VueDepartement");
        }

        public ActionResult ProfilMember(int id)
        {
            var _context = new DbCaimanContext();
            var memberDtail = _context.Members
                .Include(mem => mem.Competences)
                .Include(mem => mem.SocialNetworks)
                .FirstOrDefault(m => m.MemberId == id);
            var memSpecialite = _context.Specialites.ToList();
            var membTrans = _context.Transports.ToList();

            var memberWithAllDetails = Mapper.Map<Member, ProfilMemberDTO>(memberDtail);


            ProfilMVM profilMVM = new ProfilMVM();
            profilMVM.ProfilMemberDTO = memberWithAllDetails;
            profilMVM.Specialites = memSpecialite;
            profilMVM.Transports = membTrans;
            return View(profilMVM);
        }

        [HttpPost]
        public ActionResult ProfilMember(Member member, Competence competence, SocialNetwork socialNetwork, int id)
        {
            var _context = new DbCaimanContext();
            var memberDtail = _context.Members
                .Include(mem => mem.Competences)
                .Include(mem => mem.SocialNetworks)
                .FirstOrDefault(m => m.MemberId == id);
            var memSpecialite = _context.Specialites.ToList();
            var membTrans = _context.Transports.ToList();

            var memberWithAllDetails = Mapper.Map<Member, ProfilMemberDTO>(memberDtail);


            ProfilMVM profilMVM = new ProfilMVM();
            profilMVM.ProfilMemberDTO = memberWithAllDetails;
            profilMVM.Specialites = memSpecialite;
            profilMVM.Transports = membTrans;

            var bd = db.Members.Find(id);

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)//Vérifie que le fichier existe
                {
                    var fileName = Path.GetFileName(file.FileName); //Récupération du nom du fichier
                    var path = Path.Combine(Server.MapPath("/Fichier"), fileName);//Enregistrement du fichier dans le dossier Fichier
                    member.MemberImageName = fileName;
                    file.SaveAs(path);
                    bd.MemberImageName = fileName;
                    db.Members.Update(bd);
                    db.SaveChanges();
                }
            }
            else if (member.MemberPnom != null)
            {
                bd.MemberName = member.MemberName;
                bd.MemberPnom = member.MemberPnom;
                bd.MemberCommune = member.MemberCommune;
                bd.MemberDescription = member.MemberDescription;
                bd.MemberMail = member.MemberMail;
                bd.MemberQuartier = member.MemberQuartier;
                bd.MemberCommune = member.MemberCommune;
                bd.MemberPhone = member.MemberPhone;
                bd.MemberStatus = member.MemberStatus;
                db.Members.Update(bd);
                db.SaveChanges();
            }
            else if ( member.MemberNote != 0)
            {
                bd.MemberNote = member.MemberNote;
                db.Members.Update(bd);
                db.SaveChanges();

            }
            else
            {
                bd.MemberIsArchived = member.MemberIsArchived;
                bd.MemberDateArchive = DateTime.Now;
                db.Members.Update(bd);
                db.SaveChanges();
            }

            if (competence.CompetenceName != null)
            {
                var _con = new DbCaimanContext();


                Member memberAdd = new Member();
                memberAdd = _context.Members.FirstOrDefault(c => c.MemberId == id);
                memberAdd.Competences = new List<Competence>();
                Competence addCompetence = new Competence();
                addCompetence.CompetenceName = competence.CompetenceName;
                memberAdd.Competences.Add(addCompetence);
                _con.Members.Update(memberAdd);
                _con.SaveChanges();
                _con.Dispose();
                /* _Transport.Members = new List<Member>();
                 if (_Specialite != null)
                 {
                     _Specialite.Members.Add(mem);
                     _context.Specialites.Update(_Specialite);
                 }
                 _Transport.Members.Add(mem);

                 _context.Transports.Add(_Transport);
                 _context.SaveChanges();
                 _context.Dispose();*/
            }

            if (socialNetwork.NetworkName != null)
            {
                var _con = new DbCaimanContext();
                Member memberAdd = new Member();
                memberAdd = _context.Members.FirstOrDefault(c => c.MemberId == id);
                memberAdd.SocialNetworks = new List<SocialNetwork>();
                SocialNetwork socialNetwork1 = new SocialNetwork();
                socialNetwork1.NetworkName = socialNetwork.NetworkName;
                socialNetwork1.NetworkLink = socialNetwork.NetworkLink;
                memberAdd.SocialNetworks.Add(socialNetwork1);
                _con.Members.Add(memberAdd);
                _con.SaveChanges();


                /*   db.SocialNetworks.Add(socialNetwork);
                   db.SaveChanges();*/
            }
            return RedirectToAction("ProfilMember", profilMVM);
        }
    }
}