using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MilistaSeries.Models;

namespace MilistaSeries.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class MisDatosController : Controller
    {
        private MilistaseriesBaseEntities1 db = new MilistaseriesBaseEntities1();

        // GET: Perfil
        public ActionResult Index()
        {
            // Se seleccionan los datos del eusuario correspondiente al usuario actual
            string wUsuario = User.Identity.GetUserName();
            var usuario = db.USUARIOS.Where(e => e.Correo == wUsuario).FirstOrDefault();
            if (usuario == null)
            {
                // Si no existe el usuario, redirige a la acción Index del controlador Home
                return RedirectToAction("Index", "Home");
            }
            // Si existe el usuario correspondiente se va a View
            return View(usuario);
        }

        // GET: MisDatos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MisDatos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="IdUsuario,Nombre,Correo,Contrasenya,Ubicacion,Genero,FechaNacimiento,FechaCreacion,Imagen")] USUARIOS usuario, HttpPostedFileBase Imagen)
        {
            // Para asignar el Nombre del usuario identificado al campo Correo de USUARIOS
            usuario.Correo = User.Identity.GetUserName();
            usuario.Contrasenya = "7As5Esh3j8";
            string FechaActual = DateTime.Today.ToString("dd/MM/yyyy");
            usuario.FechaCreacion = DateTime.Parse(FechaActual);
            

            if (ModelState.IsValid)
            {
                db.USUARIOS.Add(usuario);
                db.SaveChanges();
                if (usuario.Imagen != null)
                {
                    usuario.Imagen = usuario.IdUsuario + Path.GetExtension(Imagen.FileName);
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    Imagen.SaveAs(Server.MapPath("~/Content/Images/usuario/" + usuario.Imagen));
                }
                else
                {
                    usuario.Imagen ="default.jpg";
                    db.SaveChanges();
                }

                // Redirige a la acción Index del controlador Home
                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }

        // GET: MisDatos/Edit
        public ActionResult Edit()
        {
            // Se seleccionan los datos del eusuario correspondiente al usuario actual
            string wUsuario = User.Identity.GetUserName();
            
            var usuario = db.USUARIOS.Where(e => e.Correo == wUsuario).FirstOrDefault();
            if (usuario == null)
            {
                // Si no existe el usuario, redirige a la acción Index del controlador Home
                return RedirectToAction("Index", "Home");
            }
            // Si existe el usuario correspondiente se va a View
            return View(usuario);
        }
        // POST: MisDatos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario, Nombre, Correo, Contrasenya, Ubicacion, Genero, FechaNacimiento, FechaCreacion, Imagen")] USUARIOS usuario, HttpPostedFileBase Imagen)
        {
            usuario.Correo = User.Identity.GetUserName();

            string valor = usuario.IdUsuario + ".jpg";

            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                if(usuario.Imagen != null && Imagen != null)
                {
                    usuario.Imagen = usuario.IdUsuario + Path.GetExtension(Imagen.FileName);
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    Imagen.SaveAs(Server.MapPath("~/Content/Images/usuario/" + usuario.Imagen));
                }
                else
                {
                    if (usuario.Imagen == "default.jpg") {
                        usuario.Imagen = "default.jpg";
                    }
                    else
                    {
                        usuario.Imagen = valor;
                    }
                    db.SaveChanges();
                }
                

                return RedirectToAction("Index", "MisDatos");
            }
            return View(usuario);
        }


    }
}