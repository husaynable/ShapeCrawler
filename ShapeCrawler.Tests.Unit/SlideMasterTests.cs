﻿using System.Linq;
using FluentAssertions;
using ShapeCrawler.Shapes;
using ShapeCrawler.Tests.Unit.Helpers;
using Xunit;

namespace ShapeCrawler.Tests.Unit
{
    public class SlideMasterTests : IClassFixture<PresentationFixture>
    {
        private readonly PresentationFixture _fixture;

        public SlideMasterTests(PresentationFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShapeXAndY_ReturnXAndYAxesCoordinatesOfTheMasterShape()
        {
            // Arrange
            ISlideMaster slideMaster = _fixture.Pre001.SlideMasters[0];
            IShape shape = slideMaster.Shapes.First(sp => sp.Id == 2);

            // Act
            int shapeXCoordinate = shape.X;
            int shapeYCoordinate = shape.Y;

            // Assert
            shapeXCoordinate.Should().Be((int)(838200 * TestHelper.HorizontalResolution / 914400));
            shapeYCoordinate.Should().Be((int)(365125 * TestHelper.VerticalResolution / 914400));
        }

        [Fact]
        public void ShapeWidthAndHeight_ReturnWidthAndHeightSizesOfTheMaster()
        {
            // Arrange
            ISlideMaster slideMaster = _fixture.Pre001.SlideMasters[0];
            IShape shape = slideMaster.Shapes.First(sp => sp.Id == 2);
            float horizontalResolution = TestHelper.HorizontalResolution;
            float verticalResolution = TestHelper.VerticalResolution;

            // Act
            int shapeWidth = shape.Width;
            int shapeHeight = shape.Height;

            // Assert
            shapeWidth.Should().Be((int)(10515600 * horizontalResolution / 914400));
            shapeHeight.Should().Be((int)(1325563 * verticalResolution / 914400));
        }

        [Fact]
        public void AutoShapePlaceholderType_ReturnsPlaceholderType()
        {
            // Arrange
            ISlideMaster slideMaster = _fixture.Pre001.SlideMasters[0];
            IShape masterAutoShapeCase1 = slideMaster.Shapes.First(sp => sp.Id == 2);
            IShape masterAutoShapeCase2 = slideMaster.Shapes.First(sp => sp.Id == 8);
            IShape masterAutoShapeCase3 = slideMaster.Shapes.First(sp => sp.Id == 7);

            // Act
            PlaceholderType? shapePlaceholderTypeCase1 = masterAutoShapeCase1.Placeholder?.Type;
            PlaceholderType? shapePlaceholderTypeCase2 = masterAutoShapeCase2.Placeholder?.Type;
            PlaceholderType? shapePlaceholderTypeCase3 = masterAutoShapeCase3.Placeholder?.Type;

            // Assert
            shapePlaceholderTypeCase1.Should().Be(PlaceholderType.Title);
            shapePlaceholderTypeCase2.Should().BeNull();
            shapePlaceholderTypeCase3.Should().BeNull();
        }

        [Fact]
        public void ShapeGeometryType_ReturnsShapesGeometryFormType()
        {
            // Arrange
            ISlideMaster slideMaster = _fixture.Pre001.SlideMasters[0];
            IShape shapeCase1 = slideMaster.Shapes.First(sp => sp.Id == 2);
            IShape shapeCase2 = slideMaster.Shapes.First(sp => sp.Id == 8);

            // Act
            GeometryType geometryTypeCase1 = shapeCase1.GeometryType;
            GeometryType geometryTypeCase2 = shapeCase2.GeometryType;

            // Assert
            geometryTypeCase1.Should().Be(GeometryType.Rectangle);
            geometryTypeCase2.Should().Be(GeometryType.Custom);
        }

        [Fact]
        public void AutoShapeTextBoxText_ReturnsText_WhenTheSlideMasterAutoShapesTextBoxIsNotEmpty()
        {
            // Arrange
            ISlideMaster slideMaster = _fixture.Pre001.SlideMasters[0];
            IAutoShape autoShape = (IAutoShape)slideMaster.Shapes.First(sp => sp.Id == 8);

            // Act-Assert
            autoShape.TextBox.Text.Should().BeEquivalentTo("id8");
        }

        [Fact]
        public void AutoShape_TextBox_Paragraph_Portion_FontSize_returns_Font_Size()
        {
            // Arrange
            var slideMaster = _fixture.Pre001.SlideMasters[0];
            var autoShape = (IAutoShape)slideMaster.Shapes.First(sp => sp.Id == 8);

            // Act
            int portionFontSize = autoShape.TextBox.Paragraphs[0].Portions[0].Font.Size;

            // Assert
            portionFontSize.Should().Be(18);
        }
    }
}
