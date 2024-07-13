using AlatAuth.Business.Interface;
using AlatAuth.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Common.RepositoryPattern.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepo CustomerRepo { get; }
        IOtpRepo Otp { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollBack();
        void Cancel();
    }

}
