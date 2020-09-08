using Microsoft.Extensions.DependencyInjection;
using TestWebApi.Data;
using TestWebApi.Services;

namespace TestWebApi.Exensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            services.AddSingleton<DataContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<PatientsService>();
            services.AddScoped<AppointmentsService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); 

            return services;
        }
    }
}