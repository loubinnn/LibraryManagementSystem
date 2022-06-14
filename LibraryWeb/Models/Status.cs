using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryWeb.Models
{
    public partial class Status
    {
        public Status()
        {
            LendingTickets = new HashSet<LendingTicket>();
        }

        public string Status1 { get; set; }
        public string StatusDetail { get; set; }

        public virtual ICollection<LendingTicket> LendingTickets { get; set; }
    }
}
