using Raydreams.Skia.Logic;
using SkiaSharp;

namespace Raydreams.Draw.Tests;

[TestClass]
public sealed class TextProcessorTests
{
    [TestMethod]
    public void MeasureTextTest()
    {
        using SKPaint paint = new()
        {
            Color = SKColors.Black,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            StrokeWidth = 0
        };

        SKTypeface style = SKTypeface.FromFamilyName("Arial", SKFontStyle.Normal );
        SKFont font = new SKFont(style, 12);

        SkiaTextProcessor logic = new(500, 500, font, paint);
        var results = logic.Process("The quick brown fox jumped over the lazy dog.");

        Assert.IsGreaterThan<int>(0, logic.Words.Count);
    }
}
