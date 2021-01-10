using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect.Models
{
    public class Expense
    {
        public int ID { get; set; }

        [Range(1, 10000)]
        [Column(TypeName = "decimal(6, 2)")]
        [Display(Name = "Transaction value")]
        public int Sum { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Transaction date")]
        public DateTime Date { get; set; }
        public int DestinationID { get; set; }
        public Destination Destination { get; set; }
        public ICollection<ExpenseCategory> ExpenseCategories { get; set; }

        

    }
}
