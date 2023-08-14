using Application.Helpers;
using Application.RandomLocationCreator;
using Application.Repositories;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantBranchesController : ControllerBase
    {

        public RestaurantBranchesController(IWriteRestaurantBranchesRepo writeRestaurantBranchesRepo, IReadRestaurantBranchesRepo readRestaurantBranchesRepo, IRandomLocationCreator randomLocationCreator)
        {
            _writeRestaurantBranchesRepo = writeRestaurantBranchesRepo;
            _readRestaurantBranchesRepo = readRestaurantBranchesRepo;
            _randomLocationCreator = randomLocationCreator;
        }

        readonly private IWriteRestaurantBranchesRepo _writeRestaurantBranchesRepo;
        readonly private IReadRestaurantBranchesRepo _readRestaurantBranchesRepo;
        readonly private IRandomLocationCreator _randomLocationCreator;

        [HttpGet]
        public async Task Test()
        {
            double currentLatitude = 41.0082;
            double currentLongitude = 28.9784;

            Random random = new Random();

            List<RestaurantBranches> restaurantBranches = new List<RestaurantBranches>();
            for (int i = 0; i < 30; i++)
            {
                Location location = _randomLocationCreator.LocationCreator(currentLatitude, currentLongitude, 10000);
                restaurantBranches.Add(new()
                {
                    Longitude= location.Longitude,
                    Latitude= location.Latitude,
                    Name = $"Restorant{i}"
                });
            }

            await _writeRestaurantBranchesRepo.AddRangeAsync(restaurantBranches);

            var count = await _writeRestaurantBranchesRepo.SaveAsync();
        }
    }
}
