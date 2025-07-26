using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PatternTemplate.DAL;

namespace PatternTemplate.IOC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            #region DAL

            /// Register UnitOfWork
            services.AddScoped<IUnitOfWork<PatternTemplateDbContext>, UnitOfWork<PatternTemplateDbContext>>();

            // Register Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Register HttpContextAccessor and Configuration
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion
            // var BllClasses = typeof(PageBll).Assembly.GetTypes().Where(p => p.IsClass && p.Name.ToLower().Contains("bll"));

            #region BLL

            #endregion

        }
    }

}
