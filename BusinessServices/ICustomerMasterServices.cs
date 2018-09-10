using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices
{
    public interface ICustomerMasterServices
    {
        CustomerEntity GetCustomerById(int CustomerId);
        IEnumerable<CustomerEntity> GetAllCustomers();
        int CreateCustomer(CustomerEntity customerEntity);
        bool UpdateCustomer(int CustomerId, CustomerEntity customerEntity);
        bool DeleteCustomer(int CustomerId);

   }
}
