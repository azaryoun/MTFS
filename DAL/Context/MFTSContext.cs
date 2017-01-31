using MTFS.Business.Domain.Model;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using MTFS.Utilities.General;
using System.Threading.Tasks;

namespace MTFS.DAL.Context
{
    //update-database -force
    //add-migration

    public partial class MFTSContext : DbContext, IUnitOfWork
    { 
        public MFTSContext()
           : base("name=MFTS")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        #region Define dbset for seed Method

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanySubsystem> CompanySubsystems { get; set; }
        public virtual DbSet<Menuitem> Menuitems { get; set; }
        public virtual DbSet<Menutitle> Menutitles { get; set; }
        public virtual DbSet<Subsystem> Subsystems { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Transporttype> Transporttypes { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<AgentCarrier> AgentCarriers { get; set; }

        #endregion 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentException("modelBuilder");
            }
            modelBuilder.Configurations.AddFromAssembly(Assembly.Load("MTFS.DAL"));

            base.OnModelCreating(modelBuilder); 
        }

        /// <summary>
        ///IUnitOfWork Members 
        /// </summary> 
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : Base
        {
            return base.Set<TEntity>();

        }
        public async  override Task<int> SaveChangesAsync()
        {
            applyCorrectYeKe();
            auditFields();
            return await base.SaveChangesAsync();
        }
        public override int SaveChanges()
        {
            applyCorrectYeKe();
            auditFields();
            return base.SaveChanges();
        }

        private void applyCorrectYeKe()
        {
            //پیدا کردن موجودیت‌های تغییر کرده
            var changedEntities = this.ChangeTracker
                                      .Entries()
                                      .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var item in changedEntities)
            {
                if (item.Entity == null) continue;

                //یافتن خواص قابل تنظیم و رشته‌ای این موجودیت‌ها
                var propertyInfos = item.Entity.GetType().GetProperties(
                    BindingFlags.Public | BindingFlags.Instance
                    ).Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                var pr = new PropertyReflector();

                //اعمال یکپارچگی نهایی
                foreach (var propertyInfo in propertyInfos)
                {
                    var propName = propertyInfo.Name;
                    var val = pr.GetValue(item.Entity, propName);
                    if (val != null)
                    {
                        var newVal = val.ToString().Replace("ي", "ی").Replace("ؤ", "و").Replace("ة", "ه");
                        //var newVal = val.ToString().Replace("ی", "ی").Replace("ک", "ک");
                        if (newVal == val.ToString()) continue;
                        pr.SetValue(item.Entity, propName, newVal);
                    }
                }
            }
        }

        private void auditFields()
        {
            // var auditUser = User.Identity.Name; // in web apps
            var auditDate = DateTime.Now;
            foreach (var entry in this.ChangeTracker.Entries<Base>())
            {
                // Note: You must add a reference to assembly : System.Data.Entity
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.createdDate = auditDate;

                        break;

                    case EntityState.Modified:
                        entry.Entity.modifedDate = auditDate;
                        break;
                }
            }
        }


    }
}
