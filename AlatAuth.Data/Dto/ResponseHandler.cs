using AlatAuth.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Data.Dto
{
    public static class ResponseHandler
    {
        public static ApiResponse FailureResponse(string code, string message)
        {
            return new ApiResponse(code, ResponseStatus.Failed.ToString(), message);
        }
        public static ApiResponse SuccessResponse(string message, string code, object data)
        {
            return new ApiResponse(code, ResponseStatus.Successful.ToString(), message, data);
        }
        public static ApiResponse SuccessResponse(string message, object data)
        {
            return new ApiResponse(SuccessCodes.DEFAULT_SUCCESS_CODE, ResponseStatus.Successful.ToString(), message, data);
        }
        public static ApiResponse SuccessResponse(string message)
        {
            return new ApiResponse(SuccessCodes.DEFAULT_SUCCESS_CODE, ResponseStatus.Successful.ToString(), message);
        }
    }

}
