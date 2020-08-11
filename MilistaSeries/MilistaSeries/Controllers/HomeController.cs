using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MilistaSeries.Models;

namespace MilistaSeries.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
         MilistaseriesBaseEntities1 db = new MilistaseriesBaseEntities1();
        // Si existe el empleado correspondiente al usuario actual
        // se va a View, en caso contrario se va a crear el empleado.
        string usuario = User.Identity.GetUserName();
            var usuarios = db.USUARIOS.Where(e => e.Correo == usuario).FirstOrDefault();
            if (User.Identity.IsAuthenticated &&
            User.IsInRole("Usuario") &&
            usuarios == null)
            {
                return RedirectToAction("Create", "MisDatos");
            }

            var pRODUCTO = db.PRODUCTO;
            return View(pRODUCTO.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}