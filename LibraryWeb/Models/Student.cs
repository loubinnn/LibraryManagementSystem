using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryWeb.Models
{
    public partial class Student
    {
        public Student()
        {
            LendingTickets = new HashSet<LendingTicket>();
        }

        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public int? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public virtual Sex SexNavigation { get; set; }
        public virtual ICollection<LendingTicket> LendingTickets { get; set; }
    }
}
