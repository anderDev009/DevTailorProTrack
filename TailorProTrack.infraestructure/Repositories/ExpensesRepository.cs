

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


        public override void Remove(Expenses entity)
        {
			var paymentsInThisExpense = _context.Set<PaymentExpenses>().Where(x => x.FK_EXPENSE == entity.ID).ToList();
			//borrar los pagos de este gasto
			foreach (var payment in paymentsInThisExpense)
			{
				_context.Set<PaymentExpenses>().Remove(payment);
				_context.SaveChanges();
                if(payment.FK_BANK_ACCOUNT == null && payment.FK_BANK_ACCOUNT > 0)
                {
					BankAccount account = _context.Set<BankAccount>().Find(payment.FK_BANK_ACCOUNT);
					account.CREDIT_AMOUNT = _context.Set<PaymentExpenses>().Where(x => x.FK_BANK_ACCOUNT == payment.FK_BANK_ACCOUNT).Sum(x => x.AMOUNT);
					account.BALANCE = account.DEBIT_AMOUNT - account.CREDIT_AMOUNT;
					_context.Set<BankAccount>().Update(account);
					_context.SaveChanges();
				}

                
			}
			var entityToRemove = GetEntity(entity.ID);
			entityToRemove.REMOVED = true;
			_context.Set<Expenses>().Update(entityToRemove);
			_context.SaveChanges();
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

        public decimal GetAmountPending(int idExpense)
        {
			var expense = _context.Set<Expenses>().Find(idExpense);
			if (expense != null)
			{
				var paymentsExpenses = _context.Set<Expenses>()
					.Include(x => x.PaymentsExpenses)
					.Where(x => x.ID == idExpense)
					.Select(x => x.PaymentsExpenses.Sum(x => x.AMOUNT)).Single();
				return expense.AMOUNT - paymentsExpenses;
			}
			return 0;
        }

        public List<Expenses> GetExpensesWithBuyId()
        {
            return _context.Set<Expenses>().Where(x => x.COMPLETED == false && (x.FK_BUY != null && x.FK_BUY != 0)).ToList();
        }

        public List<Expenses> GetExpensesWithoutBuyId()
        {
            return _context.Set<Expenses>().Where(x => x.COMPLETED == false && (x.FK_BUY == null && x.FK_BUY == 0)).ToList();
        }

        public List<Expenses> GetExpensesWithBuyIdPaginated(int page, int itemsPerPage, bool withBuy)
        {
            if (withBuy)
            {
                return _context.Set<Expenses>().Where(x => x.COMPLETED == false && (x.FK_BUY != null && x.FK_BUY != 0))
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList();
            }
            else
            {
                return _context.Set<Expenses>().Where(x => x.COMPLETED == false && (x.FK_BUY == null || x.FK_BUY == 0))
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList();
            }
        }

        public List<Expenses> GetBuysPending()
        {
            List<Expenses> expenses = _context.Set<Expenses>().Where(x => x.COMPLETED == false && !x.REMOVED && x.FK_BUY != null).ToList();
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

        public List<Expenses> GetOnlyExpensesPending()
        {
            List<Expenses> expenses = _context.Set<Expenses>().Where(x => x.COMPLETED == false && !x.REMOVED && x.FK_BUY == null).ToList();
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

        public List<Expenses> GetExpensesByDate(DateTime startDate, DateTime endDate)
        {
            var expenses = _context.Set<Expenses>().Where(x => x.CREATED_AT >= startDate && x.CREATED_AT <= endDate && x.FK_BUY == null).ToList();
            return expenses;
        }

        public List<Expenses> GetBuysByDate(DateTime startDate, DateTime endDate)
        {
            var expenses = _context.Set<Expenses>().Where(x => x.CREATED_AT >= startDate && x.CREATED_AT <= endDate && x.FK_BUY != null).ToList();
            return expenses;
        }
    }
}
