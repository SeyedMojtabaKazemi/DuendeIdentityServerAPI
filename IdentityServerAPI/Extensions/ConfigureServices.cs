using ApplicationService.Command.UserAggregate;
using Contract;
using Contract.UserAggregate;
using Domain;
using Domain.UserAggregate;
using Infra;
using Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerAPI.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection UseSql(this IServiceCollection services,IConfiguration configuration)
        {
            string conn = configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<IdentityServerDbContext>(option => option.UseSqlServer(conn));
            return services;
        }

        public static IServiceCollection AddScopeConfigure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IUserServiceCommand, UserServiceCommand>();
            services.AddIdentity<User, Role>(option =>
            {
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 1;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireDigit = false;
                option.Password.RequiredUniqueChars = 1;
                }
            )
                .AddEntityFrameworkStores<IdentityServerDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
