using System;

namespace studyRats.Library.Framework.Core.Domain.Entity
{
    public class EntityId : TypedIdValueBase
    {
        public EntityId()
        {
        }

        public EntityId(string value) : base(value)
        {
        }

        public EntityId(Guid value) : base(value)
        {
        }
    }
}
