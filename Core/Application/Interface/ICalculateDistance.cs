using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ICalculateDistance
    {
        double CalculateDistanceBusiness(double lat1, double lon1, double lat2, double lon2, char unit = 'K');
    }
}
