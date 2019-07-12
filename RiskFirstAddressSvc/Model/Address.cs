using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskFirstAddressSvc.Model
{
    public class Address
    {

        public string FirstName { get => _firstName; set => _firstName = ProperNounClean(value); }
        public string LastName { get => _lastName; set => _lastName = ProperNounClean(value); }
        public string StreetAddress { get => _streetAddress; set => _streetAddress = ProperNounClean(value);  }
        public string City { get => _city; set => _city = ProperNounClean(value);  }
        public string Country { get => _country; set => _country = ProperNounClean(value);  }

        private string _firstName;
        private string _lastName;
        private string _streetAddress;
        private string _city;
        private string _country;

        private string ProperNounClean(string name)
        {
            var retVal = string.Empty;
            var words = name.Split(' ');
            foreach(var w in words)
            {
                if(char.IsLower(w[0]))
                {
                    retVal += char.ToUpper(w[0]) + w.Substring(1) + ' ';
                }
                else
                {
                    retVal += w + ' ';
                }
            }
            return retVal.TrimEnd();
        }
       
    }
}
