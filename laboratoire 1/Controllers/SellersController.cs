using MyCRUD_WebApp_JSON.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace laboratoire_1.Controllers
{
    public class SellersController : Controller
    {
        // GET: Seller
        public ActionResult Index()
        {
            return View(DB.Sellers.ToList().OrderBy(c => c.Id).ThenBy(c => c.Name).ToList());
        }

        public ActionResult Create()
        {
            return View(new Seller {});
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = DB.Sellers.Get(id.Value);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }
        //code donner par le prof
        // POST: Contacts/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Seller seller)
        {
            if (ModelState.IsValid)
            {
                DB.Sellers.Add(seller);
                return RedirectToAction("Index");
            }
            return View(seller);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = DB.Sellers.Get(id.Value);

            if (seller == null)
            {
                return HttpNotFound();
            }
            Session["CurrentModifiedSellerId"] = seller.Id;

            return View(seller);
        }

        // POST: Contacts/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Seller seller)
        {
            if (seller.Id != (int)Session["CurrentModifiedContactId"])
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                DB.Sellers.Update(seller);
                return RedirectToAction("Index");
            }
            return View(seller);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int id)
        {
            DB.Sellers.Delete(id);
            return RedirectToAction("Index");
        }
    }
}