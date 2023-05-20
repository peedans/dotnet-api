
using AutoMapper;
using dotnetapi.Models.User;
using dotnetapi.ViewModels.User;

namespace dotnetapi.Profiles.User
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
		{
            CreateMap<CompanyViewModel, Company>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.Location, cd => cd.MapFrom(map => map.location))
            ;

            CreateMap<Company, CompanyViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.location, cd => cd.MapFrom(map => map.Location))
            ;

            CreateMap<CompanyRequestViewModel, Company>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.Location, cd => cd.MapFrom(map => map.location))
            ;

            CreateMap<Company, CompanyRequestViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.location, cd => cd.MapFrom(map => map.Location))
            ;

            CreateMap<CompanyResponseViewModel, Company>()
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.Location, cd => cd.MapFrom(map => map.location))
            ;

            CreateMap<Company, CompanyResponseViewModel>()
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.location, cd => cd.MapFrom(map => map.Location))
            ;
        }
    }
}