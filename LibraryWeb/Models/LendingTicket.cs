using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryWeb.Models
{
    public partial class LendingTicket
    {
        public string LendingTicketId { get; set; }
        public string StudentId { get; set; }
        public DateTime? BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public string Status { get; set; }
        public string BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Status StatusNavigation { get; set; }
        public virtual Student Student { get; set; }
    }
}
