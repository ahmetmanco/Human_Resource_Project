using AutoMapper;
using HumanResource.Application.Models.DTOs.TitleDTOs;
using HumanResource.Domain.Entities;
using HumanResource.Domain.Enums;
using HumanResource.Domain.Repositories;

namespace HumanResource.Application.Services.TitleService
{
    internal class TitleService : ITitleService
    {
        private readonly ITitleRepository _titleRepository;
        private readonly IMapper _mapper;

        public TitleService(ITitleRepository TitleRepository, IMapper mapper)
        {
            _titleRepository = TitleRepository;
            _mapper = mapper;
        }


        public async Task<bool> Create(CreateTitleDTO model, int? companyId)
        {
            Title title = _mapper.Map<Title>(model);
            title.StatuId = Status.Active.GetHashCode();
            title.CompanyId = companyId;
            return await _titleRepository.Add(title);
        }

        public async Task Delete(int id)
        {
            Title title = await _titleRepository.GetDefault(x => x.Id == id);
            if (title != null)
            {
                title.StatuId = Status.Deleted.GetHashCode();
                await _titleRepository.Delete(title);
            }
        }

        public async Task<UpdateTitleDTO> GetById(int id)
        {
            Title title = await _titleRepository.GetDefault(x => x.Id == id);
            return _mapper.Map<UpdateTitleDTO>(title);

        }

        public async Task<bool> Update(UpdateTitleDTO model)
        {
            Title title = _mapper.Map<Title>(model);
            return await _titleRepository.Update(title);

        }
    }
}
