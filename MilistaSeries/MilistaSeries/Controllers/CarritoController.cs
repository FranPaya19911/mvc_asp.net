using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MilistaSeries.Models;

namespace MilistaSeries.Controllers
{
    public class CarritoController : Controller
    {
        private MilistaseriesBaseEntities1 db = new MilistaseriesBaseEntities1();
        // GET: Carrito
        public ActionResult Index()
        {
            if (Session["Pedido"] == null)
                return RedirectToAction("CarritoVacio");

           

            int idPedido = (int)Session["Pedido"];
            var lineas = db.LINEA_PEDIDO.Where(a => a.Fk_Pedido == idPedido);
            if (lineas.Count() <= 0)
            {
                return RedirectToAction("CarritoVacio");
            }
            ViewBag.idPedido = idPedido;

            decimal total = 0;
            foreach(var item in lineas)
            {
                total += (item.Camtidad * item.PRODUCTO.Precio);
            }
            ViewBag.total = total;

            return View(lineas.ToList());
        }

        public ActionResult CarritoVacio()
        {
            return View();
        }

        public ActionResult AgregarCarrito(int id)
        {
            if (Session["Pedido"] == null)
            {
                string usuario = User.Identity.GetUserName();
                USUARIOS cliente = db.USUARIOS.Where(e => e.Correo == usuario).FirstOrDefault();

                PEDIDO pedido = new PEDIDO();

                pedido.Fk_Usuario = cliente.IdUsuario;
                pedido.Confirmar = false;
                pedido.Total = 0;
                string FechaActual = DateTime.Today.ToString("dd/MM/yyyy");
                pedido.Fecha = DateTime.Parse(FechaActual);

                db.PEDIDO.Add(pedido);
                db.SaveChanges();

                Session["Pedido"] = pedido.IdPedido;
            }

            LINEA_PEDIDO linea = new LINEA_PEDIDO();
            PRODUCTO producto = db.PRODUCTO.Find(id);

            linea.Fk_Pedido = (int)Session["Pedido"];
            linea.Fk_Producto = producto.IdProducto;
            linea.Precio = producto.Precio;
            linea.Camtidad = 1;

            db.LINEA_PEDIDO.Add(linea);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ConfirmarCompra(int id)
        {
            PEDIDO pedido = db.PEDIDO.Find(id);
            
            int idPedido = (int)Session["Pedido"];
            var lineas = db.LINEA_PEDIDO.Where(a => a.Fk_Pedido == idPedido);

            decimal total = 0;
            foreach (var item in lineas)
            {
                total += (item.Camtidad * item.PRODUCTO.Precio);
            }

            pedido.Confirmar = true;
            pedido.Total = total;

            db.Entry(pedido).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            Session["Pedido"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult EliminarLinea(int id)
        {
            LINEA_PEDIDO linea = db.LINEA_PEDIDO.Find(id);
            db.LINEA_PEDIDO.Remove(linea);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SumarCantdiad(int id)
        {
            LINEA_PEDIDO linea = db.LINEA_PEDIDO.Find(id);
            linea.Camtidad++;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RestarCantdiad(int id)
        {
            LINEA_PEDIDO linea = db.LINEA_PEDIDO.Find(id);
            linea.Camtidad--;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}