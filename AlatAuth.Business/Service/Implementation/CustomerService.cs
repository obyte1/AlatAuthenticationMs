using AlatAuth.Business.Service.Interface;
using AlatAuth.Common.RepositoryPattern.Interface;
using AlatAuth.Data.Dto;
using AlatAuth.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Business.Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;

        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<dynamic> CreateCustomer(CustomerRequest request)
        {
            var customer =  _repository.Find(x=>x.PhoneNumber == request.PhoneNumber);
            if (customer != null)
            {
                return BadRequest(new { Message = "Customer with this phone number already exists." });
            }
        }
    }
}
