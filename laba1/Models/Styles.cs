using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace istp_laba1
{
    public partial class Styles
    {
        public Styles()
        {
            Lessons = new HashSet<Lessons>();
            TeacherStyles = new HashSet<TeacherStyles>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "enter style name")]
        public string Name { get; set; }

        public virtual ICollection<Lessons> Lessons { get; set; }
        public virtual ICollection<TeacherStyles> TeacherStyles { get; set; }
    }
}
