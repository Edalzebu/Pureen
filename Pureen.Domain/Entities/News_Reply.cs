using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pureen.Domain.Entities
{
    public class NewsReply : IEntity
    {
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual string Heading { get; set; }
        public virtual string Informacion { get; set; }
        public virtual long UserId { get; set; }
    }
}
