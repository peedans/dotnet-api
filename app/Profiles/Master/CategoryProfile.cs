using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models.Master;
using app.ViewModels.Master;
using AutoMapper;

namespace app.Profiles.Master
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
		{
            CreateMap<CategoryViewModel, Category>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.Description, cd => cd.MapFrom(map => map.description))
            ;

            CreateMap<Category, CategoryViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.description, cd => cd.MapFrom(map => map.Description))
            ;

            CreateMap<CategoryRequestViewModel, Category>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.Description, cd => cd.MapFrom(map => map.description))
            ;

            CreateMap<Category, CategoryRequestViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.description, cd => cd.MapFrom(map => map.Description))
            ;

            CreateMap<CategoryResponseViewModel, Category>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.Description, cd => cd.MapFrom(map => map.description))
            ;

            CreateMap<Category, CategoryResponseViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.description, cd => cd.MapFrom(map => map.Description))
            ;
        }

    }
}