using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace istp_laba1
{
    public partial class Teachers
    {
        public Teachers()
        {
            LessonTeacher = new HashSet<LessonTeacher>();
            TeacherStyles = new HashSet<TeacherStyles>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "enter name")]
        public string Name { get; set; }
        

        public virtual ICollection<LessonTeacher> LessonTeacher { get; set; }
        public virtual ICollection<TeacherStyles> TeacherStyles { get; set; }
    }
}
