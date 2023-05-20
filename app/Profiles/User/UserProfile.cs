using AutoMapper;
using UserModel = dotnetapi.Models.User.User;
using dotnetapi.ViewModels.User;

namespace dotnetapi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, UserModel>()
                .ForMember(x => x.Username, opt => opt.MapFrom(src => src.username))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(x => x.Lastname, opt => opt.MapFrom(src => src.lastname))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(x => x.Password, opt => opt.MapFrom(src => src.password))
                .ForMember(x => x.LastLogin, opt => opt.MapFrom(src => src.last_login))
                .ForMember(x => x.LastLogout, opt => opt.MapFrom(src => src.last_logout))
                .ForMember(x => x.DepartmentId, opt => opt.MapFrom(src => src.department_id))
                .ForMember(x => x.RoleId, opt => opt.MapFrom(src => src.role_id))
            ;

            CreateMap<UserModel, UserViewModel>()
                .ForMember(x => x.username, opt => opt.MapFrom(src => src.Username))
                .ForMember(x => x.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(x => x.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.password, opt => opt.MapFrom(src => src.Password))
                .ForMember(x => x.last_login, opt => opt.MapFrom(src => src.LastLogin))
                .ForMember(x => x.last_logout, opt => opt.MapFrom(src => src.LastLogout))
                .ForMember(x => x.department_id, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(x => x.role_id, opt => opt.MapFrom(src => src.RoleId))
            ;

            CreateMap<UserRequestViewModel, UserModel>()
                .ForMember(x => x.Username, opt => opt.MapFrom(src => src.username))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(x => x.Lastname, opt => opt.MapFrom(src => src.lastname))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.email))
                .ForMember(x => x.Password, opt => opt.MapFrom(src => src.password))
                .ForMember(x=>x.DepartmentId , opt => opt.MapFrom(src => src.department_id) )
                .ForMember(x=>x.RoleId , opt => opt.MapFrom(src => src.role_id))
            ;

            CreateMap<UserModel, UserRequestViewModel>()
                .ForMember(x => x.username, opt => opt.MapFrom(src => src.Username))
                .ForMember(x => x.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(x => x.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.password, opt => opt.MapFrom(src => src.Password))
                .ForMember(x=>x.department_id , opt => opt.MapFrom(src => src.DepartmentId) )
                .ForMember(x=>x.role_id , opt => opt.MapFrom(src => src.RoleId))
            ;


            CreateMap<UserModel, UserResponseViewModel>()
                .ForMember(x => x.id, cd => cd.MapFrom(x => x.id))
                .ForMember(x => x.username, opt => opt.MapFrom(src => src.Username))
                .ForMember(x => x.name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.lastname, opt => opt.MapFrom(src => src.Lastname))
                .ForMember(x => x.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.department, cd => cd.MapFrom(map => new DepartmentResponseViewModel{id = map.Department!.id, name = map.Department.Name}))
                .ForMember(x => x.role, opt => opt.MapFrom(map => new RoleResponseViewModel{id = map.Role!.id, name = map.Role.Name , description = map.Role.Description}))
                .ForMember(x => x.company, cd => cd.MapFrom(map => map.UserCompany!.Select(t => new IsMainUserCompanyResponseViewModel {id = (long)t.CompanyId, name = t.Company.Name , location = t.Company.Location , is_main = t.IsMain })))

            ;
        }
    }
}
