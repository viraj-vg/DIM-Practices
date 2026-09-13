using FintechJobPortal.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FintechJobPortal.Tests;

public class HealthEndpointTests
{
    [Fact]
    public void CheckHealth_ShouldReturnOkStatusHealthy()
    {
        // Arrange
        var controller = new HealthController();

        // Act
        var result = controller.CheckHealth() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Value);
    }
}
