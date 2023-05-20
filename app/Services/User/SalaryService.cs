using AutoMapper;
using dotnetapi.ViewModels;
using dotnetapi.ViewModels.User;
using UserModel = dotnetapi.Models.User.User;
using NTR.Common.DataAccess;
using dotnetapi.Models.User;
using Microsoft.EntityFrameworkCore;

namespace dotnetapi.Services.User
{
    public interface ISalaryService
    {
        List<SalaryResponseViewModel> GetAll();

        SalaryResponseViewModel GetById(long id);
        SalaryResponseViewModel Create(SalaryRequestViewModel Salary);
        bool Update(long id, SalaryRequestUpdateViewModel Salary);

        bool Delete(long id);

        SalaryHistoryResponseViewModel CreateSalaryHistory(SalaryHistoryRequestViewModel salaryHistory);
    }
    public class SalaryService : ISalaryService
    {
        private readonly IUowProvider uowProvider;

        private readonly IMapper mapper;

         public SalaryService(IMapper mapper , IUowProvider uowProvider)
        {
            this.mapper = mapper;
            this.uowProvider = uowProvider;
        }

        public List<SalaryResponseViewModel> GetAll()
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Salary>();
                var salary = repository.Filters(s => s!.is_deleted == false)
                .Include(u => u.Company)
                .Include(u => u.User)
                .ToList();
                var salarys = repository.GetAllActive();

                return this.mapper.Map<List<SalaryResponseViewModel>>(salarys);
            }
        }

         public SalaryResponseViewModel GetById(long id)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Salary>();
                var salary = repository.Filters(s => s!.is_deleted == false)
                .Include(u => u.Company)
                .Include(u => u.User)
                .FirstOrDefault();
                var Salary = repository.GetActive(id);
                if(Salary == null){
                    throw new Exception("Salary not found");
                }
                return this.mapper.Map<SalaryResponseViewModel>(Salary);
            }
        }

        public SalaryResponseViewModel Create(SalaryRequestViewModel salary)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Salary>();
                var user = uow.GetRepository<UserModel>().GetActive(salary.company_id);
                if (user == null) throw new Exception("company_id not found");
                var company = uow.GetRepository<Company>().GetActive(salary.user_id);
                if (user == null) throw new Exception("user_id not found");
                var newSalary = this.mapper.Map<Salary>(salary);
                repository?.Add(newSalary);
                uow?.SaveChanges();
                return this.mapper.Map<SalaryResponseViewModel>(newSalary);
            }
        }

        public bool Update(long id, SalaryRequestUpdateViewModel salary)
        {
             using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Salary>();
                var existingSalary = repository.GetActive(id);
                
                if (existingSalary == null)
                    throw new Exception("Salary not found");

                existingSalary = mapper.Map<SalaryRequestUpdateViewModel,Salary>(salary, existingSalary);
                uow.SaveChanges();

                return true;
            }
        }

        public bool Delete(long id)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Salary?>();
                var existingSalary= repository.GetActive(id);

                if (existingSalary == null)
                {
                 throw new Exception("Salary not found");
                }

                existingSalary.is_deleted = true;
                uow.SaveChanges();

                return true;

            }
        }

        public SalaryHistoryResponseViewModel CreateSalaryHistory(SalaryHistoryRequestViewModel salaryHistory)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<SalaryHistory>();
                var newSalaryHistory = this.mapper.Map<SalaryHistory>(salaryHistory);
                repository?.Add(newSalaryHistory);
                uow?.SaveChanges();
                return this.mapper.Map<SalaryHistoryResponseViewModel>(newSalaryHistory);
            }
        }
    }
}