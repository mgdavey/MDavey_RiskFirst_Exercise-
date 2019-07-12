using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RiskFirstAddressSvc.Controllers;
using RiskFirstAddressSvc.Data;
using RiskFirstAddressSvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskFirstAddressSvcTest
{
    [TestClass]
    public class AddressControllerTest
    {

        [TestMethod]
        public void TestGetAddressCallsDataSource()
        {
            var dataSrc = new Mock<IAddressDataSource>();
            var logger = new Mock<ILogger<AddressController>>();
            var addresses = CreateAddresses();
            dataSrc.Setup(d => d.GetAllAddresses()).Returns(addresses).Verifiable();
            var target = new AddressController(dataSrc.Object, logger.Object);
            var results = target.GetAddressesByCity();

            var london = results.Where(r => r.CityGroupName == "London").FirstOrDefault();
            var rome = results.Where(r => r.CityGroupName == "Rome").FirstOrDefault();
            var nyc = results.Where(r => r.CityGroupName == "New York").FirstOrDefault();
            var chi = results.Where(r => r.CityGroupName == "Chicago").FirstOrDefault();

            Assert.IsNotNull(london);
            Assert.IsNotNull(rome);
            Assert.IsNotNull(nyc);
            Assert.IsNotNull(chi);
            Assert.AreEqual(london.Addresses.Count(), 1);
            Assert.AreEqual(rome.Addresses.Count(), 1);
            Assert.AreEqual(nyc.Addresses.Count(), 2);
            Assert.AreEqual(chi.Addresses.Count(), 1);

            var bruno = rome.Addresses.Where(a => a.LastName == "Bruno").FirstOrDefault();
            var brown = london.Addresses.Where(a => a.LastName == "Brown").FirstOrDefault();
            var francis = nyc.Addresses.Where(a => a.LastName == "Francis").FirstOrDefault();
            var davey = nyc.Addresses.Where(a => a.LastName == "Davey").FirstOrDefault();
            var king = chi.Addresses.Where(a => a.LastName == "King").FirstOrDefault();

            Assert.IsNotNull(bruno);
            Assert.IsNotNull(brown);
            Assert.IsNotNull(francis);
            Assert.IsNotNull(davey);
            Assert.IsNotNull(king);

            dataSrc.VerifyAll();
        }

        private static IEnumerable<Address> CreateAddresses()
        {
            var addresses = new List<Address>()
            {
                new Address(){ City = "Rome",FirstName = "Giovanni",LastName = "Bruno",Country = "Italy"},
                new Address(){ City = "London", FirstName = "Tom", LastName = "Brown", Country = "England"},
                new Address() { City= "New York", FirstName = "Evelyn", LastName = "Francis", Country = "USA"},
                new Address() { City = "New York", FirstName = "Michael", LastName = "Davey", Country = "USA"},
                new Address() { City = "Chicago", FirstName = "Raymond", LastName = "King", Country = "USA"}
             };
            return addresses;
        }
            
        }
    }

