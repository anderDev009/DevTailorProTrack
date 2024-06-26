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
        public decimal GetAmountPending(int idExpense);
    }
}
