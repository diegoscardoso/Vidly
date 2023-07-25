using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        #region Private Fieds
        private readonly VidlyDbContext _context;
        #endregion

        #region Constructors
        public MoviesController(VidlyDbContext dbContext)
        {
            _context = dbContext ;
        }
        #endregion

        #region Public Methods
        public IActionResult Index()
        {
            var _movies = _context.Movies.Include(o=>o.Genre).ToList();
            
            return View(_movies);
        }

        public IActionResult Details(int id) 
        {
            var movie = _context.Movies.Include(o => o.Genre).FirstOrDefault(x => x.Id == id);

            return View(movie);
        }

        [Route(@"movies/released/{year:regex(\d{{4}}):range(2000,2040)}/{month:regex(\d{{2}}):range(1,12)}")]
        public IActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
        #endregion
    }
}

