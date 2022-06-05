using AutoMapper;
using Domain.Modals.Allowance;
using Domain.Modals.Bonus_Wage;
using Domain.Modals.Company;
using Domain.Modals.Department;
using Domain.Modals.LaborContract;
using Domain.Modals.Menu;
using Domain.Modals.MonthlySalary;
using Domain.Modals.Overtime;
using Domain.Modals.Position;
using Domain.Modals.Role;
using Domain.Modals.Team;
using Domain.Modals.Timekeeping;
using Domain.Modals.MonthTimekeeping;
using Domain.Modals.User;
using Domain.Modals.ViolationMoney;
using Domain.Modals.Wage;
using Domain.Modals.WageType;
using Server.Entities;

namespace Server
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserToAdd>().ReverseMap();
            CreateMap<User, UserToGet>().ReverseMap();
            CreateMap<User, UserToUpdate>().ReverseMap();

            CreateMap<Allowance, AllowanceToAdd>().ReverseMap();
            CreateMap<Allowance, AllowanceToGet>().ReverseMap();
            CreateMap<Allowance, AllowanceToUpdate>().ReverseMap();

            CreateMap<Bonus_Wage, Bonus_WageToAdd>().ReverseMap();
            CreateMap<Bonus_Wage, Bonus_WageToGet>().ReverseMap();
            CreateMap<Bonus_Wage, Bonus_WageToUpdate>().ReverseMap();

            CreateMap<Company, CompanyToAdd>().ReverseMap();
            CreateMap<Company, CompanyToGet>().ReverseMap();
            CreateMap<Company, CompanyToUpdate>().ReverseMap();

            CreateMap<Department, DepartmentToAdd>().ReverseMap();
            CreateMap<Department, DepartmentToGet>().ReverseMap();
            CreateMap<Department, DepartmentToUpdate>().ReverseMap();

            CreateMap<LaborContract, LaborContractToAdd>().ReverseMap();
            CreateMap<LaborContract, LaborContractToGet>().ReverseMap();
            CreateMap<LaborContract, LaborContractToUpdate>().ReverseMap();

            CreateMap<Menu, MenuToAdd>().ReverseMap();
            CreateMap<Menu, MenuToGet>().ReverseMap();
            CreateMap<Menu, MenuToUpdate>().ReverseMap();

            CreateMap<MonthlySalary, MonthlySalaryToAdd>().ReverseMap();
            CreateMap<MonthlySalary, MonthlySalaryToGet>().ReverseMap();
            CreateMap<MonthlySalary, MonthlySalaryToUpdate>().ReverseMap();

            CreateMap<Overtime, OvertimeToAdd>().ReverseMap();
            CreateMap<Overtime, OvertimeToGet>().ReverseMap();
            CreateMap<Overtime, OvertimeToUpdate>().ReverseMap();

            CreateMap<Position, PositionToAdd>().ReverseMap();
            CreateMap<Position, PositionToGet>().ReverseMap();
            CreateMap<Position, PositionToUpdate>().ReverseMap();

            CreateMap<Role, RoleToAdd>().ReverseMap();
            CreateMap<Role, RoleToGet>().ReverseMap();
            CreateMap<Role, RoleToUpdate>().ReverseMap();

            CreateMap<Team, TeamToAdd>().ReverseMap();
            CreateMap<Team, TeamToGet>().ReverseMap();
            CreateMap<Team, TeamToUpdate>().ReverseMap();

            CreateMap<Timekeeping, TimekeepingToAdd>().ReverseMap();
            CreateMap<Timekeeping, TimekeepingToGet>().ReverseMap();
            CreateMap<Timekeeping, TimekeepingToUpdate>().ReverseMap();

            CreateMap<MonthTimeKeeping, MonthTimekeepingToAdd>().ReverseMap();
            CreateMap<MonthTimeKeeping, MonthTimekeepingToGet>().ReverseMap();
            CreateMap<MonthTimeKeeping, MonthTimekeepingToUpdate>().ReverseMap();

            CreateMap<ViolationMoney, ViolationMoneyToAdd>().ReverseMap();
            CreateMap<ViolationMoney, ViolationMoneyToGet>().ReverseMap();
            CreateMap<ViolationMoney, ViolationMoneyToUpdate>().ReverseMap();

            CreateMap<Wage, WageToAdd>().ReverseMap();
            CreateMap<Wage, WageToGet>().ReverseMap();
            CreateMap<Wage, WageToUpdate>().ReverseMap();

            CreateMap<Wage_Type, Wage_TypeToAdd>().ReverseMap();
            CreateMap<Wage_Type, Wage_TypeToGet>().ReverseMap();
            CreateMap<Wage_Type, Wage_TypeToUpdate>().ReverseMap();
        }
    }
}
