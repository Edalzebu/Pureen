using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pureen.Domain.Entities
{
    public class NewsListOrder : IEntity
    {
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual long NumberInLine { get; set; }
        public virtual long NewId { get; set; }

    }
}
