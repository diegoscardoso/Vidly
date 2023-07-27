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

        public IActionResult Details(int id)
        {
            var customer = _context.Customers.Include(o => o.MembershipType).SingleOrDefault(x => x.Id == id);

            if (customer == null)
                return NotFound();
           
            return View(customer);
        }

        public IActionResult Add()
        {
            var membershipTypes = _context.MembershipType.ToList();
            
            return View(new AddCustomerModelView { 
                  MembershipTypes = membershipTypes
            });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Customers");
        }
    }
}
