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
        public virtual string Heading { get; set; }
        public virtual string Information { get; set; }
        public virtual DateTime PostedDateTime { get; set; }
        public virtual bool CommentsEnabled { get; set; }
        public virtual long UserId { get; set; }
        public virtual IList<long> RepliesListIds { get; set; }

        // News Editing Versions
        public virtual bool IsaVersion { get; set; }
        public virtual long SubversionId { get; set; }
        public virtual long ParentVersionId { get; set; }
    }
}
