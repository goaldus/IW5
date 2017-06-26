using System;
using IWManager.DAL.Entities.Base.Interface;

namespace IWManager.DAL.Entities.Base.Implementation
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
