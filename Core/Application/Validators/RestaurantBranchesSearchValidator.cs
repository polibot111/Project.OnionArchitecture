using Application.Features.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class RestaurantBranchesSearchValidator : AbstractValidator<RestaurantBranchesRequest>
    {
        public RestaurantBranchesSearchValidator()
        {
            RuleFor(x => x.Latitude).NotEmpty().NotNull().WithMessage("Latitude boş ya da null olamaz");
            RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).WithMessage("Latitude -90 ila 90 değerleri arasında olmalıdır");

            RuleFor(x => x.Longitude).NotEmpty().NotNull().WithMessage("Longitude boş ya da null olamaz");
            RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).WithMessage("Latitude -180 ila 180 değerleri arasında olmalıdır");

        }
    }
}
