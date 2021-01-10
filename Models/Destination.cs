using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.Models
{
    public class Destination
    {
        public int ID { get; set; }
        public string DestinationName { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
