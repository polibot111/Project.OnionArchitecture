using Application.DTO_s;
using Application.Features.Queries;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.SomeBusiness;
using Application.RequestObjForSomeBusiness;
using Domain.Entities;

namespace Application.Features.Queries
{
    public class RestaurantBranchesRequest : IRequest<RestaurantBranchesResponse>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Distance { get; set; }
        public int Count { get; set; }
    }

    public class RestaurantBranchesResponse
    {
        public List<RestaurantBranchesDTO> RestaurantBrancheDTO { get; set; }
    }

    public class RestaurantBranchesHandler : IRequestHandler<RestaurantBranchesRequest, RestaurantBranchesResponse>
    {
        readonly IReadRestaurantBranchesRepo _readRestaurantBranches;

        public RestaurantBranchesHandler(IReadRestaurantBranchesRepo readRestaurantBranches)
        {
            _readRestaurantBranches = readRestaurantBranches;
        }

        public async Task<RestaurantBranchesResponse> Handle(RestaurantBranchesRequest request, CancellationToken cancellationToken)
        {

            var restaurants =
                await Task.Run(() =>
                {
                 var restaurants = _readRestaurantBranches.GetAll().ToList();

                    return restaurants;
                });


            List<RestaurantBranchesDTO> restaurantBrancheDTO = restaurants.Select(x =>
            new RestaurantBranchesDTO
            {
                Id = x.id,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Name = x.Name,
                Distance = CalculateDistance.CalculateDistanceBusiness(
                    request.Latitude, request.Longitude,
                    x.Latitude, x.Longitude)
            }).Where(x => x.Distance < 10000).OrderBy(x => x.Distance).Take(5).ToList();

            return new() { RestaurantBrancheDTO = restaurantBrancheDTO };
        }

    }
}
