using AutoMapper;
using AutoMapper.QueryableExtensions;
using CaimanProject.DAL;
using CaimanProject.Models;
using CaimanProject.VM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace CaimanProject.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var _context = new DbCaimanContext();
            /*var allProjet = GetAllProjets();*/
            var VmAllP = new AllProjets();

            /*            var ctx = _context.Projets.Include(MP => MP.ProjetMembers)
                            .ThenInclude(mem => mem.Member).SingleOrDefault(c=>c.ProjetId == 1);*/
            /*            var ctx1 = Mapper.DynamicMap<Projet, ProjetsDTO>(ctx);
            */
            var idallPr = _context.Projets.ToList();

            List<ProjetsDTO> allProjetMembers = new List<ProjetsDTO>();
            foreach (var item in idallPr)
            {
                var ctx2 = _context.Projets.Include(MP => MP.ProjetMembers)
               .ThenInclude(mem => mem.Member).FirstOrDefault(c => c.ProjetId == item.ProjetId);
                if (ctx2 != null)
                {
                    ProjetsDTO monMApp = Mapper.Map<Projet, ProjetsDTO>(ctx2);
                    allProjetMembers.Add(monMApp);
                }
                
            }

            ViewBag.Alls = allProjetMembers;
            VmAllP.Specialites = GetSpecilites();
            
            return View(VmAllP);

        }


        private List<Specialite> GetSpecilites()
        {
            var _context = new DbCaimanContext();
            return _context.Specialites.ToList();
        }

        public ActionResult NewMember()
        {
            var _context = new DbCaimanContext();
            ViewBag.SpecialiteList  = _context.Specialites.ToList();
            _context.Dispose();
            return View();
        }

        //Get Projets 




        [HttpPost]
        public ActionResult NewMember(SaveMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _context = new DbCaimanContext();
                List<Specialite> List = _context.Specialites.ToList();
                ViewBag.SpecialiteList = List;
                Specialite _Specialite = _context.Specialites.FirstOrDefault(Sp => Sp.SpecialiteName == model.SpecialiteName);
                _Specialite.Members = new List<Member>();
                var mem = new Member();
                    mem.MemberCommune = model.MemberCommune;
                    mem.MemberName = model.MemberName;
                    mem.MemberPnom = model.MemberPnom;
                    mem.MemberDescription = model.MemberDescription;
                    mem.MemberLieuNaissance = model.MemberLieuNaissance;
                    mem.MemberNaissance = model.MemberNaissance;
                    mem.MemberPhone = model.MemberPhone;

                    mem.MemberQuartier = model.MemberQuartier;
                    mem.MemberMail = model.MemberMail;
                    mem.MemberSex = model.MemberSex;
                Transport _Transport = new Transport{ TranportName = model.TranportName };
                 _Transport.Members = new List<Member>();

                if (_Specialite != null)
                {
                    _Specialite.Members.Add(mem);
                    _context.Specialites.Update(_Specialite);
                }
                 _Transport.Members.Add(mem);
           
                _context.Transports.Add(_Transport);
                _context.SaveChanges();
                _context.Dispose();

                return View();
            }
            return View(model);
        }

    }

}