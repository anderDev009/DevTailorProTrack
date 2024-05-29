

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
    }
}
