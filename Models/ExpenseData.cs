using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.Models
{
    public class ExpenseData
    {
        public IEnumerable<Expense> Expenses { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<ExpenseCategory> ExpenseCategories { get; set; }
    }
}
