
using AutoMapper;
using dotnetapi.Models.User;
using dotnetapi.ViewModels.User;

namespace dotnetapi.Profiles.User
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
		{
            CreateMap<DepartmentViewModel, Department>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
            ;

            CreateMap<Department, DepartmentViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
            ;

            CreateMap<DepartmentRequestViewModel, Department>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
            ;

            CreateMap<Department, DepartmentRequestViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
            ;

            CreateMap<DepartmentResponseViewModel, Department>()
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
            ;

            CreateMap<Department, DepartmentResponseViewModel>()
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
            ;
        }
    }
}