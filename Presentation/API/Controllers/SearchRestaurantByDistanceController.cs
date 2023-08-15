using API.Filters;
using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ResponseFilterAttribute))]
    public class SearchRestaurantByDistanceController : ControllerBase
    {
        readonly IMediator _mediator;

        public SearchRestaurantByDistanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> SearchLocationByDistance([FromQuery] RestaurantBranchesQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
