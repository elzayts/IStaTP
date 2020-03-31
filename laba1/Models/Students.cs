using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace istp_laba1
{
    public partial class Students
    {
        public Students()
        {
            StudentAbonements = new HashSet<StudentAbonements>();
        }

        public int Id { get; set; }
        [Display(Name = "Profile description")]
        public string ProfileDescription { get; set; }
        [Required(ErrorMessage ="enter name!!!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "enter surname!!!")]
        [Display(Name ="Surname")]
        public string Photo { get; set; }

        public virtual ICollection<StudentAbonements> StudentAbonements { get; set; }
    }
}
