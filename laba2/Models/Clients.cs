using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace istatp_lab2.Models
{
    public class DateAttribute : RangeAttribute
    {
        public DateAttribute()
          : base(typeof(DateTime), DateTime.Now.AddYears(-90).ToShortDateString(), DateTime.Now.AddYears(-18).ToShortDateString()) { }
    }
    public class Clients
    {

        public Clients()
        {
            ClientRooms = new List<ClientRoom>();
        }
        public int ClientID { get; set; }
        [Required(ErrorMessage = "enter name")]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "enter last name")]
        [StringLength(50)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "enter birth date")]
        [Display(Name = "Birth date")]
        [Date()]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "enter email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        public virtual ICollection<ClientRoom> ClientRooms{ get; set; }
    }
}
