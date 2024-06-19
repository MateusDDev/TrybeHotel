namespace TrybeHotel.Models
{
    using System.ComponentModel.DataAnnotations;

    public class City {
        [Key]
        public int CityId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string State { get; set; } = null!;
        public ICollection<Hotel>? Hotels { get; set; }
    }
}