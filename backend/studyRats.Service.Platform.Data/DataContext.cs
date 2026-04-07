using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using studyRats.Service.Platform.Domain.Entities.User;

namespace studyRats.Service.Platform.Data
{
    public class DataContext : DbContext
    {
        #region DbSetSection

        public DbSet<User> Users { get; set; }

        #endregion

        #region Required On Model Creating



        #endregion


    }
}
