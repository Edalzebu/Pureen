﻿using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Pureen.Domain;
using Pureen.Domain.Entities;
using Pureen.Web.Models;
using Ninject.Modules;

namespace Pureen.Web.Infrastructure
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<AccountRegisterModel, Account>().ForMember(x=>x.FirstName, o=>o.MapFrom(y=>y.Firstname)).ForMember(x=>x.LastName, o=>o.MapFrom(y=>y.Lastname));
            Mapper.CreateMap<AccountLoginModel, Account>();
            Mapper.CreateMap<Account, ListUsersModel>();
            Mapper.CreateMap<Account, AccountProfileModel>().ForMember(x=>x.MemberSince, o=>o.MapFrom(y=>y.RegisterDateTime));
            Mapper.CreateMap<News, ListNewsModel>();
            Mapper.CreateMap<ListNewsModel, News>();
            Mapper.CreateMap<NewsReply, ListCommentsModel>()
                .ForMember(x => x.Information, o => o.MapFrom(y => y.Informacion));
            Mapper.CreateMap<MakeaCommentModel, NewsReply>()
                .ForMember(x => x.Informacion, i => i.MapFrom(o => o.Information));
            Mapper.CreateMap<ContactUsModel, ContactMessage>();
            Mapper.CreateMap<ContactMessage, ContactUsModel>();
            Mapper.CreateMap<ContactMessage, ListContactMessagesModel>();
            Mapper.CreateMap<ContactMessage, ShowContactMessageModel>();
        }
    }
}