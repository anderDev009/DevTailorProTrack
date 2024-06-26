﻿
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IPhoneRepository : IBaseRepository<Phone>
    {
        void SaveMany(List<Phone> phones);
    }
}
