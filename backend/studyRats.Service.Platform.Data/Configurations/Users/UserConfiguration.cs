using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using studyRats.Service.Platform.Domain.Entities.Users;
using studyRats.Service.Platform.Domain.ValueObjects;
using System.Reflection.Emit;

namespace studyRats.Service.Platform.Data.Configurations.Users
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public UserConfiguration() { }
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .HasConversion(
                    e => e.Value,                      // Email -> string for storage
                    v => Email.Create(v).Value)        // string -> Email on materialization
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("Email");
        }
    }
}
