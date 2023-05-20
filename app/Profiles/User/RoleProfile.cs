
using AutoMapper;
using dotnetapi.Models.User;
using dotnetapi.ViewModels.User;

namespace dotnetapi.Profiles.User
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
		{
            CreateMap<RoleViewModel, Role>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.Description, cd => cd.MapFrom(map => map.description))
            ;

            CreateMap<Role, RoleViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.description, cd => cd.MapFrom(map => map.Description))
            ;

            CreateMap<RoleRequestViewModel, Role>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.Description, cd => cd.MapFrom(map => map.description))
            ;

            CreateMap<Role, RoleRequestViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.description, cd => cd.MapFrom(map => map.Description))
            ;

            CreateMap<RoleResponseViewModel, Role>()
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.Description, cd => cd.MapFrom(map => map.description))
            ;

            CreateMap<Role, RoleResponseViewModel>()
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.description, cd => cd.MapFrom(map => map.Description))
                .ForMember(x => x.actions, cd => cd.MapFrom(map => map.RoleActions!.Select(t => new ActionResponseViewModel {id = t.ActionId, name = t.Action.Name, priority = t.Action.Priority})))
            ;
        }
    }
}