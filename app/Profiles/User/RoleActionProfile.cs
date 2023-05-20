
using AutoMapper;
using dotnetapi.Models.User;
using dotnetapi.ViewModels.User;

namespace dotnetapi.Profiles.User
{
    public class RoleActionProfile : Profile
    {
        public RoleActionProfile()
		{
            CreateMap<RoleActionViewModel, Models.User.RoleAction>()
				.ForMember(x => x.ActionId, cd => cd.MapFrom(map => map.actionId))
                .ForMember(x => x.RoleId, cd => cd.MapFrom(map => map.roleId))
            ;

            CreateMap<Models.User.RoleAction, RoleActionViewModel>()
                .ForMember(x => x.actionId, cd => cd.MapFrom(map => map.ActionId))
                .ForMember(x => x.roleId, cd => cd.MapFrom(map => map.RoleId))
            ;

            CreateMap<RoleActionRequestViewModel, Models.User.RoleAction>()
				.ForMember(x => x.ActionId, cd => cd.MapFrom(map => map.actionId))
                .ForMember(x => x.RoleId, cd => cd.MapFrom(map => map.roleId))
            ;

            CreateMap<Models.User.RoleAction, RoleActionRequestViewModel>()
                .ForMember(x => x.actionId, cd => cd.MapFrom(map => map.ActionId))
                .ForMember(x => x.roleId, cd => cd.MapFrom(map => map.RoleId))
            ;

            CreateMap<RoleActionResponseViewModel, Models.User.RoleAction>()
				.ForMember(x => x.ActionId, cd => cd.MapFrom(map => map.actionId))
                .ForMember(x => x.RoleId, cd => cd.MapFrom(map => map.roleId))
            ;

            CreateMap<Models.User.RoleAction, RoleActionResponseViewModel>()
                .ForMember(x => x.actionId, cd => cd.MapFrom(map => map.ActionId))
                .ForMember(x => x.roleId, cd => cd.MapFrom(map => map.RoleId))
            ;
        }
    }
}