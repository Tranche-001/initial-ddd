using Microsoft.EntityFrameworkCore;
using studyRats.Service.Platform.Data.Configurations.Users;
using studyRats.Service.Platform.Domain.Abstractions;
using studyRats.Service.Platform.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace studyRats.Service.Platform.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {}


        #region DbSetSection

        public DbSet<User> Users { get; set; }

        #endregion

        #region Place to Apply Configurations

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        #endregion
    }
}
