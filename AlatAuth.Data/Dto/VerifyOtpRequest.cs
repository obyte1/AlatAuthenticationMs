using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Data.Dto
{
    public class VerifyOtpRequest
    {
        public int OtpId { get; set; }
        public string Otp { get; set; }
    }
}
