using System.ComponentModel.DataAnnotations;


namespace hotelmanagementsystem.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required] 
        public string Title { get; set; }

        [Required]
        public string RoomNumber { get; set; }

        [Required]
        public string Type { get; set; } // Example: Single, Double, Suite

        [Required]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;

        public string ImageUrl { get; set; } // Optional for room image

        public string Description { get; set; }



    }
}
