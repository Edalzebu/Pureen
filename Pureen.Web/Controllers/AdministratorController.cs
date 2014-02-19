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
    public class AdministratorController : Controller
    {
        //
        // GET: /Administrator/
        private readonly IReadOnlyRepository _readOnlyRepository;
        private readonly IWriteOnlyRepository _writeOnlyRepository;

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
            var singleNew = Mapper.Map<ListNewsModel, News>(model);
            singleNew = FillingTheNewswithExtraData(singleNew);
            _writeOnlyRepository.Create(singleNew);
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

        // Auxiliars

        public News FillingTheNewswithExtraData(News danew)
        {
            var account = GetAccountFromUserNameorEmail();
            danew.UserId = account.Id;
            danew.PostedDateTime = DateTime.Now;
            return danew;
        }

        public Account GetAccountFromUserNameorEmail()
        {
            var account = _readOnlyRepository.First<Account>(x => x.Email == User.Identity.Name) ??
                          _readOnlyRepository.First<Account>(x => x.Username == User.Identity.Name);
            return account;
        }
    }
}
