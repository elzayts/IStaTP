using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace istatp_lab2.Models
{
    public class Rooms
    {
        public Rooms()
        {
            ClientRooms = new List<ClientRoom>();
        }
        public int RoomID { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Number")]
        [Range(1, 500, ErrorMessage = "must be in range 1 .. 500")]
        [Required(ErrorMessage = "enter room number")]
        public int Number { get; set; }

        [Display(Name = "Price per night")]
        [Range(1, 5000, ErrorMessage = "must be in range 1 .. 5000")]
        [Required(ErrorMessage = "enter price")]
        public int Price { get; set; }

        public virtual ICollection<ClientRoom> ClientRooms { get; set; }
    }
}
