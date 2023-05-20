
using AutoMapper;
using dotnetapi.Models.User;
using dotnetapi.ViewModels.User;

namespace dotnetapi.Profiles.User
{
    public class UserCompanyProfile : Profile
    {
        public UserCompanyProfile()
		{
            CreateMap<UserCompanyViewModel, UserCompany>()
				.ForMember(x => x.UserId, cd => cd.MapFrom(map => map.user_id))
                .ForMember(x => x.CompanyId, cd => cd.MapFrom(map => map.company_id))
            ;

            CreateMap<UserCompany, UserCompanyViewModel>()
                .ForMember(x => x.user_id, cd => cd.MapFrom(map => map.UserId))
                .ForMember(x => x.company_id, cd => cd.MapFrom(map => map.CompanyId))
            ;

            CreateMap<UserCompanyRequestViewModel, UserCompany>()
                .ForMember(x => x.UserId, cd => cd.MapFrom(map => map.user_id))
                .ForMember(x => x.CompanyId, cd => cd.MapFrom(map => map.company_id))
                .ForMember(x => x.IsMain, cd => cd.MapFrom(map => map.is_main))
            ;

            CreateMap<UserCompany, UserCompanyRequestViewModel>()
                .ForMember(x => x.user_id, cd => cd.MapFrom(map => map.UserId))
                .ForMember(x => x.company_id, cd => cd.MapFrom(map => map.CompanyId))
                .ForMember(x => x.is_main, cd => cd.MapFrom(map => map.IsMain))
            ;

            CreateMap<UserCompanyResponseViewModel,UserCompany>()
				.ForMember(x => x.UserId, cd => cd.MapFrom(map => map.user_id))
                .ForMember(x => x.CompanyId, cd => cd.MapFrom(map => map.company_id))
            ;

            CreateMap<UserCompany, UserCompanyResponseViewModel>()
                .ForMember(x => x.user_id, cd => cd.MapFrom(map => map.UserId))
                .ForMember(x => x.company_id, cd => cd.MapFrom(map => map.CompanyId))
                .ForMember(x => x.is_main, cd => cd.MapFrom(map => map.IsMain))
            ;

             CreateMap<UserCompanyRequestUpdateViewModel, UserCompany>()
                .ForMember(x => x.UserId, cd => cd.MapFrom(map => map.user_id))
                .ForMember(x => x.CompanyId, cd => cd.MapFrom(map => map.company_id))
            ;
        }
    }
}