using dotnetapi.Models.User;
using Microsoft.EntityFrameworkCore;
using dotnetapi.ViewModels.Base;
using AutoMapper;
using dotnetapi.ViewModels.User;
using NTR.Common.DataAccess;
using NTR.Common.DataAccess.Repositories;

namespace dotnetapi.Services.User
{
    public interface IDepartmentService
    {
        List<DepartmentResponseViewModel> GetAll();
        DepartmentResponseViewModel GetById(long id);
        DepartmentResponseViewModel Create(DepartmentRequestViewModel department);
        bool Update(long id, DepartmentRequestViewModel department);

        bool Delete(long id);

    }
    public class DepartmentService : IDepartmentService
    {
        private readonly IUowProvider uowProvider;

        private readonly IMapper mapper;

         public DepartmentService(IMapper mapper , IUowProvider uowProvider)
        {
            this.mapper = mapper;
            this.uowProvider = uowProvider;
        }

        public List<DepartmentResponseViewModel> GetAll()
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Department?>();
                var departments = repository.GetAllActive();

                return this.mapper.Map<List<DepartmentResponseViewModel>>(departments);
            }
        }

        public DepartmentResponseViewModel GetById(long id)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Department?>();
                var department = repository.GetActive(id);
                if(department == null){
                    throw new Exception("Department not found");
                }
                return this.mapper.Map<DepartmentResponseViewModel>(department);
            }
        }

        public DepartmentResponseViewModel Create(DepartmentRequestViewModel department)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Department>();
                if (repository.Any(a => a.Name == department.name && a.is_deleted == false))
                    throw new Exception("Department name already exists");
                var newDepartment = this.mapper.Map<Department>(department);
                repository?.Add(newDepartment);
                uow?.SaveChanges();
                return this.mapper.Map<DepartmentResponseViewModel>(newDepartment);
            }
        }

        public bool Update(long id, DepartmentRequestViewModel department)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Department>();
                var existingDepartment = repository.GetActive(id);
                
                if (existingDepartment == null) 
                {
                    throw new Exception("Department not found");
                }

                if (department.name != existingDepartment.Name)
                {
                    var nameExist = repository.Any(a => a.Name == department.name);

                    if (nameExist)
                    {
                        throw new Exception("Department name already exists");
                    }
                }

                existingDepartment = mapper.Map<DepartmentRequestViewModel,Department>(department, existingDepartment);
                uow.SaveChanges();

                return true;

            }
        }

        public bool Delete(long id)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Department?>();
                var existingDepartment = repository.GetActive(id);

                if (existingDepartment == null)
                {
                 throw new Exception("Department not found");
                }

                existingDepartment.is_deleted = true;
                uow.SaveChanges();

                return true;

            }
        }
    }
}