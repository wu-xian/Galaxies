using Galaxies.Logic.BIZ;
using Galaxies.Logic.DAL.MYSQL;
using Galaxies.Logic.IDAL;
using Galaxies.Model.Context;
using Galaxies.Model.LogicModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galaxies.Core.Services
{
    public static class GalaxiesServiceCollectionExtensions
    {
        public static void AddGalaxiesServices(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            //add and config  galaxies dbcontext
            services.AddDbContext<GalaxiesDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Galaxies")
                );
            }
            );
            //add services
            services.AddSingleton<GalaxiesMaker>();
            services.AddScoped<ContextService>();
            services.AddScoped<CookieService>();
            services.AddScoped<SecurityService>();
            services.AddScoped<SessionService>();
            //add dal
            services.AddScoped<IUserDAL, UserDAL>();
            services.AddScoped<IRoleDAL, RoleDAL>();
            services.AddScoped<IUserRoleClaimDAL, UserRoleClaimDAL>();
            services.AddScoped<IProgramForWebDAL, ProgramForWebDAL>();
            services.AddScoped<IProgramRoleClaimDAL, ProgramRoleClaimDAL>();
            services.AddScoped<IMenuForWebDAL, MenuForWebDAL>();
            services.AddScoped<IMenuForWebInRolesDAL, MenuForWebInRolesDAL>();
            //add biz
            services.AddScoped<UserBIZ>();
            services.AddScoped<RoleBIZ>();
            services.AddScoped<UserRoleClaimBIZ>();
            services.AddScoped<ProgramForWebBIZ>();
            services.AddScoped<ProgramRoleClaimBIZ>();
            services.AddScoped<MenuForWebBIZ>();
            services.AddScoped<MenuForWebInRolesBIZ>();

            services.AddMvc();
        }
    }

    public class GalaxiesMaker
    { }
}
