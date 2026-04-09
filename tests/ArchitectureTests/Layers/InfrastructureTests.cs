using NetArchTest.Rules;
using Shouldly;
using TestResult = NetArchTest.Rules.TestResult;

namespace ArchitectureTests.Layers;

public class InfrastructureTests : BaseTest
{
    [Fact]
    public void Infrastructure_Should_NotHaveDependencyOn_Web()
    {
        TestResult result = Types
            .InAssembly(InfrastructureAssembly)
            .Should()
            .NotHaveDependencyOn(WebAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}
