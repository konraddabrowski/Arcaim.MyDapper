using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Arcaim.MyDapper
{
    public static class Extensions
    {
        public static IServiceCollection AddMyDapper(
            this IServiceCollection services,
            Func<IServiceProvider, IDbConnection> action)
        {
            services.AddScoped<IDbConnection>(action);
            services.AddScoped<IDapperWrapper, DapperWrapper>();

            return services;
        }
    }
}