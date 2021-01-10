using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Expenses
{
    public class IndexModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public IndexModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IList<Expense> Expense { get; set; }
        public ExpenseData ExpenseD { get; set; }
        public int ExpenseID { get; set; }
        public int CategoryID { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID)
        {
            ExpenseD = new ExpenseData();

            ExpenseD.Expenses = await _context.Expense
            .Include(b => b.Destination)
            .Include(b => b.ExpenseCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.Date)
            .ToListAsync();
            if (id != null)
            {
                ExpenseID = id.Value;
                Expense expense = ExpenseD.Expenses
                .Where(i => i.ID == id.Value).Single();
                ExpenseD.Categories = expense.ExpenseCategories.Select(s => s.Category);
            }
        }
    }
}
