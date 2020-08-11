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
    public class GeneroProductoController : Controller
    {
        private MilistaseriesBaseEntities1 db = new MilistaseriesBaseEntities1();

        // GET: GeneroProducto
        public ActionResult Index()
        {
            var gENERO_PRODUCTO = db.GENERO_PRODUCTO.Include(g => g.GENERO).Include(g => g.PRODUCTO);
            return View(gENERO_PRODUCTO.ToList());
        }

        // GET: GeneroProducto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENERO_PRODUCTO gENERO_PRODUCTO = db.GENERO_PRODUCTO.Find(id);
            if (gENERO_PRODUCTO == null)
            {
                return HttpNotFound();
            }
            return View(gENERO_PRODUCTO);
        }

        // GET: GeneroProducto/Create
        public ActionResult Create()
        {
            ViewBag.Fk_Genero = new SelectList(db.GENERO, "IdGenero", "Nombre");
            ViewBag.Fk_Producto = new SelectList(db.PRODUCTO, "IdProducto", "Titulo");
            return View();
        }

        // POST: GeneroProducto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdG_P,Fk_Genero,Fk_Producto")] GENERO_PRODUCTO gENERO_PRODUCTO)
        {
            if (ModelState.IsValid)
            {
                db.GENERO_PRODUCTO.Add(gENERO_PRODUCTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fk_Genero = new SelectList(db.GENERO, "IdGenero", "Nombre", gENERO_PRODUCTO.Fk_Genero);
            ViewBag.Fk_Producto = new SelectList(db.PRODUCTO, "IdProducto", "Titulo", gENERO_PRODUCTO.Fk_Producto);
            return View(gENERO_PRODUCTO);
        }

        // GET: GeneroProducto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENERO_PRODUCTO gENERO_PRODUCTO = db.GENERO_PRODUCTO.Find(id);
            if (gENERO_PRODUCTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_Genero = new SelectList(db.GENERO, "IdGenero", "Nombre", gENERO_PRODUCTO.Fk_Genero);
            ViewBag.Fk_Producto = new SelectList(db.PRODUCTO, "IdProducto", "Titulo", gENERO_PRODUCTO.Fk_Producto);
            return View(gENERO_PRODUCTO);
        }

        // POST: GeneroProducto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdG_P,Fk_Genero,Fk_Producto")] GENERO_PRODUCTO gENERO_PRODUCTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gENERO_PRODUCTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fk_Genero = new SelectList(db.GENERO, "IdGenero", "Nombre", gENERO_PRODUCTO.Fk_Genero);
            ViewBag.Fk_Producto = new SelectList(db.PRODUCTO, "IdProducto", "Titulo", gENERO_PRODUCTO.Fk_Producto);
            return View(gENERO_PRODUCTO);
        }

        // GET: GeneroProducto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENERO_PRODUCTO gENERO_PRODUCTO = db.GENERO_PRODUCTO.Find(id);
            if (gENERO_PRODUCTO == null)
            {
                return HttpNotFound();
            }
            return View(gENERO_PRODUCTO);
        }

        // POST: GeneroProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GENERO_PRODUCTO gENERO_PRODUCTO = db.GENERO_PRODUCTO.Find(id);
            db.GENERO_PRODUCTO.Remove(gENERO_PRODUCTO);
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
