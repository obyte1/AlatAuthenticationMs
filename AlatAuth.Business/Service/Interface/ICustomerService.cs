using AlatAuth.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Business.Service.Interface
{
    public interface ICustomerService
    {
        Task<dynamic> CreateCustomer(CustomerRequest request);
        Task<dynamic> GetCustomers(int pageNumber, int pageSize);
    }
}
