using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.Application.Dtos.BankAccount;

namespace TailorProTrack.Application.Dtos.Expenses.PaymentExpense
{
    public record PaymentExpenseDtoAdd
    {
        [Required]
        public int IdExpense { get; set; }
        public int IdPaymentType { get; set; }
        public int? IdBankAccount { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }

    public record PaymentExpenseDtoUpdate : PaymentExpenseDtoAdd
    {
        [Required]
        public int Id { get; set; }
    }

    public record PaymentExpenseDtoGet
    {
        public int Id { get; set; }
        public ExpensesDtoGet Expense { get; set; }
        public string PaymentType { get; set; }
        public BankAccountDtoGet? BankAccount { get; set; }
        public decimal Amount { get; set; }
    }
}
