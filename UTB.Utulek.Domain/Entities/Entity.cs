using UTB.Utulek.Domain.Entities.Interfaces;

namespace UTB.Utulek.Domain.Entities
{
    public class Entity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}