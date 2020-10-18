using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewShoreTest.Models
{
    public class TransportModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "FlightNumber")]
        public string FlightNumber { get; set; }

        public TransportModel()
        {

        }
    }
}