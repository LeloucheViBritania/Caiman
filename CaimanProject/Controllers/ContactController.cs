using CaimanProject.Models;
using CaimanProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.EntityFrameworkCore;

namespace CaimanProject.Controllers
{
    public class ContactController : Controller
    {
        DbCaimanContext db = new DbCaimanContext();
        // GET: Contact
        //Pagination 
        public ActionResult Contact(int page = 0)
        {

            var bedroom = from s in db.Contacts select s;
            const int PageSize = 6; // you can always do something more elegant to set this

            var count = bedroom.Count();

            var data = bedroom.Skip(page * PageSize).Take(PageSize).OrderByDescending(s => s.ContactId).ToList();

            ViewBag.MaxPage = (count / PageSize) - (count % PageSize == 0 ? 1 : 0);

            ViewBag.Page = page;
            ViewBag.Contact = data;
            return View();
        }

        // recherche par nom , prenom , specialite
        [HttpPost]
        public ActionResult Contact(string Searching)
        {
            var bg = from s in db.Contacts select s;
            if (Searching != null)
            {
                bg = bg.Where(s => s.ContactName.Contains(Searching) || s.ContactPname.Contains(Searching) || s.ContactFonction.Contains(Searching) || s.ContactSite.Contains(Searching));
                ViewBag.Contact = bg.ToList();

            }
            return View();
        }

        public ActionResult ModifContact(int id)
        {
            var contact = db.Contacts.Find(id);
            return View(contact);
        }


        // Modification des contacts
        [HttpPost]
        public ActionResult ModifContact(Contact contact, int id)
        {
            var bd = db.Contacts.Find(id);
            if (contact != null)
            {
                bd.ContactEmail = contact.ContactEmail;
                bd.ContactFonction = contact.ContactFonction;
                bd.ContactName = contact.ContactName;
                bd.ContactNumber = contact.ContactNumber;
                bd.ContactPname = contact.ContactPname;
                bd.ContactSite = contact.ContactSite;

                db.Contacts.Update(bd);
                db.SaveChanges();
            }
            return View();
        }

        private List<Contact> GetContact()
        {
            var contact = db.Contacts.ToList();
            return contact;
        }

        // vue des contacts
        public ActionResult NewContact()
        {
            return View();
        }
        //Ajout de contact
        [HttpPost]
        public ActionResult NewContact(Contact contact)
        {

            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return View();
            }
            else
            {
                return Content("Vos information sont pas totalement complètes");
            }
        }
    }
}