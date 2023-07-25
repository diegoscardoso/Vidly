using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class MoviesGenre
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
