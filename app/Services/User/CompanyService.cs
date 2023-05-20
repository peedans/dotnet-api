using dotnetapi.Models.User;
using Microsoft.EntityFrameworkCore;
using dotnetapi.ViewModels.Base;
using AutoMapper;
using dotnetapi.ViewModels.User;
using NTR.Common.DataAccess;
using NTR.Common.DataAccess.Repositories;

namespace dotnetapi.Services.User
{
    public interface ICompanyService
    {
        List<CompanyResponseViewModel> GetAll();
        CompanyResponseViewModel GetById(long id);
        CompanyResponseViewModel Create(CompanyRequestViewModel company);
        bool Update(long id, CompanyRequestViewModel company);

        bool Delete(long id);

    }
    public class CompanyService : ICompanyService
    {
        private readonly IUowProvider uowProvider;

        private readonly IMapper mapper;

         public CompanyService(IMapper mapper , IUowProvider uowProvider)
        {
            this.mapper = mapper;
            this.uowProvider = uowProvider;
        }

        public List<CompanyResponseViewModel> GetAll()
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Company>();
                var companies = repository.GetAllActive();

                return this.mapper.Map<List<CompanyResponseViewModel>>(companies);
            }
        }

        public CompanyResponseViewModel GetById(long id)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Company>();
                var company = repository.GetActive(id);
                if(company == null){
                    throw new Exception("Company not found");
                }
                return this.mapper.Map<CompanyResponseViewModel>(company);
            }
        }

        public CompanyResponseViewModel Create(CompanyRequestViewModel company)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Company>();
                if (repository.Any(a => a.Name == company.name && a.is_deleted == false))
                    throw new Exception("Company name already exists");
                var newCompany = this.mapper.Map<Company>(company);
                repository?.Add(newCompany);
                uow?.SaveChanges();
                return this.mapper.Map<CompanyResponseViewModel>(newCompany);
            }
        }

        public bool Update(long id, CompanyRequestViewModel company)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Company>();
                var existingCompany = repository.GetActive(id);
                
                if (existingCompany == null) 
                {
                    throw new Exception("Company not found");
                }

                existingCompany = mapper.Map<CompanyRequestViewModel,Company>(company, existingCompany);
                uow.SaveChanges();

                return true;

            }
        }

        public bool Delete(long id)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Company>();
                var existingCompany = repository.GetActive(id);

                if (existingCompany == null)
                {
                 throw new Exception("Company not found");
                }

                existingCompany.is_deleted = true;
                uow.SaveChanges();

                return true;

            }
        }
    }
}