using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public MovieFormType MovieFormType { get; set; }
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public string? Description { get; set; } 
        
        [Display(Name = "Release Date")]
        [Required(ErrorMessage = "Release Date is required.")]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        [Display(Name = "Genre")]
        public int? GenreId { get; set; }

        public IEnumerable<MoviesGenre> MoviesGenres { get; set; } = Enumerable.Empty<MoviesGenre>();

        [Display(Name = "Number in Stock")]
        [Range(1,20)]
        [Required]
        public int StockQuantity { get; set; }
    }

    public enum MovieFormType : int
    {
        Add = 1,
        Edit = 2
    }
}
