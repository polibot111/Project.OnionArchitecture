using Application.RequestObjForSomeBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IRandomLocationCreator
    {
        Location LocationCreator(double latitude, double longitude, double halfDiameter);
    }
}
