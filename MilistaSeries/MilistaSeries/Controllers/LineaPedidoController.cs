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
    public class LineaPedidoController : Controller
    {
        private Models.MilistaseriesBaseEntities1 db = new MilistaseriesBaseEntities1();

        // GET: LineaPedido
        public ActionResult Index()
        {
            var lINEA_PEDIDO = db.LINEA_PEDIDO.Include(l => l.PEDIDO).Include(l => l.PRODUCTO);
            return View(lINEA_PEDIDO.ToList());
        }

        // GET: LineaPedido/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LINEA_PEDIDO lINEA_PEDIDO = db.LINEA_PEDIDO.Find(id);
            if (lINEA_PEDIDO == null)
            {
                return HttpNotFound();
            }
            return View(lINEA_PEDIDO);
        }

        // GET: LineaPedido/Create
        public ActionResult Create()
        {
            ViewBag.Fk_Pedido = new SelectList(db.PEDIDO, "IdPedido", "IdPedido");
            ViewBag.Fk_Producto = new SelectList(db.PRODUCTO, "IdProducto", "Titulo");
            return View();
        }

        // POST: LineaPedido/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdLineaP,Camtidad,Fk_Pedido,Fk_Producto,Precio")] LINEA_PEDIDO lINEA_PEDIDO)
        {
            if (ModelState.IsValid)
            {
                db.LINEA_PEDIDO.Add(lINEA_PEDIDO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fk_Pedido = new SelectList(db.PEDIDO, "IdPedido", "IdPedido", lINEA_PEDIDO.Fk_Pedido);
            ViewBag.Fk_Producto = new SelectList(db.PRODUCTO, "IdProducto", "Titulo", lINEA_PEDIDO.Fk_Producto);
            return View(lINEA_PEDIDO);
        }

        // GET: LineaPedido/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LINEA_PEDIDO lINEA_PEDIDO = db.LINEA_PEDIDO.Find(id);
            if (lINEA_PEDIDO == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fk_Pedido = new SelectList(db.PEDIDO, "IdPedido", "IdPedido", lINEA_PEDIDO.Fk_Pedido);
            ViewBag.Fk_Producto = new SelectList(db.PRODUCTO, "IdProducto", "Titulo", lINEA_PEDIDO.Fk_Producto);
            return View(lINEA_PEDIDO);
        }

        // POST: LineaPedido/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdLineaP,Camtidad,Fk_Pedido,Fk_Producto,Precio")] LINEA_PEDIDO lINEA_PEDIDO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lINEA_PEDIDO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fk_Pedido = new SelectList(db.PEDIDO, "IdPedido", "IdPedido", lINEA_PEDIDO.Fk_Pedido);
            ViewBag.Fk_Producto = new SelectList(db.PRODUCTO, "IdProducto", "Titulo", lINEA_PEDIDO.Fk_Producto);
            return View(lINEA_PEDIDO);
        }

        // GET: LineaPedido/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LINEA_PEDIDO lINEA_PEDIDO = db.LINEA_PEDIDO.Find(id);
            if (lINEA_PEDIDO == null)
            {
                return HttpNotFound();
            }
            return View(lINEA_PEDIDO);
        }

        // POST: LineaPedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LINEA_PEDIDO lINEA_PEDIDO = db.LINEA_PEDIDO.Find(id);
            db.LINEA_PEDIDO.Remove(lINEA_PEDIDO);
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
