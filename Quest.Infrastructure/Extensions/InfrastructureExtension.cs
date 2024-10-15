using Microsoft.Extensions.DependencyInjection;
using Quest.Infrastructure.Interfaces;
using Quest.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quest.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IQuestRepository, QuestRepository>();
            services.AddScoped<IQuestPlayerRepository, QuestPlayerRepository>();

            return services;
        }
    }
}
