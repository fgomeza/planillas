using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Repository.Repositories.Classes;

namespace Repository.Context
{
    
    public class AppContext: DbContext
    {
        public AppContext(string context) : base(nameOrConnectionString: context)
        {
            
        }
        public DbSet<VacationEntity> Vacations { get; set; }
        public DbSet<RoleOperationEntity> RoleOperations { get; set; }
        public DbSet<PenaltyTypeEntity> PenaltyTypes { get; set; }
        public DbSet<PenaltyEntity> Penalties { get; set; }
        public DbSet<AdministratorEntity> Administrators { get; set; }
        public DbSet<CallEntity> Calls { get; set; }
        public DbSet<DebitEntity> Debits { get; set; }
        public DbSet<DebitTypeEntity> DebitTypes { get; set; }
        public DbSet<DebitPaymentEntity> DebitPayments { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<ExtraEntity> Extras { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<OperationEntity> Operations { get; set; }
        public DbSet<PayrollEntity> Payrolls { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<SalaryEntity> Salaries { get; set; }
        public DbSet<SavingsEntity> Savings { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        
    }
}
