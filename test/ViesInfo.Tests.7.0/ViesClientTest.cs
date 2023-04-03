using Xunit;

namespace ViesInfo.Tests._7._0;

public class ViesClientTest
{
    [Fact]
    public void viesclient_shoud_return_info_about_gus()
    {
        var sut = new ViesClient(forTesting: true);

        var result = sut.GetCompany("PL5261040828");

        Assert.NotNull(result);
        Assert.Equal("GŁÓWNY URZĄD STATYSTYCZNY", result.Name);
    }
}