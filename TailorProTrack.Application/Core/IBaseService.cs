
namespace TailorProTrack.Application.Core
{
    public interface IBaseService<DtoAdd,DtoRemove,DtoUpdate>
    {
        ServiceResultWithHeader GetAll(PaginationParams @params);
        ServiceResult GetById(int id);
        ServiceResult Add(DtoAdd dtoAdd);
        ServiceResult Remove(DtoRemove dtoRemove); 
        ServiceResult Update(DtoUpdate dtoUpdate);
    }
}
