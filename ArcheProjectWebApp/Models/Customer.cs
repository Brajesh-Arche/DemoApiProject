using System;
using System.Collections.Generic;

#nullable disable

namespace ArcheProjectWebApp.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLocation { get; set; }
        public string ProductName { get; set; }
        public string PaymentType { get; set; }
        public DateTime AddDate { get; set; }
        public bool IsActive { get; set; }
    }
}
