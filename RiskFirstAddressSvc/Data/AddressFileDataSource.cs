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

namespace RiskFirstAddressSvc.Data
{
    /// <summary>
    /// Implementation 
    /// </summary>
    public class JsonFileAddressDataSource : IAddressDataSource
    {

        private ILog _logger = LogManager.GetLogger(typeof(JsonFileAddressDataSource));

        public JsonFileAddressDataSource(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            if(_addresses == null)
            {
                Init();
            }
            return _addresses;
        }

        public void Init()
        {
            try
            {
                _logger.Info($"Initializing AddressFileDataSoure. Loading address file _filepath");
                if (_addresses == null)
                {
                    _addresses = new List<Address>();
                    using (TextReader rdr = new StreamReader(_filePath))
                    {
                        _addresses = (List<Address>)JsonConvert.DeserializeObject(rdr.ReadToEnd(), typeof(List<Address>));
                        _logger.Info($"Address file loaded _addresses.Count() addresses");
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.Error($"Unable to initialize Address Data", ex);
                throw;
            }
        }

        private IEnumerable<Address> _addresses;
        private readonly string _filePath;
    }
}
