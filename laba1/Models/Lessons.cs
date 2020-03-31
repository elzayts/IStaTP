using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace istp_laba1
{
    public partial class Lessons
    {
        public Lessons()
        {
            LessonTeacher = new HashSet<LessonTeacher>();
        }

        public int Id { get; set; }
        public int ClassroomId { get; set; }
        [Required(ErrorMessage = "enter date")]
        [Display(Name ="Date&Time")]
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int StyleId { get; set; }

        [Required(ErrorMessage = "choose classroom")]
        [Display(Name = "Classroom")]
        public virtual Classrooms Style { get; set; }
        [Required(ErrorMessage = "choose style")]
        [Display(Name = "Style")]
        public virtual Styles StyleNavigation { get; set; }
        public virtual ICollection<LessonTeacher> LessonTeacher { get; set; }
    }
}
