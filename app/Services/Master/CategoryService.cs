using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models.Master;
using app.ViewModels.Master;
using AutoMapper;
using NTR.Common.DataAccess;
using NTR.Common.DataAccess.Uow;

namespace app.Services.Master
{

    public interface ICategoryService
    {
        CategoryResponseViewModel Create(CategoryRequestViewModel category);
    }

    public class CategoryService : ICategoryService
    {
        private readonly IUowProvider uowProvider;

        private readonly IMapper mapper;

        public CategoryService(IMapper mapper, IUowProvider uowProvider)
        {
            this.mapper = mapper;
            this.uowProvider = uowProvider;
        }

        public CategoryResponseViewModel Create(CategoryRequestViewModel category)
        {
            using (var uow = uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<Category>();
                if (repository.Any(a => a.Name == category.name && a.is_deleted == false))
                    throw new Exception("Category name already exists");
                var newCategory = this.mapper.Map<Category>(category);
                repository?.Add(newCategory);
                uow?.SaveChanges();
                return this.mapper.Map<CategoryResponseViewModel>(newCategory);
            }
        }
    }

}