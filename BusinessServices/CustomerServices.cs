using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;

namespace BusinessServices
{
    public class CustomerServices : ICustomerMasterServices
    {
        private readonly UnitOfWork _unitOfWork;
        public CustomerServices()
        {
            _unitOfWork = new UnitOfWork();
        }
        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="customerEntity"></param>
        /// <returns></returns>
        public int CreateCustomer(CustomerEntity customerEntity)
        {
            using (var scope = new TransactionScope())
            {
                var customer = new customermaster
                {
                    CustomerName = customerEntity.CustomerName,
                    ContacntNo = customerEntity.ContacntNo,
                    EmailId = customerEntity.EmailId,
                    GSTIN = customerEntity.GSTIN,
                    PAN = customerEntity.PAN,
                    TIN = customerEntity.TIN,
                    Country = customerEntity.Country,
                    State = customerEntity.State,
                    City = customerEntity.City,
                    Zip = customerEntity.Zip

                };
                _unitOfWork.CustomerRepository.Insert(customer);
                _unitOfWork.Save();
                scope.Complete();
                return customer.CustomerID;
            }

        }

        public bool DeleteCustomer(int CustomerId)
        {
            var success = false;
            if (CustomerId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.CustomerRepository.GetByID(CustomerId);
                    if (product != null)
                    {

                        _unitOfWork.CustomerRepository.Delete(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<CustomerEntity> GetAllCustomers()
        {
            var customers = _unitOfWork.CustomerRepository.GetAll().ToList();
            if (customers.Any())
            {
                Mapper.CreateMap<customermaster, CustomerEntity>();
                var customerModel = Mapper.Map<List<customermaster>, List<CustomerEntity>>(customers);
                return customerModel;
            }
            return null;
        }

        public CustomerEntity GetCustomerById(int CustomerId)
        {
            var customer = _unitOfWork.CustomerRepository.GetByID(CustomerId);
            if (customer != null)
            {
                Mapper.CreateMap<customermaster, CustomerEntity>();
                var customerModel = Mapper.Map<customermaster, CustomerEntity>(customer);
                return customerModel;
            }
            return null;
        }

        public bool UpdateCustomer(int CustomerId, CustomerEntity customerEntity)
        {
            var success = false;
            if (customerEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var customer = _unitOfWork.CustomerRepository.GetByID(CustomerId);
                    if (customer != null)
                    {
                        customer.CustomerName = customerEntity.CustomerName.Trim();
                        customer.ContacntNo = customerEntity.ContacntNo.Trim();
                        customer.EmailId = customerEntity.EmailId.Trim();
                        customer.GSTIN = customerEntity.GSTIN.Trim();
                        customer.PAN = customerEntity.PAN.Trim();
                        customer.TIN = customerEntity.TIN.Trim();
                        customer.Country = customerEntity.Country.Trim();
                        customer.State = customerEntity.State.Trim();
                        customer.City = customerEntity.City.Trim();
                        customer.Zip = customerEntity.Zip.Trim();
                        _unitOfWork.CustomerRepository.Update(customer);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
