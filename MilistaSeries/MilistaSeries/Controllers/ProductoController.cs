using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MilistaSeries.Models;

namespace MilistaSeries.Controllers
{
    public class ProductoController : Controller
    {
        private MilistaseriesBaseEntities1 db = new MilistaseriesBaseEntities1();

        // GET: Producto
        public ActionResult Index()
        {
            var pRODUCTO = db.PRODUCTO.Include(p => p.TIPO);
            return View(pRODUCTO.ToList());
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTO pRODUCTO = db.PRODUCTO.Find(id);
            if (pRODUCTO == null)
            {
                return HttpNotFound();
            }

            return View(pRODUCTO);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.Fk_Tipo = new SelectList(db.TIPO, "IdTipo", "Nombre");
            return View();
        }

        // POST: Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProducto,Titulo,Anyo,Duracion,Pais,Sinopsis,Imagen,Clasificacion,Precio,Direccion,licencias,Studios,Actores,Fk_Tipo")] PRODUCTO producto, HttpPostedFileBase Imagen)
        {

            producto.Precio = (producto.Precio / 100);
            if (ModelState.IsValid)
            {
                db.PRODUCTO.Add(producto);
                db.SaveChanges();
                if(producto.Imagen != null)
                {

                    producto.Imagen = producto.IdProducto + Path.GetExtension(Imagen.FileName);
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();
                    Imagen.SaveAs(Server.MapPath("~/Content/Images/producto/" + producto.Imagen));
                }
                

                return RedirectToAction("Index");
            }

            ViewBag.Fk_Tipo = new SelectList(db.TIPO, "IdTipo", "Nombre", producto.Fk_Tipo);
            return View(producto);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTO pRODUCTO = db.PRODUCTO.Find(id);
            if (pRODUCTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_Tipo = new SelectList(db.TIPO, "IdTipo", "Nombre", pRODUCTO.Fk_Tipo);
            return View(pRODUCTO);
        }

        // POST: Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProducto,Titulo,Anyo,Duracion,Pais,Sinopsis,Imagen,Clasificacion,Precio,Direccion,licencias,Studios,Actores,Fk_Tipo")] PRODUCTO pRODUCTO)
        {
            //pRODUCTO.Precio = (pRODUCTO.Precio / 100);
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fk_Tipo = new SelectList(db.TIPO, "IdTipo", "Nombre", pRODUCTO.Fk_Tipo);
            return View(pRODUCTO);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTO pRODUCTO = db.PRODUCTO.Find(id);
            if (pRODUCTO == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCTO);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCTO pRODUCTO = db.PRODUCTO.Find(id);
            db.PRODUCTO.Remove(pRODUCTO);
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
