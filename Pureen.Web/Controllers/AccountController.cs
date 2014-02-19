using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using BootstrapMvcSample.Controllers;
using Pureen.Domain;
using Pureen.Domain.Entities;
using Pureen.Domain.Services;
using Pureen.Web.Models;
using Pureen.Web.Utils;

namespace Pureen.Web.Controllers
{
    public class AccountController : BootstrapBaseController
    {
        private readonly IReadOnlyRepository _readOnlyRepository;
        private readonly IWriteOnlyRepository _writeOnlyRepository;

        public AccountController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View(new AccountRegisterModel());
        }
        [HttpPost]
        public ActionResult Register(AccountRegisterModel model)
        {
            var account = Mapper.Map<AccountRegisterModel, Account>(model);
            
            if (CheckifAccountisaGo(account))
            {
                account.Password = Md5Encryption.Encriptar(model.Password);
                account.RegisterDateTime = DateTime.Now;
                _writeOnlyRepository.Create(account); 
                Success("Your Account is registered.");
                return RedirectToAction("Login");
            }
            else
            {
                Error("Failed to create an account ");
            }

            return View(new AccountRegisterModel());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View(new AccountLoginModel());
        }
        [HttpPost]
        public ActionResult Login(AccountLoginModel model)
        {

            var roles = new List<string>();
            if (CheckifAccountExists(model))
            {
                if (CheckAuthCredentials(model))
                {
                    
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    SetAuthenticationCookie(model.Username, roles);
                    return RedirectToAction("Index", "Public");
                }
                else
                {
                    Error("Incorrect password for this username.");
                }
            }
            else
            {
                Error("That username doesnt exists on our database.");
            }
               
            return View(new AccountLoginModel());
        }

        public ActionResult JellieHi()
        {
            var model = new JellieTestingModel {Message = "Hello world for Jellie", Username = "Edalzebu"};
            return View(model);
        }

        //Aux functions 
        public bool CheckAuthCredentials(AccountLoginModel model)
        {
            var account = ReturnAccountIfEmailExists(model) ?? ReturnAccountIfUsernameExists(model);
            if(account != null)
                return account.Password == Md5Encryption.Encriptar(model.Password);
            return false;
        }

        public Account ReturnAccountIfUsernameExists(AccountLoginModel model)
        {
            var account = _readOnlyRepository.First<Account>(x => x.Username == model.Username);
            return account;
        }
        public Account ReturnAccountIfEmailExists(AccountLoginModel model)
        {
            var account = _readOnlyRepository.First<Account>(x => x.Email == model.Username);
            return account;
        }
        public bool CheckifAccountEmailExists(Account account)
        {
            var realaccount = _readOnlyRepository.First<Account>(x => x.Email == account.Email);
            return realaccount != null;
        }

        public bool CheckifAccountUsernameExists(Account account)
        {
            var realaccount = _readOnlyRepository.First<Account>(x => x.Username == account.Username);
            return realaccount != null;
        }

        public bool CheckifAccountisaGo(Account account)
        {
            return !(CheckifAccountEmailExists(account) || CheckifAccountUsernameExists(account));
        }

        public bool CheckifAccountExists(AccountLoginModel model)
        {
            var account = ReturnAccountIfEmailExists(model) ?? ReturnAccountIfUsernameExists(model);
            return account != null;
        }
    } 
}