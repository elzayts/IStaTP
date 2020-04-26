using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace istatp_lab2.Models
{
    public class Orders
    {
        public Orders()
        {
            ClientRooms = new List<ClientRoom>();
        }
        public int FinesID { get; set; }

        [Required(ErrorMessage = "enter name")]
        [Display(Name = "Name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Range(1, 1000, ErrorMessage = "must be in range 1 .. 1000")]
        [Required(ErrorMessage = "enter price")]
        [Display(Name = "Price")]
        public int Price { get; set; }

        public virtual ICollection<ClientRoom> ClientRooms { get; set; }
    }
}
