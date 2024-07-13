using AlatAuth.Common.RepositoryPattern.Implementation;
using AlatAuth.Common.RepositoryPattern.Interface;
using AlatAuth.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Business.Interface
{
    public interface IOtpRepo : IRepository<OTPEntity>
    {
    }
}
