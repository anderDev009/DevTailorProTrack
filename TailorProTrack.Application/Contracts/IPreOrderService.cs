﻿

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.PreOrder;

namespace TailorProTrack.Application.Contracts
{
    public interface IPreOrderService : IBaseService<PreOrderDtoAdd, PreOrderDtoRemove, PreOrderDtoUpdate>
    {
        ServiceResult GetAccountsReceivable();
        ServiceResult GetPreOrdersNotCompleted();
        ServiceResult GetPreOrdersByRecentDate();
        bool ChangeStatusPreOrder(int idPreOrder, bool status);
	}
}
