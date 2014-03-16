using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using AutoMapper;
using Pureen.Domain.Entities;
using Pureen.Domain.Services;
using Pureen.Web.Models;

namespace Pureen.Web.Controllers
{
    public class AdministratorController : Controller
    {
        //
        // GET: /Administrator/
        private readonly IReadOnlyRepository _readOnlyRepository;
        private readonly IWriteOnlyRepository _writeOnlyRepository;
        private const int NoSubversionNumber = -999;

        public AdministratorController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
        }
        public ActionResult AdminCp()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PostNews()
        {
            return PartialView(new ListNewsModel());
        }

        [HttpPost]
        public ActionResult PostNews(ListNewsModel model)
        {
            var singleNew = CreateNew(model);
            singleNew =_writeOnlyRepository.Create(singleNew);
            var nuevoregistrodeOrden = new NewsListOrder();
            nuevoregistrodeOrden.NewId = singleNew.Id;
            nuevoregistrodeOrden.NumberInLine = _readOnlyRepository.GetAll<NewsListOrder>().ToList().Count;
            _writeOnlyRepository.Create(nuevoregistrodeOrden);
            return RedirectToAction("AdminCp");
        }

        public ActionResult UsersList()
        {
            var userList = _readOnlyRepository.GetAll<Account>();
            var userListModel = new List<ListUsersModel>();
            foreach (var account in userList)
            {
                var model = new ListUsersModel();
                model.FirstName = account.FirstName;
                model.LastName = account.LastName;
                model.UserName = account.Username;
                model.RegisteredOn = account.RegisterDateTime.ToString("dddd, dd/MM/yyyy");
                userListModel.Add(model);
            }
            if (userListModel.Count == 0)
            {
                var model2 = new ListUsersModel();
                userListModel.Add(model2);
            }
           
            return PartialView(userListModel);
        }

        public ActionResult BanUser()
        {
            return PartialView();
        }

        public ActionResult Randomizer()
        {
            var rnd = new Random();
            var userList = _readOnlyRepository.GetAll<Account>();
            var user = userList.ToList().ElementAt( rnd.Next(userList.Count()));
            var model = Mapper.Map<Account, ListUsersModel>(user);
            model.RegisteredOn = DateTime.Now.ToString("dddd, dd/MM/yyyy");
            return PartialView(model);
        }
        public ActionResult ManageNews()
        {
            var lista = new List<ListNewsModel>();
            var daNews = _readOnlyRepository.GetAll<News>();
            foreach (var daNew in daNews)
            {
                if (CheckifNewIsGoodToGo(daNew))
                {
                    var model = Mapper.Map<News, ListNewsModel>(daNew);
                    model.Day = daNew.PostedDateTime.ToString("dd");
                    model.Year = daNew.PostedDateTime.ToString("yyyy");
                    model.Month = daNew.PostedDateTime.ToString("MMMM");
                    model.UserName = _readOnlyRepository.First<Account>(x => x.Id == daNew.UserId).Username;
                    lista.Add(model);
                }
                    
            }
            return PartialView(lista);
        }

        public ActionResult ContactManager()
        {
            var lista = new List<ListContactMessagesModel>();
            var messages = _readOnlyRepository.GetAll<ContactMessage>();
            foreach (var message in messages)
            {
                var model = Mapper.Map<ContactMessage, ListContactMessagesModel>(message);
                model.UserName = _readOnlyRepository.First<Account>(x => x.Id == message.UserId).Username;
                lista.Add(model);
            }
            return PartialView(lista);
        }
        public ActionResult DeleteNew(long id)
        {
            var daNew = _readOnlyRepository.First<News>(x => x.Id == id);
            _writeOnlyRepository.Delete(daNew);
            return RedirectToAction("AdminCp");
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            return PartialView(new ListNewsModel());
        }

        [HttpPost]
        public ActionResult Edit(ListNewsModel model)
        {
            var theNew = CreateNew(model);
            theNew.SubversionId = model.Id;
            theNew =_writeOnlyRepository.Create(theNew);
            var theOldNew = ChangeNewtoOldNotices(model.Id,theNew.Id);
            _writeOnlyRepository.Update(theOldNew);
            theNew.CommentsEnabled = theOldNew.CommentsEnabled;
            _writeOnlyRepository.Update(theNew);
           return RedirectToAction("AdminCp", "Administrator");
        }

        [HttpGet]
        public ActionResult ViewNoticeChangeLog(long id)
        {
            var daNew = _readOnlyRepository.First<News>(x => x.Id == id);
            var listaNews = new List<ListNewsModel>();

            while (daNew.SubversionId != NoSubversionNumber)
            {
                listaNews.Add(Mapper.Map<News, ListNewsModel>(daNew));
                daNew = _readOnlyRepository.First<News>(x => x.Id == daNew.SubversionId);
            }
            return PartialView(listaNews);
        }

        // Auxiliares
        public News ChangeNewtoOldNotices(long oldNewId, long newNoticeId)
        {
            var oldNew = _readOnlyRepository.First<News>(x => x.Id == oldNewId);
            oldNew.IsaVersion = true;
            oldNew.ParentVersionId = newNoticeId;
            var ordenviejo = _readOnlyRepository.First<NewsListOrder>(x => x.NewId == oldNew.Id);
            ordenviejo.NewId = newNoticeId;
            ordenviejo.NumberInLine = ordenviejo.NumberInLine;
            _writeOnlyRepository.Update(ordenviejo);
            return oldNew;
        }
        private static bool CheckifNewIsGoodToGo(News daNew)
        {
            return daNew.IsaVersion != true;
        }

        public News FillingTheNewswithExtraData(News danew)
        {
            var account = GetAccountFromUserNameorEmail();
            danew.UserId = account.Id;
            danew.PostedDateTime = DateTime.Now;
            danew.IsaVersion = false;
            return danew;
        }
        public Account GetAccountFromUserNameorEmail()
        {
            var account = _readOnlyRepository.First<Account>(x => x.Email == User.Identity.Name) ??
                          _readOnlyRepository.First<Account>(x => x.Username == User.Identity.Name);
            return account;
        }
        public News CreateNew(ListNewsModel model)
        {
            var singleNew = Mapper.Map<ListNewsModel, News>(model);
            singleNew = FillingTheNewswithExtraData(singleNew);
            singleNew.SubversionId = NoSubversionNumber;
            return singleNew;
        }

        public ActionResult ShowContactMessage(long id)
        {
            var message = _readOnlyRepository.First<ContactMessage>(x => x.Id == id);
            var model = Mapper.Map<ContactMessage, ShowContactMessageModel>(message);
            model.UserName = _readOnlyRepository.First<Account>(x => x.Id == message.UserId).Username;
            return PartialView(model);
        }
    }
}
