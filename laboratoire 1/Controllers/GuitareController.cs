using MyCRUD_WebApp_JSON.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace laboratoire_1.Controllers
{
    public class GuitareController : Controller
    {
        // GET: Guitare
        public ActionResult Index()
        {
            return View(DB.Guitars.ToList().OrderBy(c => c.Id).ThenBy(c => c.Id).ToList());
        }

        public ActionResult Create()
        {
            return View(new Guitar());
        }

        // POST: Guitare/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guitar guitar)
        {
            try
            {
                List<Seller> sellers = MyCRUD_WebApp_JSON.Models.DB.Sellers.ToList();
                guitar.Seller = sellers.Find((obj) => obj.Id == guitar.SellerId);
            }
            catch
            {

            }


            if (ModelState.IsValid)
            {
                DB.Guitars.Add(guitar);
                return RedirectToAction("Index");
            }
            return View(guitar);
        }

        // GET: Guitare/Delete/5
        public ActionResult Delete(int id)
        {
            DB.Guitars.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Guitare/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guitar guitar = DB.Guitars.Get(id.Value);
            if (guitar == null)
            {
                return HttpNotFound();
            }
            return View(guitar);
        }

        // GET: Guitare/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guitar guitare = DB.Guitars.Get(id.Value);

            if (guitare == null)
            {
                return HttpNotFound();
            }
            return View(guitare);
        }

        // POST: Guitare/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guitar guitar)
        {
            if (guitar.Id != (int)Session["CurrentModifiedContactId"])
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                DB.Guitars.Update(guitar);
                return RedirectToAction("Index");
            }
            return View(guitar);
        }
    }
}