﻿
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IExpensesRepository : IBaseRepository<Expenses>
    {
        //public List<Expenses> GetAccountsPayable();
        public void ConfirmExpenses(int idExpense);
        public bool ExpenseIsPending (int idExpense);
        public List<Expenses> GetExpensesPending();
        public List<Expenses> GetBuysPending();
        public List<Expenses> GetOnlyExpensesPending();
        public List<Expenses> GetExpensesWithBuyId();
        public List<Expenses> GetExpensesWithoutBuyId();
        public List<Expenses> GetExpensesWithBuyIdPaginated(int page, int itemsPerPage, bool withBuy);
        public List<Expenses> GetExpensesByDate(DateTime startDate,DateTime endDate);
        public List<Expenses> GetBuysByDate(DateTime startDate, DateTime endDate);

        public decimal GetAmountPending(int idExpense);
    }
}
