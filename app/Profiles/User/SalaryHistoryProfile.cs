
using AutoMapper;
using dotnetapi.Models.User;
using dotnetapi.ViewModels.User;

namespace dotnetapi.Profiles.User
{
    public class SalaryHistoryProfile : Profile
    {
        public SalaryHistoryProfile()
		{
            CreateMap<SalaryHistoryViewModel, SalaryHistory>()
				.ForMember(x => x.Amount, cd => cd.MapFrom(map => map.amount))
                .ForMember(x => x.SalaryId, cd => cd.MapFrom(map => map.salary_id))
            ;

            CreateMap<SalaryHistory, SalaryHistoryViewModel>()
                .ForMember(x => x.amount, cd => cd.MapFrom(map => map.Amount))
                .ForMember(x => x.salary_id, cd => cd.MapFrom(map => map.SalaryId))
            ;

            CreateMap<SalaryHistoryRequestViewModel, SalaryHistory>()
				.ForMember(x => x.Amount, cd => cd.MapFrom(map => map.amount))
                .ForMember(x => x.SalaryId, cd => cd.MapFrom(map => map.salary_id))
            ;

            CreateMap<SalaryHistory, SalaryHistoryRequestViewModel>()
                .ForMember(x => x.amount, cd => cd.MapFrom(map => map.Amount))
                .ForMember(x => x.salary_id, cd => cd.MapFrom(map => map.SalaryId))
                
            ;
    

            CreateMap<SalaryHistory, SalaryHistoryResponseViewModel>()
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
                .ForMember(x => x.amount, cd => cd.MapFrom(map => map.Amount))
                .ForMember(x => x.salary, cd => cd.MapFrom(map => new SalaryResponseViewModel{id = map.Salary.id , amount = (int?)map.Salary.Amount , description = map.Salary.Description }))
            ;
        }
    }
}