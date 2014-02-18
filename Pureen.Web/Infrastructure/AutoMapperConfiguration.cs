using System.Collections.Generic;
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
        }
    }
}