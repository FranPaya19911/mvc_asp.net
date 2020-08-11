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
    public class PedidoController : Controller
    {
        private MilistaseriesBaseEntities1 db = new MilistaseriesBaseEntities1();

        // GET: Pedido
        public ActionResult Index()
        {
            var pEDIDO = db.PEDIDO.Include(p => p.USUARIOS);
            return View(pEDIDO.ToList());
        }

        // GET: Pedido/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PEDIDO pEDIDO = db.PEDIDO.Find(id);
            if (pEDIDO == null)
            {
                return HttpNotFound();
            }
            return View(pEDIDO);
        }

        // GET: Pedido/Create
        public ActionResult Create()
        {
            ViewBag.Fk_Usuario = new SelectList(db.USUARIOS, "IdUsuario", "Nombre");
            return View();
        }

        // POST: Pedido/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPedido,Fecha,Fk_Usuario,Total,Confirmar")] PEDIDO pEDIDO)
        {
            if (ModelState.IsValid)
            {
                db.PEDIDO.Add(pEDIDO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fk_Usuario = new SelectList(db.USUARIOS, "IdUsuario", "Nombre", pEDIDO.Fk_Usuario);
            return View(pEDIDO);
        }

        // GET: Pedido/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PEDIDO pEDIDO = db.PEDIDO.Find(id);
            if (pEDIDO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_Usuario = new SelectList(db.USUARIOS, "IdUsuario", "Nombre", pEDIDO.Fk_Usuario);
            return View(pEDIDO);
        }

        // POST: Pedido/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPedido,Fecha,Fk_Usuario,Total,Confirmar")] PEDIDO pEDIDO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pEDIDO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fk_Usuario = new SelectList(db.USUARIOS, "IdUsuario", "Nombre", pEDIDO.Fk_Usuario);
            return View(pEDIDO);
        }

        // GET: Pedido/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PEDIDO pEDIDO = db.PEDIDO.Find(id);
            if (pEDIDO == null)
            {
                return HttpNotFound();
            }
            return View(pEDIDO);
        }

        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PEDIDO pEDIDO = db.PEDIDO.Find(id);
            db.PEDIDO.Remove(pEDIDO);
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
