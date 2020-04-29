using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using AutoLotDal.Models;
using AutoLotDal.Interception;

namespace AutoLotDal.EF
{
    public class AutoLotEntities : DbContext
    {
        public AutoLotEntities()
            : base("name=AutoLotConnection")
        {
       //----------------to do for interseption
       // DbInterception.Add(new ConsoleWriterInterceptor());
       //---------------to do for DataBaseLogger
       //    DatabaseLogger.StartLogging();
       //   DbInterception.Add(DatabaseLogger);
        
        //---------------to do for objectContext interseption
        var context = (this as IObjectContextAdapter).ObjectContext;
        context.ObjectMaterialized += OnObjectMaterialized;

        }

        static readonly DatabaseLogger DatabaseLogger = new DatabaseLogger();
        public  virtual DbSet<CreditRisk> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>()
                .HasMany(e=>e.Orders)
                .WithRequired(e=>e.Inventory)
                .WillCascadeOnDelete(false);
        }

        protected override void Dispose(bool disposing)
        {
            DbInterception.Remove(DatabaseLogger);
            DatabaseLogger.StopLogging();
            base.Dispose(disposing);
        }

        private void OnObjectMaterialized(object sender, System.Data.Entity.Core.Objects.ObjectMaterializedEventArgs e)
        {
            Console.WriteLine("ObjectMaterialized ON");
        }
        
    }
}