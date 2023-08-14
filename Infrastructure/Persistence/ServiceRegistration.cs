using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ProjectDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));

            services.AddScoped<IWriteRestaurantBranchesRepo, WriteRestaurantBranchesRepo>();
            services.AddScoped<IReadRestaurantBranchesRepo, ReadRestaurantBranchesRepo>();

        }
    }
}
