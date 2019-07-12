using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RiskFirstAddressSvc.Data;
using RiskFirstAddressSvc.Model;

namespace RiskFirstAddressSvc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        private readonly IAddressDataSource _dataSrc;
        private readonly ILogger<AddressController> _logger;

        /// <summary>
        /// Controller for REST calls pertaining to user addresses
        /// </summary>
        /// <param name="dataSrc">DataSource for address data</param>
        public AddressController(IAddressDataSource dataSrc, ILogger<AddressController> logger)
        {
            _dataSrc = dataSrc;
            _logger = logger;
        }

        /// <summary>
        /// Returns all user addresses grouped by city
        /// </summary>
        /// <returns>A collection of city names and the addresses that are found in each city</returns>
        [HttpGet("AddressByCity")]
        public IEnumerable<AddressGroup> GetAddressesByCity()
        {
            try
            {
                var all = _dataSrc.GetAllAddresses();
                return all.GroupBy(a => a.City).Select(g => new AddressGroup() { CityGroupName = g.Key, Addresses = g.ToList() }); 
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error ocurred returning address groups: {ex}");
                throw ex;
            }
        }


        
    }
}
