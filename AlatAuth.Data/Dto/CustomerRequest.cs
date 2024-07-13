using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Data.Dto
{
    public class CustomerRequest
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StateOfResidence { get; set; }
        public string LGA { get; set; }

    }
}
