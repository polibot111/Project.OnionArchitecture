using Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.RandomLocationCreator
{
    public interface IRandomLocationCreator
    {
        Location LocationCreator(double latitude, double longitude, double halfDiameter);
    }
}
