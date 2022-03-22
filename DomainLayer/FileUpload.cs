using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public class FileUpload
    {
        [Key]
        public int RecordId { get; set; }
        public string FullName{ get; set; }
        public string SelfMobileNomber { get; set; }
        public string Email { get; set; }
        public string FamilyMobileNumber { get; set; }
        public string FriendMobileNumber { get; set; }

    }
}
