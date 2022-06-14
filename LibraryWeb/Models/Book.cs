using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryWeb.Models
{
    public partial class Book
    {
        public Book()
        {
            LendingTickets = new HashSet<LendingTicket>();
        }

        public string BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorId { get; set; }
        public string PubisherId { get; set; }
        public DateTime? PublishDate { get; set; }
        public int? NumberOfpage { get; set; }
        public int? StockAmount { get; set; }
        public int? CurrentAmount { get; set; }
        public string Category { get; set; }

        public virtual Author Author { get; set; }
        public virtual Category CategoryNavigation { get; set; }
        public virtual Publisher Pubisher { get; set; }
        public virtual ICollection<LendingTicket> LendingTickets { get; set; }
    }
}
