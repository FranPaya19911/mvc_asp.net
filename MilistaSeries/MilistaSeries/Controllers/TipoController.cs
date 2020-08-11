using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MilistaSeries.Models;

namespace MilistaSeries.Controllers
{
    public class TipoController : Controller
    {
        private MilistaseriesBaseEntities1 db = new MilistaseriesBaseEntities1();

        // GET: Tipo
        public ActionResult Index()
        {
            return View(db.TIPO.ToList());
        }

        // GET: Tipo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO tIPO = db.TIPO.Find(id);
            if (tIPO == null)
            {
                return HttpNotFound();
            }
            return View(tIPO);
        }

        // GET: Tipo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tipo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipo,Nombre")] TIPO tIPO)
        {
            if (ModelState.IsValid)
            {
                db.TIPO.Add(tIPO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIPO);
        }

        // GET: Tipo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO tIPO = db.TIPO.Find(id);
            if (tIPO == null)
            {
                return HttpNotFound();
            }
            return View(tIPO);
        }

        // POST: Tipo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipo,Nombre")] TIPO tIPO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIPO);
        }

        // GET: Tipo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO tIPO = db.TIPO.Find(id);
            if (tIPO == null)
            {
                return HttpNotFound();
            }
            return View(tIPO);
        }

        // POST: Tipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TIPO tIPO = db.TIPO.Find(id);
            db.TIPO.Remove(tIPO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
