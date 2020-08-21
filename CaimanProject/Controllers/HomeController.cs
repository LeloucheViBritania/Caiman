using CaimanProject.DAL;
using CaimanProject.Models;
using CaimanProject.VM;
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
            return View();
        }

        public ActionResult NewMember()
        {
            var context = new DbCaimanContext();
            ViewBag.SpecialiteList  = context.Specialites.ToList();
            context.Dispose();
            return View();
        }

        [HttpPost]
        public ActionResult NewMember(SaveMemberViewModel model)
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

    }

}