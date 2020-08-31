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
    public class NotesController : Controller
    {
        // GET: Notes
        [HttpGet]
        public ActionResult NotesOnProjet(int id)
        {

            var _context = new DbCaimanContext();
            var AllNotesForPro = _context.Projets.Include(no => no.NotePs)
                                                 .SingleOrDefault(c => c.ProjetId == id);
            var NotePro = Mapper.Map<Projet, NoteAddProDetailDTO>(AllNotesForPro);
            NoteAddProDetailDTO NoteDTO = new NoteAddProDetailDTO();
            NoteDTO = NotePro;
            return View(NoteDTO);
        }


        public ActionResult AddNote(int id)
        {
            var _context = new DbCaimanContext();

            var bd = _context.Projets.Include(Projet => Projet.NotePs)
                                                .SingleOrDefault(c => c.ProjetId == id);
            if (bd != null)
            {
                var NotePro = Mapper.Map<Projet, NoteAddProDetailDTO>(bd);
                NoteAddProDetailDTO NoteDTO = new NoteAddProDetailDTO();
                NoteDTO = NotePro;
                return View(NoteDTO);
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddNote(NoteP note, int id)
        {
            if (ModelState.IsValid)
            {
                var _context = new DbCaimanContext();

                Projet projetAdd = new Projet();
                projetAdd = _context.Projets.FirstOrDefault(c => c.ProjetId == id);

                projetAdd.NotePs = new List<NoteP>();

                NoteP notepAdd = new NoteP();
                notepAdd.NotePDate = DateTime.Now;
                notepAdd.NotePDescription = note.NotePDescription;

                projetAdd.NotePs.Add(notepAdd);

                _context.Projets.Update(projetAdd);
                _context.SaveChanges();

                return RedirectToAction("ProjetDetail", "Projet", new { id = id });
            }
            return View();
        }

        public ActionResult DetailsNote(int id)
        {
            var _context = new DbCaimanContext();
            var note = _context.NotePs.Include(c => c.Projet).FirstOrDefault(no => no.NotePId == id);

            return View(note);
        }
    }
}