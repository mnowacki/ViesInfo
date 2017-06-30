namespace ViesInfo
{
    public class ViesClient
    {
        private const string ProdUrl = @"http://ec.europa.eu/taxation_customs/vies/services/checkVatService";
        private const string TestUrl = @"http://ec.europa.eu/taxation_customs/vies/services/checkVatTestService";

        public ViesClient(bool forTesting = false)
        {
            Address = forTesting ? TestUrl : ProdUrl;
        }

        public string Address { get; }

        public ViesCompanyInfo GetCompany(string vatNumber)
        {
            string requestIdentifier;
            bool isValid;
            return GetCompany(vatNumber, null, out isValid, out requestIdentifier);
        }
        public ViesCompanyInfo GetCompany(string vatNumber, string askingVatNumber, out bool isValid, out string requestIdentifier)
        {
            vatNumber = NormalizeVatNumber(vatNumber);

            using (var client = new checkVatService())
            {
                string countryCode = vatNumber.Substring(0, 2);
                vatNumber = vatNumber.Substring(2, vatNumber.Length - 2);

                string traderName = string.Empty;
                string traderCompanyType = string.Empty;
                string traderStreet = string.Empty;
                string traderPostcode = string.Empty;
                string traderCity = string.Empty;
                string requesterCountryCode = string.Empty;
                string requesterVatNumber = string.Empty;

                if (askingVatNumber != null)
                {
                    requesterCountryCode = askingVatNumber.Substring(0, 2);
                    requesterVatNumber = askingVatNumber.Substring(2, vatNumber.Length - 2);
                }

                bool valid;
                string traderAddress;
                matchCode traderNameMatch;
                bool traderNameMatchSpecified;
                matchCode traderCompanyTypeMatch;
                bool traderCompanyTypeMatchSpecified;
                matchCode traderStreetMatch;
                bool traderStreetMatchSpecified;
                matchCode traderPostcodeMatch;
                bool traderPostcodeMatchSpecified;
                matchCode traderCityMatch;
                bool traderCityMatchSpecified;
                //string requestIdentifier;

                var result = client.checkVatApprox(
                    ref countryCode,
                    ref vatNumber,
                    ref traderName,
                    ref traderCompanyType,
                    ref traderStreet,
                    ref traderPostcode,
                    ref traderCity,
                    requesterCountryCode,
                    requesterVatNumber,
                    out valid,
                    out traderAddress,
                    out traderNameMatch,
                    out traderNameMatchSpecified,
                    out traderCompanyTypeMatch,
                    out traderCompanyTypeMatchSpecified,
                    out traderStreetMatch,
                    out traderStreetMatchSpecified,
                    out traderPostcodeMatch,
                    out traderPostcodeMatchSpecified,
                    out traderCityMatch,
                    out traderCityMatchSpecified,
                    out requestIdentifier);

                isValid = valid;

                return new ViesCompanyInfo
                {
                    Name = traderName,
                    VatNumber = vatNumber,
                    Address = traderAddress,
                    CountryCode = countryCode
                };
            }
        }

        private string NormalizeVatNumber(string vatNumber)
        {
            return vatNumber;
        }
    }
}
