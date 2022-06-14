using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryWeb.Models
{
    public partial class Sex
    {
        public Sex()
        {
            Students = new HashSet<Student>();
        }

        public string Sex1 { get; set; }
        public string SexDetail { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
