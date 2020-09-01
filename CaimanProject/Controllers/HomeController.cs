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

            ViewBag.Alls = allProjetMembers.Where(c => c.IsArchieved == false); ;
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






        public ActionResult FinishProjet(int id)
        {
            var _context = new DbCaimanContext();
            Projet projetF = _context.Projets.FirstOrDefault(proId => proId.ProjetId == id);
            return View(projetF);
        }



        [HttpPost]


        public ActionResult FinishProjet(Projet proFUp, int id)
        {
         
            var _contextNoTrack = new DbCaimanContext();

            var projetF = _contextNoTrack.Projets.Include(c => c.ProjetMembers)
                                                     .ThenInclude(m => m.Member).AsNoTracking()
                                                 .SingleOrDefault(c => c.ProjetId == id);
            if (ModelState.IsValid)
            {
                projetF.IsArchieved = true;
                projetF.ProjetDateFin = DateTime.Now;
                projetF.ProjetMoney = proFUp.ProjetMoney;
                projetF.ProjetProgressBar = 100;
                var _con = new DbCaimanContext();
                List<int> tempIdList = projetF.ProjetMembers.Select(q => q.MemberId).ToList();
                var temp = _con.Members.Where(q => !tempIdList.Contains(q.MemberId)).AsNoTracking();
                _contextNoTrack.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                foreach (var memProjet in temp)
                {
                    Member memUp = new Member
                    {
                        MemberId = memProjet.MemberId,
                        MemberMissionActive = memProjet.MemberMissionActive--,
                        MemberMissonFin = memProjet.MemberMissonFin++
                    };
                    var proMP = new ProjetMember
                    {
                        Member = memUp
                    };
                   projetF.ProjetMembers.Add(proMP);
                }
                _con.Projets.Update(projetF);
                _con.SaveChanges();

                return RedirectToAction("ArchiveAllProjet", "Home");
            }
            return View(projetF);
        }



        public ActionResult ArchiveAllProjet(int page = 0)
        {
                var _context = new DbCaimanContext();
                /*var allProjet = GetAllProjets();*/
                var VmAllP = new AllProjets();

                var ctx = _context.Projets.Include(MP => MP.ProjetMembers)
                    .ThenInclude(mem => mem.Member).SingleOrDefault(c => c.ProjetId == 1);
                var ctx1 = Mapper.DynamicMap<Projet, ProjetsDTO>(ctx);

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
                var bedroom = allProjetMembers.Where(c => c.IsArchieved == true);
                const int PageSize = 6; // you can always do something more elegant to set this

                var count = bedroom.Count();

                var data = bedroom.Skip(page * PageSize).Take(PageSize).OrderByDescending(s => s.ProjetId).ToList();

                ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);

                ViewBag.Page = page;
                ViewBag.Alls = data;
        
            VmAllP.Specialites = GetSpecilites();
                return View(VmAllP);

        }
       


        public ActionResult OldMembers()
        {
            var _context = new DbCaimanContext();
            var oldMember = _context.Members.Where(m => m.MemberIsArchived == true).ToList();
            var oldVM = new OldVm();
            oldVM.OldMembers = oldMember;
            return View(oldVM);
        }

    }

}