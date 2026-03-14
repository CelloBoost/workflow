using Workflow.Api.Rest.System.Mappers;

namespace Workflow.Tests.Unit.System;

[TestFixture]
public sealed class SystemMapperTests
{
    [Test]
    public void ToResponse_ShouldMapVersion()
    {
        var result = SystemMapper.ToResponse("1.0.0");

        Assert.That(result.Version, Is.EqualTo("1.0.0"));
    }
}
