﻿using AlatAuth.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Data.Entity
{
    public class OTPEntity : BaseEntity
    {
        public string OTP { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public OtpStatus Status { get; set; } = OtpStatus.Active;
        public OtpType OtpType { get; set; }
        public OtpChannel OtpChannel { get; set; }
    }
}