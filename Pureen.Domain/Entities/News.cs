using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pureen.Domain.Entities
{
    public class News : IEntity
    {
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
        
    }
}
