using AlatAuth.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Data.Dto
{
    public class APIRequestModel
    {
        public ApiType ApiType { get; set; } = ApiType.Get;
        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string xKey { get; set; } = string.Empty;

    }

}
