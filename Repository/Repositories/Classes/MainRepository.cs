using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Context;
using Repository.Entities;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Classes
{
    public class MainRepository : IDisposable
    {
        private readonly AppContext _context;

        public CallRepository Calls { get; }
        public ErrorRepository Errors { get; }
        public UserRepository Users { get; }
        public LocationRepository Locations { get; }
        public GroupRepository Groups { get; }
        public OperationRepository Operations { get; }
        public RoleRepository Roles { get; }
        public DebitRepository Debits { get; }
        public PenaltyRepository Penalties { get; }
        public PenaltyTypeRepository PenaltyTypes { get; }
        public EmployeeRepository Employees { get; }
        public ExtraRepository Extras { get; }
        public PayRollRepository PayRolls { get; }
        public SalaryRepository Salarys { get; }
        public DebitTypeRepository DebitTypes { get; }
        public DebitPaymentRepository DebitPayments { get; }
        public AdministratorRepository Administrators { get; }

        public MainRepository(AppContext context)
        {
            _context = context;
            Calls = new CallRepository(_context);
            Errors = new ErrorRepository(_context);
            Users = new UserRepository(_context);
            Locations = new LocationRepository(_context);
            Groups = new GroupRepository(_context);
            Operations = new OperationRepository(_context);
            Roles = new RoleRepository(_context);
            Debits = new DebitRepository(_context);
            PenaltyTypes = new PenaltyTypeRepository(_context);
            Employees = new EmployeeRepository(_context);
            Extras = new ExtraRepository(_context);
            PayRolls = new PayRollRepository(_context);
            Salarys = new SalaryRepository(_context);
            DebitTypes = new DebitTypeRepository(_context);
            Penalties = new PenaltyRepository(_context);
            DebitPayments = new DebitPaymentRepository(_context);
            Administrators = new AdministratorRepository(_context);
        }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public AppContext GetContext()
        {
            return _context;
        }
    }
}
