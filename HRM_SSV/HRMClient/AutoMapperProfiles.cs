using AutoMapper;
using Domain.Modals.Allowance;
using Domain.Modals.Bonus_Wage;
using Domain.Modals.Company;
using Domain.Modals.Department;
using Domain.Modals.LaborContract;
using Domain.Modals.Menu;
using Domain.Modals.MonthlySalary;
using Domain.Modals.MonthTimekeeping;
using Domain.Modals.Overtime;
using Domain.Modals.Position;
using Domain.Modals.Role;
using Domain.Modals.Team;
using Domain.Modals.Timekeeping;
using Domain.Modals.User;
using Domain.Modals.ViolationMoney;
using Domain.Modals.Wage;
using Domain.Modals.WageType;

namespace HRMClient
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserToGet, UserToAdd>().ReverseMap();
            CreateMap<UserToGet, UserToUpdate>().ReverseMap();

            CreateMap<AllowanceToGet, AllowanceToAdd>().ReverseMap();
            CreateMap<AllowanceToGet, AllowanceToUpdate>().ReverseMap();

            CreateMap<Bonus_WageToGet, Bonus_WageToAdd>().ReverseMap();
            CreateMap<Bonus_WageToGet, Bonus_WageToUpdate>().ReverseMap();

            CreateMap<CompanyToGet, CompanyToAdd>().ReverseMap();
            CreateMap<CompanyToGet, CompanyToUpdate>().ReverseMap();

            CreateMap<DepartmentToGet, DepartmentToAdd>().ReverseMap();
            CreateMap<DepartmentToGet, DepartmentToUpdate>().ReverseMap();

            CreateMap<LaborContractToGet, LaborContractToAdd>().ReverseMap();
            CreateMap<LaborContractToGet, LaborContractToUpdate>().ReverseMap();

            CreateMap<MenuToGet, MenuToAdd>().ReverseMap();
            CreateMap<MenuToGet, MenuToUpdate>().ReverseMap();

            CreateMap<MonthlySalaryToGet, MonthlySalaryToAdd>().ReverseMap();
            CreateMap<MonthlySalaryToGet, MonthlySalaryToUpdate>().ReverseMap();

            CreateMap<OvertimeToGet, OvertimeToAdd>().ReverseMap();
            CreateMap<OvertimeToGet, OvertimeToUpdate>().ReverseMap();

            CreateMap<PositionToGet, PositionToAdd>().ReverseMap();
            CreateMap<PositionToGet, PositionToUpdate>().ReverseMap();

            CreateMap<RoleToGet, RoleToAdd>().ReverseMap();
            CreateMap<RoleToGet, RoleToUpdate>().ReverseMap();

            CreateMap<TeamToGet, TeamToAdd>().ReverseMap();
            CreateMap<TeamToGet, TeamToUpdate>().ReverseMap();

            CreateMap<MonthTimekeepingToGet, MonthTimekeepingToAdd>().ReverseMap();
            CreateMap<MonthTimekeepingToGet, MonthTimekeepingToUpdate>().ReverseMap();

            CreateMap<TimekeepingToGet, TimekeepingToAdd>().ReverseMap();
            CreateMap<TimekeepingToGet, TimekeepingToUpdate>().ReverseMap();

            CreateMap<ViolationMoneyToGet, ViolationMoneyToAdd>().ReverseMap();
            CreateMap<ViolationMoneyToGet, ViolationMoneyToUpdate>().ReverseMap();

            CreateMap<WageToGet, WageToAdd>().ReverseMap();
            CreateMap<WageToGet, WageToUpdate>().ReverseMap();

            CreateMap<Wage_TypeToGet, Wage_TypeToAdd>().ReverseMap();
            CreateMap<Wage_TypeToGet, Wage_TypeToUpdate>().ReverseMap();
        }
    }
}
