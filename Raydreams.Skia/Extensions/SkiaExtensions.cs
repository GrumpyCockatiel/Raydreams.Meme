using System.Drawing;
using SkiaSharp;

namespace Raydreams.SKGraphs.Extensions
{
    /// <summary>Extension to convert SK structs to System.Drawing and vice versa</summary>
    public static partial class RaySkiaExtensions
    {
        /// <summary>Distance from one point to another</summary>
        /// <param name="point"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static float Distance( this SKPoint point, SKPoint other )
        {
            float a = point.X - other.X;
            float b = point.Y - other.Y;
            return Convert.ToSingle( Math.Sqrt( a * a + b * b ) );
        }

        /// <summary>Get the midpoint of two points</summary>
        public static SKPoint Midpoint( this SKPoint start, SKPoint end ) => new SKPoint( start.X + ( end.X - start.X ) / 2.0F, start.Y + ( end.Y - start.Y ) / 2.0F );

        /// <summary>Get a 1/4th of the distance between 2 points</summary>
        public static SKPoint Quarterpoint( this SKPoint start, SKPoint end ) => new SKPoint( start.X + ( end.X - start.X ) / 4.0F, start.Y + ( end.Y - start.Y ) / 4.0F );

        /// <summary>Convert an SKPoint to an MS Draw Point</summary>
        public static PointF ToMSDraw( this SKPoint pt ) => new PointF( pt.X, pt.Y );

        /// <summary>Float Point to SK</summary>
        public static SKPoint ToSK( this PointF pt ) => new SKPoint( pt.X, pt.Y );

        /// <summary>Int Point to SKPoint</summary>
        public static SKPoint ToSK(this Point pt) => new SKPoint(pt.X, pt.Y);

        /// <summary>Float Points to SK</summary>
        public static List<SKPoint> ToSK( this IEnumerable<PointF> pts ) => pts.Select( pt => pt.ToSK() ).ToList();

        /// <summary></summary>
        public static SKRect ToSK( this RectangleF rect ) => new SKRect( rect.Left, rect.Top, rect.Right, rect.Bottom );

        /// <summary>Make an SKRect using width and height</summary>
        /// <remarks>left, top, right, bottom</remarks>
        public static SKRect MakeSKRect( float left, float top, float width, float height ) => new SKRect( left, top, left + width, top + height);

        /// <summary>Make an SKRect using width and height</summary>
        /// <remarks>left, top, right, bottom</remarks>
        public static SKRect MakeSKRect( SKPoint topLeft, float width, float height ) => new SKRect( topLeft.X, topLeft.Y, topLeft.X + width, topLeft.Y + height);

        /// <summary>Make an SKRect using a top left and bottom right SKPoint</summary>
        /// <remarks>left, top, right, bottom</remarks>
        public static SKRect MakeSKRect( SKPoint topLeft, SKPoint bottomLeft ) => new SKRect( topLeft.X, topLeft.Y, bottomLeft.X, bottomLeft.Y );

        /// <summary>Translate a point</summary>
        /// <param name="offset"></param>
        public static SKPoint Translate(this SKPoint pt, SKPoint offset) => new SKPoint(pt.X + offset.X, pt.Y + offset.Y);

        /// <summary>Translate a point</summary>
        public static SKPoint Translate(this SKPoint pt, float xOffset, float yOffset) => new SKPoint(pt.X + xOffset, pt.Y + yOffset);

        /// <summary>Translate a rect</summary>
        public static SKRect Translate(this SKRect rect, float xOffset, float yOffset) 
            => new SKRect(rect.Left + xOffset, rect.Top + yOffset, rect.Right + xOffset, rect.Bottom + yOffset);

        /// <summary>Make an SKRect centered at midX with width and height</summary>
        public static SKRect TopCenteredSKRect(this SKPoint midTop, float width, float height)
        {
            float w = width / 2F;
            float h = height / 2F;

            return new SKRect(midTop.X - w, midTop.Y, midTop.X + w, midTop.Y + height);
        }

        /// <summary>Make an SKRect with origin at the center</summary>
        public static SKRect CenteredSKRect( this SKPoint origin, float width, float height )
        {
            float w = width / 2F;
            float h = height / 2F;

            return new SKRect( origin.X - w, origin.Y - h, origin.X + w, origin.Y + h );
        }

        /// <summary>Test to see if a point is inside the specified rect</summary>
        public static bool IsInBounds( this SKPoint pt, SKPoint min, SKPoint max )
        {
            if ( pt.X < min.X || pt.Y < min.Y )
                return false;

            if ( pt.X > max.X || pt.Y > max.Y )
                return false;

            return true;
        }

        /// <summary>Just makes a closed polygon for a list of points</summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        public static SKPath MakePolygon( SKPoint[] vertices, SKPathFillType fillType = SKPathFillType.EvenOdd )
        {
            SKPath path = new SKPath { FillType = fillType };

            if ( vertices == null || vertices.Length < 1 )
                return path;

            // create the path
            path.MoveTo( vertices[0] );

            for ( int i = 1; i < vertices.Length; ++i )
                path.LineTo( vertices[i] );

            path.Close();
            return path;
        }

        /// <summary>Calculate the square bounding a circle</summary>
        public static SKRect BoundingSquare( this SKPoint origin, float rad ) => ( rad <= 0 ) ? new SKRect()
            : new SKRect( origin.X - rad, origin.Y - rad, origin.X + rad, origin.Y + rad );

        /// <summary>Calculate the square inscribed inside a circle</summary>
        public static SKRect InscribedSquare( this SKPoint origin, float radius )
        {
            if ( radius <= 0 )
                return new SKRect();

            float h = Convert.ToSingle( radius * Math.Sin( Math.PI / 4.0 ) );

            return new SKRect( origin.X - h, origin.Y - h, origin.X + h, origin.Y + h );
        }

        /// <summary>Is this a fully translucent color where the alpha value equals 0</summary>
        public static bool IsTranslucent( this SKColor color ) => ( color.Alpha == 0x00 );

        /// <summary>Converts an SKColor structure to an hex RGB representation</summary>
        /// <param name="alpha">Include alpha value on the end as RGBA</param>
        public static string ToHexString( this SKColor color, bool alpha = false )
        {
            if ( alpha )
                return $"{color.Red:x2}{color.Green:x2}{color.Blue:x2}{color.Alpha:x2}";

            return $"{color.Red:x2}{color.Green:x2}{color.Blue:x2}";
        }

        /// <summary>Converts SKColor to the string format rgb(R,G,B) or rgba(R,G,B,A)</summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToRGBString( this SKColor color, bool alwaysRGBA = false )
        {
            if (color.Alpha < 255 || alwaysRGBA)
            {
                double alpha = ( color.Alpha / 255.0 );
                return $"rgba({color.Red},{color.Green},{color.Blue},{alpha:f2})";
            }
            else
                return $"rgb({color.Red},{color.Green},{color.Blue})";
        }
    }
}
