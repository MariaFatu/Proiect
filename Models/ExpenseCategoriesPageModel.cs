using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect.Data;


namespace Proiect.Models
{
    public class ExpenseCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(ProiectContext context,
        Expense expense)
        {
            var allCategories = context.Category;
            var expenseCategories = new HashSet<int>(
            expense.ExpenseCategories.Select(c => c.CategoryID));
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = expenseCategories.Contains(cat.ID)
                });
            }
        }
        public void UpdateExpenseCategories(ProiectContext context,
        string[] selectedCategories, Expense expenseToUpdate)
        {
            if (selectedCategories == null)
            {
                expenseToUpdate.ExpenseCategories = new List<ExpenseCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var expenseCategories = new HashSet<int>
            (expenseToUpdate.ExpenseCategories.Select(c => c.Category.ID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!expenseCategories.Contains(cat.ID))
                    {
                        expenseToUpdate.ExpenseCategories.Add(
                        new ExpenseCategory
                        {
                            ExpenseID = expenseToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (expenseCategories.Contains(cat.ID))
                    {
                        ExpenseCategory courseToRemove
                        = expenseToUpdate
                        .ExpenseCategories
                        .SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }

}
