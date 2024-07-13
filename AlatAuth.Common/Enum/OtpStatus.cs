using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Common.Enum
{
    public enum OtpStatus
    {
        [Description("Active")]
        Active = 1,
        [Description("Verified")]
        Verified = 2,
        [Description("Expired")]
        Expired = 3
    }

    public enum OtpType
    {
        [Description("Account Verification")]
        Account = 1,
        [Description("Transaction Verification")]
        Transaction = 2,
        [Description("Reset Password")]
        Password = 3,
        [Description("BVN Validation")]
        BVNValidation = 4,
        [Description("Email Verification")]
        EmailVerification = 5,
    }

    public enum OtpChannel
    {
        [Description("SMS")]
        Sms = 1,
        [Description("Email")]
        Email = 2,
        [Description("WhatsApp")]
        WhatsApp = 3,
    }

}
