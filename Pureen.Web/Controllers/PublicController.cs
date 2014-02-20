using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
            var daNew = _readOnlyRepository.First<News>(x => x.Id == id);
            var model = Mapper.Map<News, ListNewsModel>(daNew);
            return View(model);
        }

        public ActionResult LoadComments(long id)
        {
            var lista = new List<ListCommentsModel>();
            var theNew = _readOnlyRepository.First<News>(x => x.Id == id);
            foreach (var replyId in theNew.RepliesListIds)
            {
                var reply = _readOnlyRepository.First<NewsReply>(x => x.Id == replyId);
                var user = _readOnlyRepository.First<Account>(x => x.Id == reply.UserId);
                var model = Mapper.Map<NewsReply, ListCommentsModel>(reply);
                model.UserName = user.Username;
                lista.Add(model);
            }
            return PartialView(lista);
        }

        [HttpGet]
        public ActionResult MakeaComment(long id)
        {
            var model = new MakeaCommentModel();
            model.NewId = id;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult MakeaComment(MakeaCommentModel model)
        {
            var reply = Mapper.Map<MakeaCommentModel, NewsReply>(model);
            var user = GetAccountFromUserNameorEmail();
            reply.UserId = user.Id;
            var theNew = _readOnlyRepository.First<News>(x => x.Id == model.NewId);
            reply = _writeOnlyRepository.Create(reply);
            theNew.RepliesListIds.Add(reply.Id);
            _writeOnlyRepository.Update(theNew);
            return RedirectToAction("Comments", new {id = model.NewId});
        }
        public Account GetAccountFromUserNameorEmail()
        {
            var account = _readOnlyRepository.First<Account>(x => x.Email == User.Identity.Name) ??
                          _readOnlyRepository.First<Account>(x => x.Username == User.Identity.Name);
            return account;
        }
    }
}
