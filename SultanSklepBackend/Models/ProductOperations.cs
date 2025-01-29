using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SultanSklepBackend.Models
{
    public class ProductOperations
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public int? AddressID { get; set; }
        public int ProductID { get; set; }
        public string OperationNumber { get; set; }
        public string OrderNotes { get; set; }
        public int Count { get; set; }
        public bool InCart { get; set; }
        public bool IsOrdered { get; set; }
        public bool IsPending { get; set; }
        public bool IsOnTheWay { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Address Address { get; set; }
        public virtual Product Product { get; set; }
    }
}
