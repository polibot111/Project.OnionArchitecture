using Application.Interface;
using Application.Repositories;
using Application.RequestObjForSomeBusiness;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class DummyDataCreater
    {
        readonly IRandomLocationCreator _randomLocationCreator;
        readonly IWriteRestaurantBranchesRepo _writeRestaurantBranchesRepo;
        readonly IServiceProvider _serviceProvider;

        public DummyDataCreater(IRandomLocationCreator randomLocationCreator, IWriteRestaurantBranchesRepo writeRestaurantBranchesRepo, IServiceProvider serviceProvider)
        {
            _randomLocationCreator = randomLocationCreator;
            _writeRestaurantBranchesRepo = writeRestaurantBranchesRepo;
            _serviceProvider = serviceProvider;
        }

        public async Task AddRestaurant()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
                
                if (!dbContext.RestorantBranches.Any())
                {
                    List<RestaurantBranches> restaurants = new();

                    for (int i = 0; i < 30; i++)
                    {

                        Location location = _randomLocationCreator.LocationCreator(66.8362587677073, 142.07585514153521, 10000);

                        RestaurantBranches restaurant = new RestaurantBranches { Latitude = location.Latitude, Longitude = location.Longitude, Name = $"Resraurant{i + DateTime.Now.Second}" };

                        restaurants.Add(restaurant);
                    }

                    await _writeRestaurantBranchesRepo.AddRangeAsync(restaurants);

                    await _writeRestaurantBranchesRepo.SaveAsync();

                }
            }

           

        }


    }
}
