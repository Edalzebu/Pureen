using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pureen.Web.Models;

namespace Pureen.Web.Controllers
{
    public class PublicController : Controller
    {
        //
        // GET: /Public/

        public ActionResult Index()
        {
            var model = new ListNewsModel();
            model.Information = "Esta es una prueba de informacion de como saldra en la pantalla de las NEws";
            model.Month = DateTime.Now.ToString("MMMM");
            model.Year = "2014";
            model.Day = DateTime.Now.ToString("dd");
            model.Heading = "Prueba!";
            var lista = new List<ListNewsModel>();          
            lista.Add(model);
            return View(lista);
        }

    }
}
