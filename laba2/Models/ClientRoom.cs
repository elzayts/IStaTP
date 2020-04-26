using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace istatp_lab2.Models
{
    public class DateAttribute1 : RangeAttribute
    {
        public DateAttribute1()
          : base(typeof(DateTime), DateTime.Now.ToShortDateString(), DateTime.Now.AddMonths(6).ToShortDateString()) { }
    }
   
    public class ClientRoom
    {
        
        public ClientRoom()
        {
            Orders = new List<Orders>();
            Fines = new List<Fines>();
        }
        [Display(Name = "Client-Room ID")]
        public int ClientRoomID { get; set; }

        [Display(Name = "Client id")]
        public int ClientID { get; set; }
        [Display(Name = "Room id")]
        public int RoomID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check in date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateAttribute1]
        public DateTime Check_inDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check out date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Check_outDate { get; set; }
        

        public virtual Clients Client { get; set; }
        public virtual Rooms Room { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Fines> Fines { get; set; }
    }
}
