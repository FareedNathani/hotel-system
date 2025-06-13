using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hotelmanagementsystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        [Required]
        public DateTime ArrivalDate { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public int Rooms { get; set; }

        [Required]
        public int Guests { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Note { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }

}