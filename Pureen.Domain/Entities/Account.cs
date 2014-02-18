using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pureen.Domain.Entities
{
    public class Account : IEntity
    {
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Facebook { get; set; }
        public virtual string Twitter { get; set; }
        public virtual string Password { get; set; }
        public virtual DateTime RegisterDateTime { get; set; }
    }
}
