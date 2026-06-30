using Raydreams.Drawing.Extensions;
using Raydreams.Drawing.Model;
using Raydreams.SKGraphs.Extensions;
using SkiaSharp;

namespace Raydreams.Skia.Logic;

/// <summary></summary>
public record WordInfo
{
    public string Word = String.Empty;

    public SKRect Position;

    //public int LineIndex;
}

/// <summary>
/// </summary>
public class SkiaTextProcessor
{
    public SkiaTextProcessor(float width, float height, SKFont font, SKPaint paint)
    {
        this.Width = width;
        this.Height = height;
        this.Font = font;
        this.Paint = paint;
        _ = font.GetFontMetrics(out SKFontMetrics metrics);
        this.Metrics = metrics;
    }

    #region [ Properties ]

    /// <summary>Width to write inside of</summary>
    public float Width {get; init;}

    /// <summary>Height only observed for an offset in vertical alignment</summary>
    public float Height {get; init;}

    public SKFont Font {get; init;}

    public SKPaint Paint {get; init;}

    public List<WordInfo> Words {get; protected set;} = [];

    public SKFontMetrics Metrics {get; protected set;}

    /// <summary>The Horizontal Text Alignment which is usually centered.</summary>
    public HorizontalTextAlign HTextAlign {get; set;} = HorizontalTextAlign.Center;

    /// <summary>The Vertical Text Alignment</summary>
    public VerticalTextAlign VTextAlign {get; set;} = VerticalTextAlign.Bottom;

    /// <summary>The full height of a line from descent to ascent</summary>
    public float LineHeight => this.Metrics.Descent - this.Metrics.Ascent;

    /// <summary>The space between lines to use</summary>
    public float Leading => this.Metrics.Leading * 2F;

    #endregion [ Properties ]

    public bool Process(string text)
    {
        var lines = this.ParseWords(text);
        this.Align(lines);
        lines.ForEach( l => this.Words.AddRange(l) );
        return this.Words.Count > 0;
    }

    /// <summary>Parses text by each letter so that you can force break a line</summary>
    /// <param name="text"></param>
    public List<List<WordInfo>> ParseWords(string text)
    {
        // get the size of a space
        float spacing = this.Font.MeasureText(" ", this.Paint );

        // get the naturally occuring line breaks
        List<string> lines = text.Split( ["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries ).ToList();

        // the output list of line - the length of the is what Line # we are on
        List<List<WordInfo>> stack = [];

        float y0 = 0;
        lines.ForEach( line =>
        {
            var lastLines = this.ParseLine(line, y0, spacing);
            stack.AddRange(lastLines);
            y0 = stack.Last().First().Position.Bottom + this.Leading;
        });

        return stack;
    }

    /// <summary></summary>
    /// <param name="text"></param>
    /// <param name="y0"></param>
    /// <param name="spacing"></param>
    /// <returns></returns>
    protected List<List<WordInfo>> ParseLine(string text, float y0, float spacing)
    {
        if (spacing < 0)
            spacing = this.Font.MeasureText(" ", this.Paint );

        // split on space and tabs for now
        string[] words = text.Split([' ','\t','\u00A0'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToArray();

        List<List<WordInfo>> stack = [];
        List<WordInfo> current = [];

        for (int i = 0; i < words.Length; ++i)
        {
            // init a rect around the word
            var textWidth = this.Font.MeasureText(words[i], out SKRect origin, this.Paint );
            origin = RaySkiaExtensions.MakeSKRect(0, y0, textWidth, this.LineHeight);

            // offset the rect based on the world position
            var x0 = i < 1 ? 0 : current.Last().Position.Right + spacing;
            origin = origin.Translate(x0, 0);

            // check for overflow
            if (origin.Right > this.Width)
            {
                stack.Add(current);
                current = [];
                x0 = 0;
            }

            //float yd = this.LineHeight * (stack.Count + 1) + (this.Metrics.Leading * 2F * stack.Count);
            float yd = (this.LineHeight + this.Leading) * stack.Count;
            current.Add( new WordInfo {
                    Word = words[i],
                    Position = RaySkiaExtensions.MakeSKRect(x0, y0 + yd, origin.Width, origin.Height ),
                }
            );

        }

        stack.Add(current);

        return stack;
    }
    

    /// <summary></summary>
    /// <param name="lines"></param>
    public void Align(List<List<WordInfo>> lines)
    {
        if ( this.HTextAlign == HorizontalTextAlign.Center || this.HTextAlign == HorizontalTextAlign.Right )
        {
            foreach (var line in lines)
            {
                if (line.Last().Position.Right > this.Width)
                    continue;

                // get the right of the last word
                float adjust = this.Width - line.Last().Position.Right;

                if ( this.HTextAlign == HorizontalTextAlign.Center )
                    adjust /= 2F;

                line.ForEach( word => word.Position = word.Position.Translate(adjust, 0) );
            }
        }
        
        if ( this.VTextAlign == VerticalTextAlign.Center || this.VTextAlign == VerticalTextAlign.Bottom )
        {
            var bottom = lines.Last().First().Position.Bottom;

            if (bottom >= this.Height )
                return;

            float adjust = this.Height - bottom;

            if ( this.VTextAlign == VerticalTextAlign.Center )
                adjust /= 2F;

            foreach (var line in lines)
                line.ForEach( word => word.Position = word.Position.Translate(0, adjust) );
        }
    }

    /// <summary></summary>
    /// <param name="text"></param>
    /// <returns>A list of lines</returns>
    public List<List<WordInfo>> ParseWordsOld(string text)
    {
        // get the size of a space
        float spacing = this.Font.MeasureText(" ", this.Paint );

        // split on space and tabs for now
        string[] words = text.Split([' ','\t','\u00A0'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToArray();

        List<List<WordInfo>> stack = [];
        List<WordInfo> current = [];

        // iterate each word and measure
        for (int i = 0; i < words.Length; ++i)
        {
            var textWidth = this.Font.MeasureText(words[i], out SKRect origin, this.Paint );
            origin = RaySkiaExtensions.MakeSKRect(0, 0, textWidth, this.LineHeight);

            // first word only
            if ( i < 1 )
            {
                current.Add( new WordInfo { Word = words[i], Position = origin });
                continue;
            }

            // move it right of the last word
            origin = origin.Translate(current.Last().Position.Right + spacing, 0);

            // if it overflows make a new line
            float x = 0;
            if (origin.Right > this.Width + 0.5F)
            {
                stack.Add(current);
                current = [];
            }
            else
                x = current.Last().Position.Right + spacing;
 
            int line = stack.Count;
            current.Add( new WordInfo {
                    Word = words[i],
                    Position = RaySkiaExtensions.MakeSKRect(x, (this.LineHeight + this.Metrics.Leading * 2F) * line, origin.Width, origin.Height ),
                    //LineIndex = line
                }
            );
        }

        stack.Add(current);
        return stack;
    }
}
