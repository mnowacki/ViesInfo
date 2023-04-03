using ViesInfo.checkVatService;

namespace ViesInfo
{
    public class ViesClient
    {
        private const string ProdUrl = @"https://ec.europa.eu/taxation_customs/vies/services/checkVatService";
        private const string TestUrl = @"https://ec.europa.eu/taxation_customs/vies/services/checkVatTestService";

        public ViesClient(bool forTesting = false)
        {
            Address = forTesting ? TestUrl : ProdUrl;
        }

        public string Address { get; }

        public ViesCompanyInfo GetCompany(string vatNumber)
        {
            return GetCompany(vatNumber, null, out _, out _);
        }

        public ViesCompanyInfo GetCompany(string vatNumber, string askingVatNumber, out bool isValid,
            out string requestIdentifier)
        {
            using (var client = new checkVatPortTypeClient())
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

                client.checkVatApprox(
                    ref countryCode,
                    ref vatNumber,
                    ref traderName,
                    ref traderCompanyType,
                    ref traderStreet,
                    ref traderPostcode,
                    ref traderCity,
                    requesterCountryCode,
                    requesterVatNumber,
                    out var valid,
                    out var traderAddress,
                    out _,
                    out _,
                    out _,
                    out _,
                    out _,
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
    }
}