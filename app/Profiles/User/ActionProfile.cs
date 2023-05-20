
using AutoMapper;
using dotnetapi.Models.User;
using dotnetapi.ViewModels.User;

namespace dotnetapi.Profiles.User
{
    public class ActionProfile : Profile
    {
        public ActionProfile()
		{
            CreateMap<ActionViewModel, Models.User.Action>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.Priority, cd => cd.MapFrom(map => map.priority))
            ;

            CreateMap<Models.User.Action, ActionViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.priority, cd => cd.MapFrom(map => map.Priority))
            ;

            CreateMap<ActionRequestViewModel, Models.User.Action>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.Priority, cd => cd.MapFrom(map => map.priority))
            ;

            CreateMap<Models.User.Action, ActionRequestViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.priority, cd => cd.MapFrom(map => map.Priority))
            ;

            CreateMap<ActionResponseViewModel, Models.User.Action>()
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.Priority, cd => cd.MapFrom(map => map.priority))
            ;

            CreateMap<Models.User.Action, ActionResponseViewModel>()
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.priority, cd => cd.MapFrom(map => map.Priority))
            ;
        }
    }
}