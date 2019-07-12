using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiskFirstAddressSvc.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace RiskFirstAddressSvcTest
{
    [TestClass]
    public class DataAccessTest
    {
        [TestMethod]
        public void TestDataLoad()
        {
            var logger = new Mock<ILogger<JsonFileAddressDataSource>>();
            var config = new Mock<IConfiguration>();
            config.Setup(c => c["AddressDataFile"]).Returns("TestAddresses.json").Verifiable();
            var src = new JsonFileAddressDataSource(config.Object, logger.Object);
            var addresses = src.GetAllAddresses();
            Assert.AreEqual(addresses.Count(), 3);

            var smith = addresses.Where(a => a.LastName == "Smith").FirstOrDefault();
            Assert.IsNotNull(smith);
            Assert.AreEqual(smith.FirstName, "John");
            Assert.AreEqual(smith.City, "London");
            Assert.AreEqual(smith.Country, "England");
            Assert.AreEqual(smith.StreetAddress, "Test St 1");

            var doe = addresses.Where(a => a.LastName == "Doe").FirstOrDefault();
            Assert.IsNotNull(doe);
            Assert.AreEqual(doe.FirstName, "Jane");
            Assert.AreEqual(doe.City, "London");
            Assert.AreEqual(doe.Country, "England");
            Assert.AreEqual(doe.StreetAddress, "Test St 2");
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LoadDataThrows()
        {
            var logger = new Mock<ILogger<JsonFileAddressDataSource>>();
            var config = new Mock<IConfiguration>();
            config.Setup(c => c["AddressDataFile"]).Returns("Notfound.json").Verifiable();
            var src = new JsonFileAddressDataSource(config.Object, logger.Object);
            src.Init();
        }

        
    }
}
