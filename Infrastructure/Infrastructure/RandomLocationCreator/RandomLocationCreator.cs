using Application.Helpers;
using Application.RandomLocationCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class RandomLocationCreator : IRandomLocationCreator
    {

        public Location LocationCreator(double latitude, double longitude, double halfDiameter)
        {
            Random random = new Random();

            // Radyan cinsinden açı ve mesafe hesaplamaları
            double randomAngle = 2 * Math.PI * random.NextDouble(); // 0 ile 2*pi arasında rastgele bir açı
            double randomDistance = halfDiameter * Math.Sqrt(random.NextDouble()); // 0 ile 10000 arasında rastgele bir mesafe

            // Yeni konumun koordinatlarını hesapla
            double newLatitude = latitude + (randomDistance / 111.32) * Math.Cos(randomAngle); // Enlem hesabı
            double newLongitude = longitude + (randomDistance / (111.32 * Math.Cos(latitude * Math.PI / 180))) * Math.Sin(randomAngle); // Boylam hesabı

            Location location = new() { Latitude = newLatitude, Longitude = newLongitude };

            return location;
        }
    }
}
