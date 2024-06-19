using API.DTOs.Requests;
using API.DTOs.Responses;
using API.Models;
using API.Utilities;
using AutoMapper;

namespace API.DTOs;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RegionRequestDto, Region>();
        CreateMap<Region, RegionResponseDto>();

        CreateMap<CountryRequestDto, Country>();
        CreateMap<Country, CountryResponseDto>();

        CreateMap<LocationRequestDto, Location>();
        CreateMap<Location, LocationResponseDto>();

        CreateMap<DepartmentRequestDto, Department>();
        CreateMap<Department, DepartmentResponseDto>();

        CreateMap<JobRequestDto, Job>();
        CreateMap<Job, JobResponseDto>();

        CreateMap<RoleRequestDto, Role>();
        CreateMap<Role, RoleResponseDto>();

        CreateMap<UserRequestDto, User>()
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => HashPasswordHandler.GenerateHash(src.Password)))
           .ForMember(dest => dest.Otp, opt => opt.MapFrom(_ => 0))
           .ForMember(dest => dest.ExpiredOtp, opt => opt.MapFrom(_ => DateTime.Now))
           .ForMember(dest => dest.IsOtpUsed, opt => opt.MapFrom(_ => true));
        CreateMap<User, UserResponseDto>();
        CreateMap<RegisterRequestDto, User>()
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => HashPasswordHandler.GenerateHash(src.Password)))
           .ForMember(dest => dest.Otp, opt => opt.MapFrom(_ => 0))
           .ForMember(dest => dest.ExpiredOtp, opt => opt.MapFrom(_ => DateTime.Now))
           .ForMember(dest => dest.IsOtpUsed, opt => opt.MapFrom(_ => true));

        CreateMap<UserRoleRequestDto, UserRole>();

        CreateMap<EmployeeRequestDto, Employee>();
        CreateMap<RegisterRequestDto, Employee>();
        CreateMap<Employee, EmployeeResponseDto>();
        CreateMap<Employee, EmployeeDetailResponseDto>()
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
           .ForMember(dest => dest.ManagerNik, opt => opt.MapFrom(src => src.Manager.Nik))
           .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager.GetFullName()))
           .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title))
           .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name))
           .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Department.Location.City));
    }
}
