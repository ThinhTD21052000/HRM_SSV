using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Configuration;
using Server.Entities;

namespace Server.DBContext
{
    public class HRM_DbContext : IdentityDbContext<User, Role, Guid>
    {
        public HRM_DbContext(DbContextOptions<HRM_DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AllowanceConfig());
            modelBuilder.ApplyConfiguration(new Bonus_WageConfig());
            modelBuilder.ApplyConfiguration(new CompanyConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new LaborContractConfig());
            modelBuilder.ApplyConfiguration(new MenuConfig());
            modelBuilder.ApplyConfiguration(new MonthlySalaryConfig());
            modelBuilder.ApplyConfiguration(new OvertimeConfig());
            modelBuilder.ApplyConfiguration(new PositionConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new TimekeepingConfig());
            modelBuilder.ApplyConfiguration(new MonthTimekeepingConfig());
            modelBuilder.ApplyConfiguration(new ViolationMoneyConfig());
            modelBuilder.ApplyConfiguration(new TeamConfig());
            modelBuilder.ApplyConfiguration(new WageConfig());
            modelBuilder.ApplyConfiguration(new Wage_TypeConfig());
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(x => new { x.UserId });
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(x => new { x.UserId });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Allowance> Allowance { get; set; }
        public DbSet<Bonus_Wage> Bonus_Wage { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<LaborContract> LaborContract { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MonthlySalary> MonthlySalary { get; set; }
        public DbSet<Overtime> Overtime { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Timekeeping> Timekeeping { get; set; }
        public DbSet<MonthTimeKeeping> MonthTimekeeping { get; set; }
        public DbSet<ViolationMoney> ViolationMoney { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Wage> Wage { get; set; }
        public DbSet<Wage_Type> Wage_Type { get; set; }
    }
}
