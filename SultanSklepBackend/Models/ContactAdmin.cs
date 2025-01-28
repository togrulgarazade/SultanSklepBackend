using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SultanSklepBackend.Models
{
    public class ContactAdmin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime CreateDT { get; set; }
        public string Fullname { get; set; }
    }
}
