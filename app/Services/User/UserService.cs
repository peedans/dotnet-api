using System.Xml.Linq;
using System.Linq;
using AutoMapper;
using dotnetapi.ViewModels;
using dotnetapi.ViewModels.User;
using UserModel = dotnetapi.Models.User.User;
using NTR.Common.DataAccess;
using Microsoft.EntityFrameworkCore;
using dotnetapi.Models.User;
using Microsoft.IdentityModel.Tokens;

namespace dotnetapi.Services.User
{
    public interface IUserService
    {
        List<UserResponseViewModel> GetAll();

        UserResponseViewModel GetById(long id);
        UserResponseViewModel Create(UserRequestViewModel User);
        bool Update(long id, UserRequestUpdateViewModel User);

        bool Delete(long id);

        UserCompanyResponseViewModel CreateUserCompany(UserCompanyRequestViewModel userCompany);
        bool SwitchUserCompany(UserCompanyRequestUpdateViewModel payload);

        bool DeleteUserCompany(UserCompanyDeleteRequestViewModel userCompany);
    }
    public class UserService : IUserService
    {
        private readonly IUowProvider uowProvider;

        private readonly IMapper mapper;

        public UserService(IMapper mapper, IUowProvider uowProvider)
        {
            this.mapper = mapper;
            this.uowProvider = uowProvider;
        }

        public List<UserResponseViewModel> GetAll()
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<UserModel>();
                var users = repository.Filters(u => u!.is_deleted == false)
                .Include(u => u.Department)
                .Include(u => u.Role)
                // .Include(u => u.UserCompany!.Where(p => p.is_deleted == false))
                // .ThenInclude(u => u.Company)
                .ToList();

