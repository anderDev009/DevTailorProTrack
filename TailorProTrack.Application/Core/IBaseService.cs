namespace TailorProTrack.Application.Core
{
    public interface IBaseService<DtoAdd,DtoRemove,DtoUpdate>
    {
        ServiceResult GetAll();
        ServiceResult GetById(int id);
        ServiceResult Add(DtoAdd dtoAdd);
        ServiceResult Remove(DtoRemove dtoRemove); 
        ServiceResult Update(DtoUpdate dtoUpdate);
    }
}
