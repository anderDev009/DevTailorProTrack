
using AutoMapper;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Dtos.CodeDgi;
using TailorProTrack.Application.Service.BaseServices;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{

    public class CodeDgiService : GenericService<CodeDgiDtoAdd, CodeDgiDtoUpdate, CodeDgiDtoGet, CodesDgi>, ICodeDgiService
    {
        private readonly IMapper _mapper;
        private readonly ICodesDgiRepository _codeDgiRepository;

        public CodeDgiService(IMapper mapper, ICodesDgiRepository repository):base(mapper, repository) 
        {
            _mapper = mapper;
            _codeDgiRepository = repository;
        }

        public int UseCodeDgi()
        {
            return _codeDgiRepository.UseCode();
        }

        public int ReverseCodeDgi()
        {
            return _codeDgiRepository.ReverseCode();
        }
    }
}
