using AutoMapper;
using HumanResource.Application.Models.DTOs.ExpenseTypeDTO;
using HumanResource.Domain.Entities;
using HumanResource.Domain.Enums;
using HumanResource.Domain.Repositories;

namespace HumanResource.Application.Services.ExpenceTypeService
{
    internal class ExpenceTypeService : IExpenceTypeService
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IMapper _mapper;

        public ExpenceTypeService(IExpenseTypeRepository expenseTypeRepository, IMapper mapper)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _mapper = mapper;
        }
        public async Task<bool> Create(CreateExpenseTypeDTO model, int? companyId)
        {
            ExpenseType expenseType = _mapper.Map<ExpenseType>(model);
            expenseType.StatuId = Status.Active.GetHashCode();
            expenseType.CompanyId = companyId;
            return await _expenseTypeRepository.Add(expenseType);
        }

        public async Task CreateDefault(int companyId)
        {
            foreach (var item in Enum.GetValues(typeof(ExpenseTypes)))
            {

                ExpenseType expenseType = new ExpenseType();
                expenseType.Name = String.Join(" ", item.ToString().Split("_"));
                expenseType.CompanyId = companyId;
                expenseType.StatuId = Status.Active.GetHashCode();
                await _expenseTypeRepository.Add(expenseType);
            }

        }

        public async Task Delete(int id)
        {
            ExpenseType expenseType = await _expenseTypeRepository.GetDefault(x => x.Id == id);
            if (expenseType != null)
            {
                expenseType.StatuId = Status.Deleted.GetHashCode();
                await _expenseTypeRepository.Delete(expenseType);
            }
        }


        public async Task<bool> Update(UpdateExpenseTypeDTO model)
        {
            ExpenseType expenseType = _mapper.Map<ExpenseType>(model);
            return await _expenseTypeRepository.Update(expenseType);
        }

        public async Task<UpdateExpenseTypeDTO> GetById(int id)
        {
            ExpenseType expenseType = await _expenseTypeRepository.GetDefault(x => x.Id == id);
            return _mapper.Map<UpdateExpenseTypeDTO>(expenseType);
        }
    }
}
