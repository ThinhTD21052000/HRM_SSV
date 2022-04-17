using AutoMapper;
using Domain.Modals.Company;
using Server.Entities;
using Server.Repositories;

namespace Server.Services
{
    public interface ICompanyService
    {
        Task Create(CompanyToAdd companyToAdd);
        Task Update(CompanyToUpdate companyToUpdate);
        Task Delete(int id);
        Task<CompanyToGet> Get(int id);
        Task<List<CompanyToGet>> GetList();
    }
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task Create(CompanyToAdd companyToAdd)
        {
            await _companyRepository.Add(_mapper.Map<Company>(companyToAdd));
        }

        public async Task Delete(int id)
        {
            await _companyRepository.Delete(_mapper.Map<Company>( await Get(id)));
        }

        public async Task<CompanyToGet> Get(int id)
        {
            return _mapper.Map<CompanyToGet>(await _companyRepository.Get(x => x.Id.Equals(id)));
        }

        public async Task<List<CompanyToGet>> GetList()
        {
            return _mapper.Map<List<CompanyToGet>>(await _companyRepository.GetAll());
        }

        public async Task Update(CompanyToUpdate companyToUpdate)
        {
            await _companyRepository.Update(_mapper.Map<Company>(companyToUpdate));
        }
    }
}
