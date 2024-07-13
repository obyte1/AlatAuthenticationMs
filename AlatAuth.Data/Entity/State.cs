using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Data.Entity
{
    public class State : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<LGA> LGAs { get; set; }
    }
}
