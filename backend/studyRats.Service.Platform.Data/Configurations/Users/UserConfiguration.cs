using studyRats.Service.Platform.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;
using studyRats.Library.Framework.Core.Data.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace studyRats.Service.Platform.Data.Configurations.Users
{
    public class UserConfiguration: EntityConfigurationBase<User>
    {
        public UserConfiguration() { }
        public override void ConfigureEntityFields(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(t => t.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Email).IsRequired();
        }
    }
}
