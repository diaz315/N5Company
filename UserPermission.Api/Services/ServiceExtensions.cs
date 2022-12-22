using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Configuration;
using System.Resources;
using UserPermission.Business;
using UserPermission.Domain;
using UserPermission.Domain.Interface.Business;
using UserPermission.Domain.Interface.Repository;
using UserPermission.Repository;

namespace UserPermission.Api.Services
{
    public static class ServiceExtensions
    {
        //ISaveOperationService

        public static void InjectCustomService(this IServiceCollection services)
        {
            //Repositorio
            //services.AddScoped<IEmployeeGeneralDataRepository<EmployeeGeneralDataDTO, RecordProfileDTO>, EmployeeGeneralDataRepository>();
            //Servicios

            services.AddScoped<ISaveOperationService, SaveOperationService>();
            services.AddScoped<IKafkaService, KafkaService>();
            services.AddScoped<IPermissionService<Permission>, PermissionService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IElasticSearchService<Permission>, ElasticSearchService>();

        }

        public static IApplicationBuilder UResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseMiddleware>();
        }


        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {

                error.Run(async context =>
                {
                    //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Error(string.Format("Algo salio mal en {0}", contextFeature.Error));

                        await context.Response.WriteAsync(new Domain.Error
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error?.Message 
                        }.ToString());
                    }
                });
            });
        }

    }
}
