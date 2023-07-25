using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieIndexViewModel
    {
        public List<Movie> Movies { get; set; }

        [Required(ErrorMessage = "The Display From (Time) Field is required")]
        [Display(Name = "Display From Time")]
        [DataType(DataType.Time)]
        public DateTime DisplayFromTime { get; set; }

        [Required(ErrorMessage = "The Display From (Date) Field is required")]
        [Display(Name = "Display From Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DisplayFromDate { get; set; }

        [Required(ErrorMessage = "The Display To (Time) Field is required")]
        [Display(Name = "Display To Time")]
        [DataType(DataType.Time)]
        public DateTime DisplayToTime { get; set; }

        [Required(ErrorMessage = "The Display To (Date) Field is required")]
        [Display(Name = "Display To Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DisplayToDate { get; set; }

        [Required(ErrorMessage = "You must enter something in the Message Title")]
        [Display(Name = "Message Title")]
        [StringLength(30, ErrorMessage = "{0} cannot exceed {1} characters")]
        public string MessageTitle { get; set; }

        [Required(ErrorMessage = "You must enter something in the Message Body")]
        [Display(Name = "Message Body")]
        [StringLength(1000, ErrorMessage = "{0} cannot exceed {1} characters")]
        public string MessageBody { get; set; }

        [Display(Name = "Only Show on Contact Us Page")]
        public bool ContactUsOnly { get; set; }

        public MovieIndexViewModel()
        {   
            this.Movies = new List<Movie>();    
        }
    }

}
