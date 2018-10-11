using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class PersonController : Controller
    {
        private PhoneBookDbEntities db = new PhoneBookDbEntities();
        // GET: Person
        public ActionResult Dashboard()
        {
            var person = db.People.ToList();
            return View(person);


        }

        // GET: Person/Details/5
        public ActionResult PersonDetails()
        {
            var person = db.People.ToList();
            return View(person);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(Person collection)
        {
            string user = User.Identity.GetUserId();
            try
            {
                Person p = new Person();
                p.FirstName = collection.FirstName;
                p.MiddleName = collection.MiddleName;
                p.LastName = collection.LastName;
                p.DateOfBirth = collection.DateOfBirth;
                p.AddedOn = DateTime.Now;
                p.HomeAddress = collection.HomeAddress;
                p.HomeCity = collection.HomeCity;
                p.FaceBookAccountId = collection.FaceBookAccountId;
                p.LinkedInId = collection.LinkedInId;
                p.TwitterId = collection.TwitterId;
                p.EmailId = collection.EmailId;
                p.AddedBy = user;

                db.People.Add(p);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
