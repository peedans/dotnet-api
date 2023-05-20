using dotnetapi.Models.User;
using Microsoft.EntityFrameworkCore;
using dotnetapi.ViewModels.Base;
using AutoMapper;
using dotnetapi.ViewModels.User;
using NTR.Common.DataAccess;
using NTR.Common.DataAccess.Repositories;

namespace dotnetapi.Services.User
{
    public interface IActionService
    {
        List<ActionResponseViewModel> GetAll();
        ActionResponseViewModel GetById(long id);
        ActionResponseViewModel Create(ActionRequestViewModel action);
        bool Update(long id, ActionRequestViewModel action);

        bool Delete(long id);

    }
    public class ActionService : IActionService
    {
        private readonly IUowProvider uowProvider;

        private readonly IMapper mapper;

         public ActionService(IMapper mapper , IUowProvider uowProvider)
        {
            this.mapper = mapper;
            this.uowProvider = uowProvider;
        }

        public List<ActionResponseViewModel> GetAll()
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Models.User.Action?>();
                var actions = repository.GetAllActive();

                return this.mapper.Map<List<ActionResponseViewModel>>(actions);
            }
        }

        public ActionResponseViewModel GetById(long id)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Models.User.Action?>();
                var action = repository.GetActive(id);
                if(action == null){
                    throw new Exception("Action not found");
                }
                return this.mapper.Map<ActionResponseViewModel>(action);
            }
        }

        public ActionResponseViewModel Create(ActionRequestViewModel action)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Models.User.Action>();
                if (repository.Any(a => a.Name == action.name && a.is_deleted == false))
                    throw new Exception("Action name already exists");
                var newAction = this.mapper.Map<Models.User.Action>(action);
                repository?.Add(newAction);
                uow?.SaveChanges();
                return this.mapper.Map<ActionResponseViewModel>(newAction);
            }
        }

        public bool Update(long id, ActionRequestViewModel action)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Models.User.Action>();
                var existingAction = repository.GetActive(id);
                
                if (existingAction == null) 
                {
                    throw new Exception("Action not found");
                }

                if (action.name != existingAction.Name)
                {
                    var nameExist = repository.Any(a => a.Name == action.name);

                    if (nameExist)
                    {
                        throw new Exception("Action name already exists");
                    }
                }

                existingAction = mapper.Map<ActionRequestViewModel, Models.User.Action>(action, existingAction);
                uow.SaveChanges();

                return true;

            }
        }

        public bool Delete(long id)
        {
            using(var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Models.User.Action?>();
                var existingAction = repository.GetActive(id);

                if (existingAction == null)
                {
                 throw new Exception("Action not found");
                }

                existingAction.is_deleted = true;
                uow.SaveChanges();

                return true;

            }
        }
    }
}