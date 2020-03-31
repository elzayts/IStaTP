using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace istp_laba1
{
    public partial class TeacherStyles
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "enter teacher id")]
        [Display(Name ="Teacher Id")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "enter style id")]
        [Display(Name = "Style Id")]
        public int StyleId { get; set; }

        public virtual Styles Style { get; set; }
        public virtual Teachers Teacher { get; set; }
    }
}
