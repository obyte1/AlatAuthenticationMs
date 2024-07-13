using AlatAuth.Business.Interface;
using AlatAuth.Common.RepositoryPattern.Interface;
using AlatAuth.Data.DataAccess;
using AlatAuth.Data.Interface;
using System;

namespace AlatAuth.Common.RepositoryPattern.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, ICustomerRepo customerRepo, IOtpRepo otpRepo, IStateRepo stateRepo, ILgaRepo lgaRepo)
        {
            this._context = context;
            this.Otp = otpRepo;
            this.CustomerRepo = customerRepo;
            this.stateRepo = stateRepo;
            this.LgaRepo = lgaRepo;
        }

        public ICustomerRepo CustomerRepo { get; private set; }
        public IOtpRepo Otp { get; private set; }
        public IStateRepo stateRepo { get; private set; }
        public ILgaRepo LgaRepo { get; private set; }


        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public async void Cancel() => await _context.DisposeAsync();
        public void Dispose() => _context.Dispose();
        public async Task BeginTransaction() => await _context.Database.BeginTransactionAsync();
        public async Task CommitTransaction() => await _context.Database.CommitTransactionAsync();
        public async Task RollBack() => await _context.Database.RollbackTransactionAsync();
    }

}
