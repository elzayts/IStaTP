using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace istp_laba1
{
    public partial class Abonements
    {
        public Abonements()
        {
            StudentAbonements = new HashSet<StudentAbonements>();
        }
        [Display(Name = "Abonement id")]
        public int Id { get; set; }
        [Required(ErrorMessage ="choose type")]
        [Display(Name = "Abonement type")]
        public int TypeId { get; set; }

        public virtual AbonementTypes Type { get; set; }
        public virtual ICollection<StudentAbonements> StudentAbonements { get; set; }
    }
}
