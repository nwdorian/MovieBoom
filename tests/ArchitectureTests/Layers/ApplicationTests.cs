using NetArchTest.Rules;
using Shouldly;

namespace ArchitectureTests.Layers;

public class ApplicationTests : BaseTest
{
    [Fact]
    public void Application_Should_NotHaveDependencyOn_Infrastructure()
    {
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Application_Should_NotHaveDependencyOn_Web()
    {
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(WebAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
