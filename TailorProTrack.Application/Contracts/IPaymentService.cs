﻿

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Payment;

namespace TailorProTrack.Application.Contracts
{
    public interface IPaymentService : IBaseService<PaymentDtoAdd,PaymentDtoRemove,PaymentDtoUpdate>
    {
        ServiceResult GetPaymentsByOrderId(int orderId);
        decimal GetAmountByIdOrder(int orderId);
        ServiceResult AddPaymentUsingNoteCredits(PaymentDtoAddWithNoteCredit dtoAdd);
        ServiceResult DetailBankAccount(int id);
    }
}
