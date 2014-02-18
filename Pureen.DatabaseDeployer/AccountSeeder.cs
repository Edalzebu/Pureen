using System.Collections.Generic;
using DomainDrivenDatabaseDeployer;
using FizzWare.NBuilder;

using NHibernate;
using Pureen.Domain.Entities;

namespace Pureen.DatabaseDeployer
{
    public class AccountSeeder : IDataSeeder
    {
        private readonly ISession _session;

        public AccountSeeder(ISession session)
        {
            _session = session;
        }

        public void Seed()
        {
            IList<Account> accountList = Builder<Account>.CreateListOfSize(10).Build();
            foreach (Account account in accountList)
            {
                _session.Save(account);
            }
        }
    }
}