using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace istp_laba1
{
    public partial class StudentAbonements
    {
        public int Id { get; set; }
       // [Editable(false)]
        [Required(ErrorMessage = "enter student id")]
        [Display(Name = "Student ID")]
        public int StudId { get; set; }
        //[Editable(false)]
        [Display(Name = "Abonement ID")]
        public int AbonId { get; set; }
        //[Editable(false)]
        [Required(ErrorMessage = "choose date of activation")]
        [Display(Name = "Activation date")]
        public DateTime ActivationDate { get; set; }
        [Display(Name = "Abonement type")]
        public virtual Abonements Abon { get; set; }
        [Display(Name = "Student name")]
        public virtual Students Stud { get; set; }
    }
}
