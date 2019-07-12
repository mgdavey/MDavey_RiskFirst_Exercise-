using RiskFirstAddressSvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskFirstAddressSvc.Data
{
    /// <summary>
    /// Interface that returns address data.
    /// </summary>
    public interface IAddressDataSource
    {
        IEnumerable<Address> GetAllAddresses();
        void Init();
    }
}
