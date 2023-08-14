using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchRestaurantByDistanceController : ControllerBase
    {
        readonly IMediator _mediator;

        public SearchRestaurantByDistanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> SearchLocationByDistance([FromQuery] RestaurantBranchesRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
