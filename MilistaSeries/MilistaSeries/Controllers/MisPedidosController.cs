using Microsoft.AspNet.Identity;
using MilistaSeries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MilistaSeries.Controllers
{
    public class MisPedidosController : Controller
    {
        private MilistaseriesBaseEntities1 db = new MilistaseriesBaseEntities1();
        // GET: MisPedidos
        public ActionResult Index()
        {
            string wUsuario = User.Identity.GetUserName();
            var pEDIDO = db.PEDIDO.Where(p => p.USUARIOS.Correo == wUsuario && p.Confirmar == true);
            if (pEDIDO.Count() <= 0)
            {
                return RedirectToAction("SinPedidos");
            }

            return View(pEDIDO.ToList());
        }

        public ActionResult SinPedidos()
        {
            return View();
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

    }
}