using System;

namespace EnvelhecerBem.Core
{
    public interface IEntity
    {
        Guid Id { get; }
    }

    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
    }
}