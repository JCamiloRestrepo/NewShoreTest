using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewShoreTest.Models
{
    public class FlightModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "DepartureStation")]
        public string DepartureStation { get; set; }

        [Required]
        [Display(Name = "ArrivalStation")]
        public string ArrivalStation { get; set; }

        [Required]
        [Display(Name = "DepartureDate")]
        public DateTime DepartureDate { get; set; }

        [ForeignKey("FkTransporte")]
        public TransportModel Transport { get; set; }

        [Display(Name = "FkTransporte")]
        [Required]
        public string FkTransporte { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public string Currency { get; set; }

        public FlightModel()
        {
        }
    }
}
