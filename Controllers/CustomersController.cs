using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private readonly VidlyDbContext _context;
        
        public CustomersController(VidlyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var customer = _context.Customers.Include(c=>c.MembershipType).ToList();
            
            return View(customer);
        }

        public IActionResult Add()
        {
            var membershipTypes = _context.MembershipType.ToList();
            
            return View("CustomerForm", new CustomerFormViewModel { 
                  MembershipTypes = membershipTypes
            });
        }

        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(o => o.Id == id);

            if (customer == null) 
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipType.ToList()
            }; 

            return View("CustomerForm", viewModel);  
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Save(Customer customer)
        {
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var c = _context.Customers.Single(o => o.Id == customer.Id);
                c.Name = customer.Name;
                c.BirthDate = customer.BirthDate;
                c.MembershipTypeId = customer.MembershipTypeId;
                c.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index", "Customers");
        }
    }
}
