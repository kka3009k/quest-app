using Microsoft.Extensions.DependencyInjection;
using Quest.Application.Interfaces;
using Quest.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IQuestService, QuestService>();

            services.AddInfrastructure();

            return services;
        }
    }
}
