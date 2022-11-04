using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.DBContext;
using Server.Entities;
using Server.Repositories;
using Server.Services;
using System.Text;

namespace Server
{
    public class DependencyInjection
    {
        private readonly IConfiguration _configuration;
        public DependencyInjection(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public void InjectDependencies(IServiceCollection services)
        {
            services.AddDbContext<HRM_DbContext>(
                item => item.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<HRM_DbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidIssuer =_configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))

                };

            });
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IAllowanceRepository), typeof(AllowanceRepository));
            services.AddScoped(typeof(IBonus_WageRepository), typeof(Bonus_WageRepository));
            services.AddScoped(typeof(ICompanyRepository), typeof(CompanyRepository));
            services.AddScoped(typeof(IDepartmentRepository), typeof(DepartmentRepository));
            services.AddScoped(typeof(ILaborContractRepository), typeof(LaborContractRepository));
            services.AddScoped(typeof(IMenuRepository), typeof(MenuRepository));
            services.AddScoped(typeof(IMonthlySalaryRepository), typeof(MonthlySalaryRepository));
            services.AddScoped(typeof(IOvertimeRepository), typeof(OvertimeRepository));
            services.AddScoped(typeof(IPositionRepository), typeof(PositionRepository));
            services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            services.AddScoped(typeof(ITeamRepository), typeof(TeamRepository));
            services.AddScoped(typeof(ITimekeepingRepository), typeof(TimekeepingRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IViolationMoneyRepository), typeof(ViolationMoneyRepository));
            services.AddScoped(typeof(IWageRepository), typeof(WageRepository));
            services.AddScoped(typeof(IWage_TypeRepository), typeof(Wage_TypeRepository));
            
            services.AddTransient(typeof(IAllowanceService), typeof(AllowanceService));
            services.AddTransient(typeof(IBonus_WageService), typeof(Bonus_WageService));
            services.AddTransient(typeof(ICompanyService), typeof(CompanyService));
            services.AddTransient(typeof(IDepartmentService), typeof(DepartmentService));
            services.AddTransient(typeof(ILaborContractService), typeof(LaborContractService));
            services.AddTransient(typeof(IMenuService), typeof(MenuService));
            services.AddTransient(typeof(IMonthlySalaryService), typeof(MonthlySalaryService));
            services.AddTransient(typeof(IOvertimeService), typeof(OvertimeService));
            services.AddTransient(typeof(IPositionService), typeof(PositionService));
            services.AddTransient(typeof(IRoleService), typeof(RoleService));
            services.AddTransient(typeof(ITeamService), typeof(TeamService));
            services.AddTransient(typeof(ITimekeepingService), typeof(TimekeepingService));
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IViolationMoneyService), typeof(ViolationMoneyService));
            services.AddTransient(typeof(IWageService), typeof(WageService));
            services.AddTransient(typeof(IWage_TypeService), typeof(Wage_TypeService));
        }
    }
}
