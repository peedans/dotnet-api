
using AutoMapper;
using dotnetapi.Models.User;
using dotnetapi.ViewModels.User;

namespace dotnetapi.Profiles.User
{
    public class SalaryProfile : Profile
    {
        public SalaryProfile()
		{
            CreateMap<SalaryViewModel, Salary>()
				.ForMember(x => x.Amount, cd => cd.MapFrom(map => map.amount))
                .ForMember(x => x.Description, cd => cd.MapFrom(map => map.description))
                .ForMember(x => x.CompanyId, cd => cd.MapFrom(map => map.company_id))
                .ForMember(x => x.UserId, cd => cd.MapFrom(map => map.user_id))

            ;

            CreateMap<Salary, SalaryViewModel>()
                .ForMember(x => x.amount, cd => cd.MapFrom(map => map.Amount))
                .ForMember(x => x.description, cd => cd.MapFrom(map => map.Description))
                .ForMember(x => x.company_id, cd => cd.MapFrom(map => map.CompanyId))
                .ForMember(x => x.user_id, cd => cd.MapFrom(map => map.UserId))

            ;

            CreateMap<SalaryRequestViewModel, Salary>()
				.ForMember(x => x.Amount, cd => cd.MapFrom(map => map.amount))
                .ForMember(x => x.Description, cd => cd.MapFrom(map => map.description))
                .ForMember(x => x.CompanyId, cd => cd.MapFrom(map => map.company_id))
                .ForMember(x => x.UserId, cd => cd.MapFrom(map => map.user_id))
            ;

            CreateMap<Salary, SalaryRequestViewModel>()
                .ForMember(x => x.amount, cd => cd.MapFrom(map => map.Amount))
                .ForMember(x => x.description, cd => cd.MapFrom(map => map.Description))
                .ForMember(x => x.company_id, cd => cd.MapFrom(map => map.CompanyId))
                .ForMember(x => x.user_id, cd => cd.MapFrom(map => map.UserId))
            ;

    

            CreateMap<Salary, SalaryResponseViewModel>()
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
                .ForMember(x => x.amount, cd => cd.MapFrom(map => map.Amount))
                .ForMember(x => x.description, cd => cd.MapFrom(map => map.Description))
                .ForMember(x => x.company, cd => cd.MapFrom(map => new CompanyResponseViewModel{id = map.Company.id , name = map.Company.Name } ))
                .ForMember(x => x.user, cd => cd.MapFrom(map => new UserResponseViewModel{id = map.User.id , name = map.User.Name}))
            ;
        }
    }
}