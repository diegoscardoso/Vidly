using Microsoft.Build.Framework;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public virtual MoviesGenre Genre { get; set; }
        
        [Required]
        public int MoviesGenreId { get; set; }
        
        [Required]
        public DateTime DataAdded { get; set; }
        
        [Required]
        public DateTime ReleaseDate { get; set; }
        
        [Required]
        public int StockQuantity { get; set;}
    }
}
