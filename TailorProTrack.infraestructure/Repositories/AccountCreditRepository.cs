

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
	public class AccountCreditRepository(TailorProTrackContext ctx)
		: BaseRepository<AccountCredit>(ctx), IAcccountCreditRepository

	{

	}
}
