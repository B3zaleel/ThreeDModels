using System.IO;
using FluentAssertions;
using Xunit;

namespace ThreeDModels.Format.Obj.UnitTests;

public class ObjReaderTests
{
    [Theory]
    [InlineData("g", new[] { "g" })]
    [InlineData("cstype  bspline", new[] { "cstype", "bspline" })]
    [InlineData("g # bspline", new[] { "g" })]
    [InlineData("v 1.2 \\ \n1.3 \\ \n1.4", new[] { "v", "1.2", "1.3", "1.4" })]
    public void GetCommandLine_GivenTextReader_ReturnsExpectedCommandLineSections(string text, string[] expectedSections)
    {
        // Arrange
        var textReader = new StringReader(text);

        // Act
        var sections = ObjReader.GetCommandLine(textReader);

        // Assert
        sections.Should().BeEquivalentTo(expectedSections);
    }
}
