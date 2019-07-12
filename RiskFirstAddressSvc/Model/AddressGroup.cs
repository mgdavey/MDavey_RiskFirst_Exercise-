using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskFirstAddressSvc.Model
{
    /// <summary>
    /// Class to represent the basic return type for this exercise.
    /// </summary>
    public class AddressGroup
    {
        public string CityGroupName { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
    }
}
