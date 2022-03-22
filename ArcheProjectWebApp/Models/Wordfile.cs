using System;
using System.Collections.Generic;

#nullable disable

namespace ArcheProjectWebApp.Models
{
    public partial class Wordfile
    {
        public int RecordId { get; set; }
        public string FullName { get; set; }
        public string SelfMobileNomber { get; set; }
        public string Email { get; set; }
        public string FamilyMobileNumber { get; set; }
        public string FriendMobileNumber { get; set; }
    }
}
