using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace istp_laba1
{
    public partial class Classrooms
    {
        public Classrooms()
        {
            Lessons = new HashSet<Lessons>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "enter classroom name")]
        public string Name { get; set; }

        public virtual ICollection<Lessons> Lessons { get; set; }
    }
}
