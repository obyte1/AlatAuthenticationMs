using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Data.Dto
{
    public partial class ApiResponse
    {
        public ApiResponse(string code, string status, string message, object data = null)
        {
            Status = status;
            StatusCode = code;
            Message = message;
            Data = data;
        }
        public object Data { get; set; } = null;
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
    }

    public partial class ApiResponse<T> where T : class
    {
        public ApiResponse(T data, string code, string status, string message)
        {
            Data = data;
            StatusCode = code;
            Status = status;
            Message = message;
        }

        public T Data { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }

}
