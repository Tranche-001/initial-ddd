using System;

namespace studyRats.Library.Framework.Core.Domain.Entity
{
    public abstract class Entity<T> where T : Entity<T>
    {
        public EntityId Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Indica se a entidade é transitória (recém-criada, ainda não persistida/carregada do banco).
        /// Entidades transitórias não validam propriedades de navegação.
        /// </summary>
        public bool IsTransient => Id == null || !Id.Value.HasValue || Id.Value.Value == Guid.Empty;

        public static T New()
        {
            var newEntity = Activator.CreateInstance<T>();
            var guidString = Guid.NewGuid().ToString();
            newEntity.Id = new EntityId(guidString);
            newEntity.IsActive = true;
            newEntity.CreateDate = DateTime.UtcNow;
            return newEntity;
        }

        public static T NewWith(EntityId id)
        {
            var newEntity = Activator.CreateInstance<T>();
            newEntity.Id = id;
            newEntity.IsActive = true;
            newEntity.CreateDate = DateTime.UtcNow;
            return newEntity;
        }
    }
}