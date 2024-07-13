using AlatAuth.Common.RepositoryPattern.Implementation;
using AlatAuth.Data.DataAccess;
using AlatAuth.Data.Entity;
using AlatAuth.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Business.Interface
{
    public class StateRepo : Repository<State>, IStateRepo
    {
        public StateRepo(AppDbContext context) : base(context)
        {
        }
    }

}
