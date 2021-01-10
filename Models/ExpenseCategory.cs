using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.Models
{
    public class ExpenseCategory
    {
        public int ID { get; set; }
        public int ExpenseID { get; set; }
        public Expense Expense { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
