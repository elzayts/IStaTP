using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace istp_laba1
{
    public partial class LessonTeacher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "choose lesson id")]
        [Display(Name = "Lesson id")]
        public int LessonId { get; set; }
        [Required(ErrorMessage = "choose teacher id")]
        [Display(Name = "Teacher Id")]
        public int TeacherId { get; set; }

        public virtual Lessons Lesson { get; set; }
        public virtual Teachers Teacher { get; set; }
    }
}
