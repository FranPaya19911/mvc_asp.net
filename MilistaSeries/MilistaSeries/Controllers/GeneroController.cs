using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MilistaSeries;
using MilistaSeries.Models;

namespace MilistaSeries.Controllers
{
    public class GeneroController : Controller
    {
        private MilistaseriesBaseEntities1 db = new MilistaseriesBaseEntities1();

        // GET: Genero
        public ActionResult Index()
        {
            return View(db.GENERO.ToList());
        }

        // GET: Genero/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENERO gENERO = db.GENERO.Find(id);
            if (gENERO == null)
            {
                return HttpNotFound();
            }
            return View(gENERO);
        }

        // GET: Genero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genero/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdGenero,Nombre")] GENERO gENERO)
        {
            if (ModelState.IsValid)
            {
                db.GENERO.Add(gENERO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gENERO);
        }

        // GET: Genero/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENERO gENERO = db.GENERO.Find(id);
            if (gENERO == null)
            {
                return HttpNotFound();
            }
            return View(gENERO);
        }

        // POST: Genero/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdGenero,Nombre")] GENERO gENERO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gENERO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gENERO);
        }

        // // GET: Genero/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    GENERO gENERO = db.GENERO.Find(id);
        //    if (gENERO == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(gENERO);
        //}

        // // POST: Genero/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    GENERO gENERO = db.GENERO.Find(id);
        //    db.GENERO.Remove(gENERO);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
