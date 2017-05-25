using Model.Registers;
using Model.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Persistence.Contexts
{
    public class EFContext : DbContext
    {
        #region [ DbSet Properties ]

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }


        public EFContext() : base("Asp_Net_MVC_CS")
        {
            var dbInit = new
            DropCreateDatabaseIfModelChanges<EFContext>();
            Database.SetInitializer<EFContext>(dbInit);
        }
        #endregion
    }
}