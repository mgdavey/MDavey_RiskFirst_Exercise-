using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RiskFirstAddressSvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using log4net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace RiskFirstAddressSvc.Data
{
    /// <summary>
    /// Implementation 
    /// </summary>
    public class JsonFileAddressDataSource : IAddressDataSource
    {
        private readonly ILogger<JsonFileAddressDataSource> _logger;
        private IEnumerable<Address> _addresses;
        private readonly string _filePath;

        public JsonFileAddressDataSource(IConfiguration config, ILogger<JsonFileAddressDataSource> logger)
        {
            _filePath = config["AddressDataFile"];
            _logger = logger;
        }

        /// <summary>
        /// Returns all addresses in datastore
        /// </summary>
        /// <returns>A collection of all the individual lines in the data store</returns>
        public IEnumerable<Address> GetAllAddresses()
        {
            if (_addresses == null)
            {
                Init();
            }
            return _addresses;
        }

        public void Init()
        {
            try
            {
                _logger.LogInformation($"Initializing AddressFileDataSoure. Loading address file { _filePath }");
                if (_addresses == null)
                {
                    _addresses = new List<Address>();
                    using (TextReader rdr = new StreamReader(_filePath))
                    {
                        _addresses = (List<Address>)JsonConvert.DeserializeObject(rdr.ReadToEnd(), typeof(List<Address>));
                        _logger.LogInformation($"Address file loaded {_addresses.Count()} addresses");
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Unable to initialize Address Data", ex);
                throw;
            }
        }

       
    }
}
