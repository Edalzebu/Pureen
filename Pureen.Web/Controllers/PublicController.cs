using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Pureen.Domain.Entities;
using Pureen.Domain.Services;
using Pureen.Web.Models;

namespace Pureen.Web.Controllers
{
    public class PublicController : Controller
    {
        //
        // GET: /Public/
         private readonly IReadOnlyRepository _readOnlyRepository;
        private readonly IWriteOnlyRepository _writeOnlyRepository;

        public PublicController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
        }
        public ActionResult Index()
        {
            var listaNews = _readOnlyRepository.GetAll<News>();
            var lista = listaNews.Select(notice => Mapper.Map<News, ListNewsModel>(notice)).ToList();
            return View(lista);
        }

        public ActionResult Comments(long id)
        {
            // on this view we need to load the NEW then load all its replies
            var daNew = _readOnlyRepository.First<News>(x => x.Id == id);
            var model = Mapper.Map<News, ListNewsModel>(daNew);
            return View(model);
        }

    }
}