                return this.mapper.Map<List<UserResponseViewModel>>(users);
            }
        }

        public UserResponseViewModel GetById(long id)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<UserModel>();
                var user = repository.Filters(u => u.id == id && u.is_deleted == false)
                .Include(u => u.Department)
                .Include(u => u.Role)
                // .Include(u => u.UserCompany!.Where(p => p.is_deleted == false))
                // .ThenInclude(u => u.Company)
                .FirstOrDefault()
                ;
                if (user == null)
                {
                    throw new Exception("user not found");
                }
                return this.mapper.Map<UserResponseViewModel>(user);
            }
        }

        public UserResponseViewModel Create(UserRequestViewModel user)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repositoryUser = uow.GetRepository<UserModel>();
                if (repositoryUser.Any(a => a.Username == user.username && a.is_deleted == false))
                {
                    throw new Exception("User name already exists");
                }

                var department = uow.GetRepository<Department>().GetActive(user.department_id);
                if (department == null)
                {
                    throw new Exception("department_id not found");
                }

                var role = uow.GetRepository<Role>().GetActive(user.role_id);
                if (role == null)
                {
                    throw new Exception("role_id not found");
                }

                var newUser = this.mapper.Map<UserModel>(user);
                repositoryUser?.Add(newUser);
                uow?.SaveChanges();
                return this.mapper.Map<UserResponseViewModel>(newUser);
            }
        }

        public bool Update(long id, UserRequestUpdateViewModel User)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<UserModel>();
                var existingUser = repository.GetActive(id);

                if (existingUser == null)
                {
                    throw new Exception("User not found");
                }

                if (User.name != existingUser.Name)
                {
                    var emailExist = repository.Any(a => a.Name == User.name);

                    if (emailExist)
                    {
                        throw new Exception("User already exists");
                    }
                }

                existingUser = mapper.Map<UserRequestUpdateViewModel, UserModel>(User, existingUser);
                uow.SaveChanges();

                return true;

            }
        }

        public bool Delete(long id)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<UserModel?>();
                var existingUser = repository.GetActive(id);

                if (existingUser == null)
                {
                    throw new Exception("User not found");
                }

                existingUser.is_deleted = true;
                uow.SaveChanges();

                return true;

            }
        }

        public UserCompanyResponseViewModel CreateUserCompany(UserCompanyRequestViewModel userCompany)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<UserCompany>();
                var newUserCompany = new UserCompany();
                var existingUserCompany = repository.Query(uc => uc.UserId == userCompany.user_id && uc.CompanyId == userCompany.company_id).FirstOrDefault();


                if (existingUserCompany != null)
                {
                    if (existingUserCompany.is_deleted)
                    {
                        existingUserCompany.is_deleted = false;
                        newUserCompany = existingUserCompany;
                    }
                    else
                    {
                        throw new Exception("Duplicate UserCompany");
                    }
                }
                else
                {
                    newUserCompany = this.mapper.Map<UserCompany>(userCompany);
                    var currentUserCompany = repository.Any(uc => uc.UserId == userCompany.user_id && uc.is_deleted == false);
                    if (!currentUserCompany)
                    {
                        newUserCompany.IsMain = true;
                    };
                    repository?.Add(newUserCompany);
                }


                uow?.SaveChanges();
                return this.mapper.Map<UserCompanyResponseViewModel>(newUserCompany);
            }
        }

        public bool SwitchUserCompany(UserCompanyRequestUpdateViewModel payload)
        {

            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var UserRepository = uow.GetRepository<UserModel>();

                var userExists = UserRepository.Any(a => a.id == payload.user_id && a.is_deleted == false);
                if (!userExists)
                {
                    throw new Exception("User ID not found.");
                }

                var CompanyRepository = uow.GetRepository<Company>();

                var companyExists = CompanyRepository.Any(a => a.id == payload.company_id && a.is_deleted == false);
                if (!companyExists)
                {
                    throw new Exception("Company ID not found.");
                }

                var repository = uow.GetRepository<UserCompany>();

                var usercompanyExists = repository.Filters(uc => uc.CompanyId == payload.company_id && uc.UserId == payload.user_id && uc.is_deleted == false).FirstOrDefault();
            
                if (usercompanyExists == null)
                {
                    usercompanyExists = new UserCompany();
                    usercompanyExists = this.mapper.Map<UserCompany>(payload);
                    repository.Add(usercompanyExists);
                }

                usercompanyExists.IsMain = true;

                // If is_main is true, set is_main to false for all other user-company records associated with the same user
                var UserCompanies = repository.Filters(uc => uc.UserId == payload.user_id && uc.CompanyId != payload.company_id && uc.is_deleted == false);
                foreach (var userCompany in UserCompanies)
                {
                    userCompany.IsMain = false;
                }

                uow.SaveChanges();
                return true;

            }
        }

        public bool DeleteUserCompany(UserCompanyDeleteRequestViewModel userCompany)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<UserCompany>();

                var userCompanyExists = repository.Any(a => a.UserId == userCompany.user_id && a.is_deleted ==false);
                if (!userCompanyExists)
                {
                    throw new Exception("User ID not found.");
                }

                var companyExists = repository.Any(a => a.CompanyId == userCompany.company_id && a.is_deleted == false);
                if (!companyExists)
                {
                    throw new Exception("Company ID not found.");
                }

                var existingUserCompany = repository.Filters(uc => uc.CompanyId == userCompany.company_id && uc.UserId == userCompany.user_id && uc.is_deleted == false).FirstOrDefault();

                if (existingUserCompany == null)
                {
                    throw new Exception("UserCompany not found");
                }

                // If the user company to delete is the main one, then set another user company to be the main one
                if (existingUserCompany.IsMain)
                {
                    var otherUserCompanies = repository.Filters(uc => uc.UserId == existingUserCompany.UserId && uc.CompanyId != userCompany.company_id && uc.is_deleted == false);
                    var otherUserCompanyToMakeMain = otherUserCompanies.FirstOrDefault();
                    if (otherUserCompanyToMakeMain != null)
                    {
                        otherUserCompanyToMakeMain.IsMain = true;
                    }
                    else
                    {
                        throw new Exception("Can not delete it have only one company");
                    }
                }

                existingUserCompany.is_deleted = true;
                existingUserCompany.IsMain = false;
                uow.SaveChanges();

                return true;

            }
        }

    }

}