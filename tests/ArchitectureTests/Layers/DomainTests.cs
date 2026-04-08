using NetArchTest.Rules;
using Shouldly;
using TestResult = NetArchTest.Rules.TestResult;

namespace ArchitectureTests.Layers;

public class DomainTests : BaseTest
{
    [Fact]
    public void Domain_Should_NotHaveDependencyOn_Application()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Domain_Should_NotHaveDependencyOn_Infrastructure()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Domain_Should_NotHaveDependencyOn_Web()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(WebAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
