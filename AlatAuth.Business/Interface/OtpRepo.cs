using AlatAuth.Common.RepositoryPattern.Implementation;
using AlatAuth.Data.DataAccess;
using AlatAuth.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Business.Interface
{
    public class OtpRepo : Repository<OTPEntity>, IOtpRepo
    {
        public OtpRepo(AppDbContext context) : base(context)
        {
        }
    }
}
