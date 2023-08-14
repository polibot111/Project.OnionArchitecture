
using Application.RequestObjForSomeBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RandomLocationCreator
{
    public class IRandomLocationCreator
    {
        public Location LocationCreator(double latitude, double longitude, double halfDiameter)
        {
            Random random = new Random();

            double randomAngle = 2 * Math.PI * random.NextDouble();
            double randomDistance = halfDiameter * Math.Sqrt(random.NextDouble()); 

            double newLatitude = latitude + randomDistance / 111.32 * Math.Cos(randomAngle);
            double newLongitude = longitude + randomDistance / (111.32 * Math.Cos(latitude * Math.PI / 180)) * Math.Sin(randomAngle);

            Location location = new() { Latitude = newLatitude, Longitude = newLongitude };

            return location;
        }
    }
}
