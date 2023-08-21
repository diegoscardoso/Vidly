using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Vidly.Data;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly VidlyDbContext _dataBase;
        public CustomersController(VidlyDbContext vidlyDbContext)
        {
            _dataBase = vidlyDbContext; 
        }

        //GET api/customers
        [HttpGet]
        public async Task<IActionResult> GetCustomers(CancellationToken cancellationToken) {

            var customers = await _dataBase.Customers.ToListAsync(cancellationToken);
            return Ok(customers);
        }

        //GET api/customers
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomers(int customerId, CancellationToken cancellationToken)
        {
            var customer = await _dataBase.Customers.FindAsync(customerId, cancellationToken);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // POST /api/customer
        [HttpPost]
        public async Task<IActionResult> PostCustomers(Customer customer, CancellationToken cancellationToken) {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerAdded = await _dataBase.Customers.AddAsync(customer, cancellationToken);

            _ = _dataBase.SaveChangesAsync(cancellationToken);

            return Ok(customerAdded);
        }

        //PUT /api/customers
        [HttpPut]
        public async Task<IActionResult> PutCustomers(Customer customer, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerInDb = await _dataBase.Customers.FindAsync(customer.Id, cancellationToken);

            if ( customerInDb == null) 
                return NotFound(customer.Id);

            customerInDb.Name = customer.Name;
            customerInDb.BirthDate = customer.BirthDate;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;

            _ = _dataBase.SaveChangesAsync(cancellationToken);

            return Ok(customerInDb);
        }

        //DELETE
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Customer customer, CancellationToken cancellationToken) 
        {
            var customerInDb = await _dataBase.Customers.FindAsync(customer.Id);

            if (customerInDb == null)
                return NotFound(customer.Id);

            _ =  _dataBase.Customers.Remove(customerInDb);
            _ = await _dataBase.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
