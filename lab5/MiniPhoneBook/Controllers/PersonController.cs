using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MiniPhoneBook.Controllers
{
    public class PersonController : Controller
    {
        private PhoneBookDbEntities db = new PhoneBookDbEntities();
        // GET: Person
        [Authorize]
        public ActionResult Index()
        {
            string user = User.Identity.GetUserId();
            List<Person> person = db.People.Where(c => c.AddedBy == user).Include(x => x.Contacts).ToList();
            return View(person);
        }

        // GET: Person/Details/5
        public ActionResult Details(int id)
        {
            Person person = db.People.SingleOrDefault(lambda => lambda.PersonId == id);

            return View(person);
        }

        // GET: Person/Create
        [Authorize]
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
                p.AddedOn = DateTime.Now;
                p.DateOfBirth = collection.DateOfBirth;
                p.FaceBookAccountId = collection.FaceBookAccountId;
                p.TwitterId = collection.TwitterId;
                p.LinkedInId = collection.LinkedInId;
                p.EmailId = collection.EmailId;
                p.HomeCity = collection.HomeCity;
                p.HomeAddress = collection.HomeAddress;
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
            Person person = db.People.SingleOrDefault(lam => lam.PersonId == id);

            return View(person);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Person collection)
        {
            try
            {
                Person p = db.People.SingleOrDefault(lam => lam.PersonId == id);
                p.FirstName = collection.FirstName;
                p.MiddleName = collection.MiddleName;
                p.LastName = collection.LastName;
                p.DateOfBirth = collection.DateOfBirth;
                p.FaceBookAccountId = collection.FaceBookAccountId;
                p.TwitterId = collection.TwitterId;
                p.LinkedInId = collection.LinkedInId;
                p.EmailId = collection.EmailId;
                p.HomeCity = collection.HomeCity;
                p.HomeAddress = collection.HomeAddress;
                db.People.Single(c => c.PersonId == id).UpdateOn = DateTime.Now.Date;

                db.SaveChanges();


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
                Person p = db.People.SingleOrDefault(lam => lam.PersonId == id);

                db.Contacts.Where(c => c.PersonId == id)
                    .ToList().ForEach(t => db.Contacts.Remove(t));
                db.People.Remove(p);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateContact(Contact collection, int id) {

            try
            {
                Contact obj = new Contact();
                obj.ContactNumber = collection.ContactNumber;
                obj.Type = collection.Type;
                obj.PersonId = db.People.Single(c => c.PersonId == id).PersonId;
                db.Contacts.Add(obj);
                db.SaveChanges();
               

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        public ActionResult Dashboard()
        {
            string user = User.Identity.GetUserId();
            List<Person> person = db.People.Where(c => c.AddedBy == user).ToList();
            return View(person);
            
        }
    }
}
