using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Expenses
{
    public class CreateModel :  ExpenseCategoriesPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public CreateModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var expense = new Expense();
            expense.ExpenseCategories = new List<ExpenseCategory>();
            PopulateAssignedCategoryData(_context, expense);
            ViewData["DestinationID"] = new SelectList(_context.Set<Destination>(), "ID", "DestinationName");
            return Page();
        }

        [BindProperty]
        public Expense Expense { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newExpense = new Expense();
            if (selectedCategories != null)
            {
                newExpense.ExpenseCategories = new List<ExpenseCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new ExpenseCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newExpense.ExpenseCategories.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Expense>(
            newExpense,
            "Expense",
            i => i.Sum, i => i.Date,
            i => i.DestinationID))
            {
                _context.Expense.Add(newExpense);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedCategoryData(_context, newExpense);
            return Page();

            
        }
    }
}
