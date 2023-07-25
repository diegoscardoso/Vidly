using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Models;

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

        [HttpGet]
        public IActionResult Details(int id)
        {
            var customer = _context.Customers.Include(o => o.MembershipType).SingleOrDefault(x => x.Id == id);

            if (customer == null)
                return NotFound();
           
            return View(customer);
        }
    }
}
