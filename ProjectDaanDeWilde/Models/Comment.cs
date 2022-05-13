using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDaanDeWilde.Models
{
    public class Comment : IComparable
    {
        public Guid CommentId { get; set; }
        public int ChargerId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CommentText { get; set; }
        public DateTime DateAndTime { get; set; }

        public string Date { get {
                return this.DateAndTime.ToString("dd/MM/yyyy");
            } }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Comment ander = (Comment)obj;
            return this.DateAndTime.CompareTo(ander.DateAndTime);
        }
    }
}
