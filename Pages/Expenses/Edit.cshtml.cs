using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Expenses
{
    public class EditModel : ExpenseCategoriesPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public EditModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Expense Expense { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                if (id == null)
                {
                    return NotFound();
                }
                Expense = await _context.Expense.FirstOrDefaultAsync(m => m.ID == id);
                if (Expense == null)
                {
                    return NotFound();
                }
                ViewData["DestinationID"] = new SelectList(_context.Set<Destination>(), "ID", "DestinationName");
                return NotFound();
            }

            Expense = await _context.Expense
                .Include(b => b.Destination)
                .Include(b => b.ExpenseCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Expense == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Expense);

            ViewData["DestinationID"] = new SelectList(_context.Destination, "ID", "DestinationName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var expenseToUpdate = await _context.Expense
            .Include(i => i.Destination)
            .Include(i => i.ExpenseCategories)
            .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (expenseToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Expense>(
            expenseToUpdate,
            "Expense",
            i => i.Sum, i => i.Date,
            i => i.Destination))
            {
                UpdateExpenseCategories(_context, selectedCategories, expenseToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Apelam UpdateExpenseCategories pentru a aplica informatiile din checkboxuri la entitatea Expenses care
            //este editata
            UpdateExpenseCategories(_context, selectedCategories, expenseToUpdate);
            PopulateAssignedCategoryData(_context, expenseToUpdate);
            return Page();
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expense.Any(e => e.ID == id);
        }
    }
}
