using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Data.Dto
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> ErrorMessages { get; set; } = new List<string>();

    }
}
