﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.Application.Core
{
    public interface IBaseServiceGeneric<DtoAdd,DtoUpdate,DtoGet,T>
        where DtoAdd : class
        where DtoUpdate : class
        where DtoGet : class
        where T : class
    {
        ServiceResultWithHeader GetAll(PaginationParams @params);
        ServiceResultWithHeader GetAllWithInclude(PaginationParams @params,List<string> properties);
        ServiceResult GetById(int id);
        ServiceResult GetByIdWithInclude(int id,List<string> properties);
        ServiceResult Add(DtoAdd dtoAdd);
        ServiceResult Remove(int ID);
        ServiceResult Update(DtoUpdate dtoUpdate, int id);
    }
}
