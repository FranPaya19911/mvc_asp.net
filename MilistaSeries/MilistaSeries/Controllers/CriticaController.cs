using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MilistaSeries;
using MilistaSeries.Models;

namespace MilistaSeries.Controllers
{
    public class CriticaController : Controller
    {
        private MilistaseriesBaseEntities1 db = new MilistaseriesBaseEntities1();

        // GET: Critica
        public ActionResult Index()
        {
            var cRITICA = db.CRITICA.Include(c => c.PRODUCTO).Include(c => c.USUARIOS);
            return View(cRITICA.ToList());
        }

        // GET: Critica/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CRITICA cRITICA = db.CRITICA.Find(id);
            if (cRITICA == null)
            {
                return HttpNotFound();
            }
            return View(cRITICA);
        }

        // GET: Critica/Create
        public ActionResult Create(int? id)
        {
            PRODUCTO producto = db.PRODUCTO.Find(id);
            ViewBag.nombre = producto.Titulo;
            ViewBag.img = producto.Imagen;
            ViewBag.Fk_Producto = new SelectList(db.PRODUCTO.Where(p=>p.IdProducto == id), "IdProducto", "Titulo");

            string wUsuario = User.Identity.GetUserName();
            ViewBag.fk_Usuario = new SelectList(db.USUARIOS.Where(u => u.Correo == wUsuario), "IdUsuario", "Nombre");
            return View();
        }

        // POST: Critica/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCritica,Nota,Comentario,fk_Usuario,Fk_Producto")] CRITICA cRITICA)
        {
            if (ModelState.IsValid)
            {
                db.CRITICA.Add(cRITICA);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Fk_Producto = new SelectList(db.PRODUCTO, "IdProducto", "Titulo", cRITICA.Fk_Producto);
            ViewBag.fk_Usuario = new SelectList(db.USUARIOS, "IdUsuario", "Nombre", cRITICA.fk_Usuario);
            return View(cRITICA);
        }

        // GET: Critica/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CRITICA cRITICA = db.CRITICA.Find(id);
            if (cRITICA == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_Producto = new SelectList(db.PRODUCTO, "IdProducto", "Titulo", cRITICA.Fk_Producto);
            ViewBag.fk_Usuario = new SelectList(db.USUARIOS, "IdUsuario", "Nombre", cRITICA.fk_Usuario);
            return View(cRITICA);
        }

        // POST: Critica/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCritica,Nota,Comentario,fk_Usuario,Fk_Producto")] CRITICA cRITICA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cRITICA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fk_Producto = new SelectList(db.PRODUCTO, "IdProducto", "Titulo", cRITICA.Fk_Producto);
            ViewBag.fk_Usuario = new SelectList(db.USUARIOS, "IdUsuario", "Nombre", cRITICA.fk_Usuario);
            return View(cRITICA);
        }

        // GET: Critica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CRITICA cRITICA = db.CRITICA.Find(id);
            if (cRITICA == null)
            {
                return HttpNotFound();
            }
            return View(cRITICA);
        }

        // POST: Critica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CRITICA cRITICA = db.CRITICA.Find(id);
            db.CRITICA.Remove(cRITICA);
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
