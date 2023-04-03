# ViesInfo
ViesInfo is .NET library for validating EU VAT number. This package are using SOAP service from [EU site](https://ec.europa.eu/taxation_customs/vies). 

```
var info = new ViesClient().GetCompany("PL5261040828");
Console.Writeln($"Name:{info.Name}, Vat:{info.VatNumber}, Address:{info.Address}");
```
