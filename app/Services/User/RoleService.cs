using AutoMapper;
using dotnetapi.ViewModels;
using dotnetapi.ViewModels.User;
using UserModel = dotnetapi.Models.User.User;
using NTR.Common.DataAccess;
using dotnetapi.Models.User;
using Microsoft.EntityFrameworkCore;

namespace dotnetapi.Services.User
{
    public interface IRoleService
    {
        List<RoleResponseViewModel> GetAll();

        RoleResponseViewModel GetById(long id);
        RoleResponseViewModel Create(RoleRequestViewModel Role);
        bool Update(long id, RoleRequestViewModel Role);

        bool Delete(long id);

        //RoleAction
        RoleActionResponseViewModel CreateRoleAction(RoleActionRequestViewModel roleAction);

        bool DeleteRoleAction(RoleActionRequestViewModel roleAction);
    }
    public class RoleService : IRoleService
    {
        private readonly IUowProvider uowProvider;

        private readonly IMapper mapper;

        public RoleService(IMapper mapper, IUowProvider uowProvider)
        {
            this.mapper = mapper;
            this.uowProvider = uowProvider;
        }

        public List<RoleResponseViewModel> GetAll()
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Role>();
                var roles = repository.Filters(r => r!.is_deleted == false)
                .Include(r => r.RoleActions!.Where(r => r.is_deleted == false))
                .ThenInclude(r => r.Action)
                .ToList();

                return this.mapper.Map<List<RoleResponseViewModel>>(roles);
            }
        }

        public RoleResponseViewModel GetById(long id)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Role>();
                var role = repository.Filters(r => r.id == id && r.is_deleted == false)
                .Include(r => r.RoleActions!.Where(r => r.is_deleted == false))
                .ThenInclude(r => r.Action)
                .FirstOrDefault();

                if (role == null)
                {
                    throw new Exception("Role not found");
                }
                return this.mapper.Map<RoleResponseViewModel>(role);
            }
        }

        public RoleResponseViewModel Create(RoleRequestViewModel role)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Role>();
                if (repository.Any(a => a.Name == role.name && a.is_deleted == false))
                    throw new Exception("Role name already exists");
                var newRole = this.mapper.Map<Role>(role);
                repository?.Add(newRole);
                uow?.SaveChanges();
                return this.mapper.Map<RoleResponseViewModel>(newRole);
            }
        }

        public bool Update(long id, RoleRequestViewModel role)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Role>();
                var existingRole = repository.GetActive(id);

                if (existingRole == null)
                    throw new Exception("Role not found");

                existingRole = mapper.Map<RoleRequestViewModel, Role>(role, existingRole);
                uow.SaveChanges();

                return true;
            }
        }

        public bool Delete(long id)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Role?>();
                var existingRole = repository.GetActive(id);

                if (existingRole == null)
                {
                    throw new Exception("Role not found");
                }

                existingRole.is_deleted = true;
                uow.SaveChanges();

                return true;

            }
        }

        public RoleActionResponseViewModel CreateRoleAction(RoleActionRequestViewModel roleAction)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<RoleAction>();
                var existingRoleAction = repository.Query(uc => uc.ActionId == roleAction.actionId && uc.RoleId == roleAction.roleId).FirstOrDefault();
                var newRoleAction = new RoleAction();
                //check input roleID, actionId are existing
                var roleIds = uow.GetRepository<Role>();
                var actionIds = uow.GetRepository<Models.User.Action>();
                if (roleIds.Any(a => a.id == roleAction.roleId && a.is_deleted == true))
                    throw new Exception("RoleId not found");
                if (actionIds.Any(a => a.id == roleAction.actionId && a.is_deleted == true))
                    throw new Exception("ActionId not found");

                if (existingRoleAction != null)
                {
                    if (existingRoleAction.is_deleted)
                    {
                        existingRoleAction.is_deleted = false;
                        newRoleAction = existingRoleAction;
                    }
                    else
                    {
                        throw new Exception("Duplicate RoleAction");
                    }
                }
                else
                {
                    newRoleAction = this.mapper.Map<RoleAction>(roleAction);
                    repository?.Add(newRoleAction);
                }

                uow?.SaveChanges();
                return this.mapper.Map<RoleActionResponseViewModel>(newRoleAction);
            }
        }

        public bool DeleteRoleAction(RoleActionRequestViewModel roleAction)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<RoleAction>();
                var existingRoleAction = repository.Filters(a => a.RoleId == roleAction.roleId && a.ActionId == roleAction.actionId && a.is_deleted == false).FirstOrDefault();

                if (existingRoleAction == null)
                {
                    throw new Exception("RoleAction not found");
                }

                existingRoleAction.is_deleted = true;
                uow.SaveChanges();

                return true;
            }
        }
    }
}