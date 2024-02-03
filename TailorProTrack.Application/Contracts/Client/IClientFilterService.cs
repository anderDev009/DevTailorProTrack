

using TailorProTrack.Application.Core;

namespace TailorProTrack.Application.Contracts.Client
{
    public interface IClientFilterService
    {
        ServiceResult FilterByFullName(string fullName);
        ServiceResult FilterByRnc(string rnc);
        ServiceResult FilterByDni(string Dni);
    }
}
