using BusinessEntities;
using BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAppAPI.Controllers
{
    [EnableCors("*","*","*")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerMasterServices _customerService;
        #region CustomerController
        public CustomerController()
        {
            _customerService = new CustomerServices();
        }

        public HttpResponseMessage Get()
        {
            var customer = _customerService.GetAllCustomers();
            var customerEntities = customer as List<CustomerEntity> ?? customer.ToList();
            if (customerEntities.Any())
                return Request.CreateResponse(HttpStatusCode.OK, customerEntities);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");
        }

        // GET api/customer
        public HttpResponseMessage Get(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer != null)
                return Request.CreateResponse(HttpStatusCode.OK, customer);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No product found for this id");
        }

        // POST api/product

        [HttpPost]

        public int Post([FromBody] CustomerEntity customerEntity)
        {
            return _customerService.CreateCustomer(customerEntity);
        }

        // PUT api/product/5
        [HttpPut]
        public bool Put(int id, [FromBody]CustomerEntity customerEntity)
        {
            if (id > 0)
            {
                return _customerService.UpdateCustomer(id, customerEntity);
            }
            return false;
        }

        // DELETE api/product/5
        [HttpDelete]
        public bool Delete(int id)
        {
            if (id > 0)
                return _customerService.DeleteCustomer(id);
            return false;
        }
        #endregion

    }
}
