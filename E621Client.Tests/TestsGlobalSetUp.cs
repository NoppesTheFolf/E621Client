using Microsoft.Extensions.Configuration;

namespace Noppes.E621.Tests;

[SetUpFixture]
public class TestsGlobalSetUp
{
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var configuration = new ConfigurationBuilder().AddUserSecrets<TestsGlobalSetUp>().Build();
        var options = configuration.GetRequiredSection(E621Options.SectionName).Get<E621Options>()!;

        TestsHelper.E621Client = new E621ClientBuilder()
            .WithUserAgent("E621Client.Tests", "n/a", "NoppesTheFolf", "GitHub")
            .WithRequestInterval(E621Constants.MinimumRequestInterval)
            .Build();

        await TestsHelper.E621Client.LogInAsync(options.Username, options.ApiKey, true);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        TestsHelper.E621Client?.Dispose();
    }
}
