using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Models;
using Vidly.ViewModels;

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

        public IActionResult Add()
        {

            var genres = _context.Genres.ToList();

            var model = new MovieFormViewModel
            {
                MovieFormType = MovieFormType.Add,
                MoviesGenres = genres
            };

            return View("MoviesForm", model);
        }

        public IActionResult Edit(int id) 
        {
            var movie = _context.Movies.Include(o => o.Genre).FirstOrDefault(x => x.Id == id);

            if (movie == null)
                return NotFound();
            
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                MovieFormType = MovieFormType.Edit,
                MoviesGenres = genres,
                Name = movie.Name, 
                Description = movie.Description,
                GenreId = movie.MoviesGenreId,
                ReleaseDate = movie.ReleaseDate,
                StockQuantity = movie.StockQuantity
            };

            return View("MoviesForm", viewModel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Save(MovieFormViewModel movie) {

            if (ModelState.IsValid)
            {
                if (movie.Id == 0)
                {
                    _context.Movies.Add(new Models.Movie
                    {
                        Name = movie.Name,
                        Description = movie.Description,
                        DataAdded = DateTime.Now,
                        MoviesGenreId = movie.GenreId.Value,
                        ReleaseDate = movie.ReleaseDate.Value,
                        StockQuantity = movie.StockQuantity
                    });
                }
                else
                {
                    var m = await _context.Movies.SingleAsync(o => o.Id == movie.Id);
                    m.Name = movie.Name;
                    m.Description = movie.Description;
                    m.MoviesGenreId = movie.GenreId.Value;
                    m.ReleaseDate = movie.ReleaseDate.Value;
                    m.StockQuantity = movie.StockQuantity;
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                var genres = _context.Genres.ToList();
                movie.MoviesGenres = genres;
                return View("MoviesForm", movie);
            }
        }

        [Route(@"movies/released/{year:regex(\d{{4}}):range(2000,2040)}/{month:regex(\d{{2}}):range(1,12)}")]
        public IActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        #endregion
    }
}

