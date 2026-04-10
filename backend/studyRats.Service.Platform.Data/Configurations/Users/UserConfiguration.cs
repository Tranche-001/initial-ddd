using studyRats.Service.Platform.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;
using studyRats.Library.Framework.Core.Data.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace studyRats.Service.Platform.Data.Configurations.Users
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public UserConfiguration() { }
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(t => t.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Email).IsRequired();
        }
    }
}
