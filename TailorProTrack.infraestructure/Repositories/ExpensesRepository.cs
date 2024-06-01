

using System.Linq;
using Microsoft.EntityFrameworkCore;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class ExpensesRepository : BaseRepository<Expenses>, IExpensesRepository    
    {
        private readonly TailorProTrackContext _context;
        private readonly IBankAccountRepository _bankAccountRepository;
        public ExpensesRepository(TailorProTrackContext ctx, IBankAccountRepository bankAccountRepository) : base(ctx)
        {
            _context = ctx;
            _bankAccountRepository = bankAccountRepository;
        }

  
     
        public void ConfirmExpenses(int idExpense)
        {
            Expenses expense = this.GetEntity(idExpense);
            if (expense != null)
            {
                //completar el gasto
                expense.COMPLETED = true;
                this.Update(expense);
            }
         
        }

        public bool ExpenseIsPending(int idExpense)
        {
            Expenses expense = _context.Set<Expenses>().Find(idExpense);
            if (expense != null)
            {
                //pagos que le han realizado a ese gasto
                var paymentsExpenses = _context.Set<Expenses>()
                    .Include(x => x.PaymentsExpenses)
                    .Where(x => x.ID == idExpense)
                    .Select(x => x.PaymentsExpenses.Sum(x => x.AMOUNT)).Single();

                if (expense.AMOUNT > paymentsExpenses)
                {
                    return true;
                }
            }

            return false;
        }

        public List<Expenses> GetExpensesPending()
        {
            List<Expenses> expenses = _context.Set<Expenses>().Where(x => x.COMPLETED == false && !x.REMOVED).ToList();
            List<Expenses> expensesPending = new List<Expenses>();
            foreach (var expense in expenses)
            {
                bool isPending = ExpenseIsPending(expense.ID);
                if (isPending)
                {
                    expensesPending.Add(expense);
                }
                else
                {
                    this.ConfirmExpenses(expense.ID);
                }
            }
            return expensesPending;
        }
    }
}
