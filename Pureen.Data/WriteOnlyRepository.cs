using System;
using NHibernate;
using Pureen.Domain.Entities;
using Pureen.Domain.Services;

namespace Pureen.Data
{
    public class WriteOnlyRepository : IWriteOnlyRepository
    {
        private readonly ISession _session;

        public WriteOnlyRepository(ISession session)
        {
            _session = session;
        }

        public T Create<T>(T itemToCreate) where T : class, IEntity
        {
            _session.Save(itemToCreate);
            return itemToCreate;
        }

        public T Update<T>(T itemToUpdate) where T : class, IEntity
        {
            _session.Update(itemToUpdate);
            _session.Flush();
            _session.Clear();
            return itemToUpdate;
        }

        public void Archive<T>(T itemToArchive)
        {
            throw new NotImplementedException();
        }
        public void Delete<T>(T itemToDelete) where T : class, IEntity
        {
            _session.Delete(itemToDelete);
            _session.Flush();
            _session.Clear();

        }

    }
}