//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;

//namespace Server.DBContext
//{
//    public class HRM_DbContextDesignFactory : IDesignTimeDbContextFactory<HRM_DbContext>
//    {
//        public HRM_DbContext CreateDbContext(string[] args)
//        {
//            IConfiguration configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .Build();
//            var connect = configuration.GetConnectionString("DefaultConnection");
//            var optionBuilder = new DbContextOptionsBuilder<HRM_DbContext>();
//            optionBuilder.UseSqlServer(connect);
//            return new HRM_DbContext(optionBuilder.Options);
//        }
//    }
//}
