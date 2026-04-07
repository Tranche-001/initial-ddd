using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using studyRats.Library.Framework.Core.Domain.Entity;

namespace studyRats.Library.Framework.Core.Data.Configurations
{
    public abstract class EntityConfigurationBase<T> : IEntityTypeConfiguration<T> where T : Entity<T>
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.CreateDate).IsRequired();
            builder.Property(c => c.UpdatedAt).IsRequired(false);

            ConfigureEntityFields(builder);

            Seed(builder);
        }

        // Serve para que as classes filhas configure o restante de seus atributos
        public abstract void ConfigureEntityFields(EntityTypeBuilder<T> builder);

        // Seed para caso queiramos testar um dado padrão no banco.
        public virtual void Seed(EntityTypeBuilder<T> builder)
        {
        }
    }
}
