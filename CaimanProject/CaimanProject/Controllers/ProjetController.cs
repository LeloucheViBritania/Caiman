using AutoMapper;
using CaimanProject.DAL;
using CaimanProject.Models;
using CaimanProject.VM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaimanProject.Controllers
{
    public class ProjetController : Controller
    {

        // GET: Projet

        public ActionResult ProjetDetail(int id)
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
        }
    }
}