using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace istp_laba1
{
    public partial class AbonementTypes
    {
        public AbonementTypes()
        {
            Abonements = new HashSet<Abonements>();
        }
        [Display(Name = "Type id")]
        public int Id { get; set; }
        [Required(ErrorMessage ="enter name of type")]
        [Display(Name ="Type")]
        public string Type { get; set; }

        public virtual ICollection<Abonements> Abonements { get; set; }
    }
}
