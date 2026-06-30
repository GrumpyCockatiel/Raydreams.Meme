using System.Text.RegularExpressions;
using Raydreams.Drawing.Model;
using SkiaSharp;

namespace Raydreams.SKGraphs.Extensions;

public static class SKImageExtensions
{
    /// <summary>Calculates the cropping rect based on the cropping type</summary>
    public static SKRect CalcCropRect( this SKBitmap bmp, CropType cropping )
    {
        SKRect cropRect = new SKRect( 0, 0, bmp.Width, bmp.Height );
        ImageOrientation orient = bmp.GetOrientation();

        if ( cropping == CropType.Original || orient == ImageOrientation.Square )
            return cropRect;

        // 
        if ( cropping == CropType.TopLeftSquare )
        {
            if ( orient == ImageOrientation.Landscape )
                cropRect = new SKRect( 0, 0, bmp.Height, bmp.Height );
            else if ( orient == ImageOrientation.Portrait )
                cropRect = new SKRect( 0, 0, bmp.Width, bmp.Width );
        }
        else if ( cropping == CropType.BottomRightSquare )
        {
            if ( orient == ImageOrientation.Landscape )
                cropRect = new SKRect( bmp.Width - bmp.Height, 0, bmp.Width, bmp.Height );
            else if ( orient == ImageOrientation.Portrait )
                cropRect = new SKRect( 0, bmp.Height - bmp.Width, bmp.Width, bmp.Height );
        }
        else // center out square
        {
            if ( orient == ImageOrientation.Landscape )
            {
                int mid = bmp.Width / 2;
                cropRect = new SKRect( mid - bmp.Height / 2, 0, mid + bmp.Height / 2, bmp.Height );
            }
            else if ( orient == ImageOrientation.Portrait )
            {
                int mid = bmp.Height / 2;
                cropRect = new SKRect( 0, mid - bmp.Width / 2, bmp.Width, mid + bmp.Width / 2 );
            }
        }

        return cropRect;
    }

    /// <summary>Crops an image given a cropping rect</summary>
    /// <param name="bmp"></param>
    /// <returns></returns>
    public static SKImage CropBitmap( this SKBitmap bmp, SKRect cropRect )
    {
        // validate the crop rect
        if ( cropRect.IsEmpty )
            return SKImage.FromBitmap( bmp );

        // destion rect is the same as the new image
        SKRect dest = new SKRect( 0, 0, cropRect.Width, cropRect.Height );

        SKSurface surface = SKSurface.Create( new SKImageInfo( (int)dest.Width, (int)dest.Height, bmp.ColorType, bmp.AlphaType, bmp.ColorSpace ) );

        surface.Canvas.DrawBitmap( bmp, cropRect, dest );

        return surface.Snapshot();
    }

    /// <summary>Resizes the Bitmap to the the specified max side dimension</summary>
    /// <param name="bmp">the bitmap</param>
    /// <param name="maxDimension">the longest side after scaling</param>
    /// <returns></returns>
    public static SKBitmap Resize( this SKBitmap bmp, int maxDimension )
    {
        maxDimension = Math.Clamp(maxDimension, 16, 4000);
        
        ImageOrientation orient = bmp.GetOrientation();

        double width = bmp.Width;
        double height = bmp.Height;

        if ( orient == ImageOrientation.Landscape )
        {
            height = height * ( maxDimension / width );
            width = maxDimension;
        }
        else
        {
            width = width * ( maxDimension / height );
            height = maxDimension;
        }

        SKBitmap dest = new SKBitmap( (int)width, (int)height, bmp.ColorType, bmp.AlphaType, bmp.ColorSpace );

        bmp.ScalePixels( dest, new SKSamplingOptions(SKFilterMode.Linear, SKMipmapMode.Linear) );

        return dest;
    }

    /// <summary>Determines the orientation of the image</summary>
    /// <param name="bmp"></param>
    /// <returns></returns>
    public static ImageOrientation GetOrientation( this SKBitmap bmp )
    {
        if ( bmp.Width < bmp.Height )
            return ImageOrientation.Portrait;

        if ( bmp.Width > bmp.Height )
            return ImageOrientation.Landscape;

        return ImageOrientation.Square;
    }

    /// <summary>Gets the length of the longer side</summary>
    public static int MaxDimension( this SKBitmap bmp ) => bmp.Width > bmp.Height ? bmp.Width : bmp.Height;

    /// <summary>True if either dimensions is greater than the specified size</summary>
    /// <param name="bmp"></param>
    /// <param name="maxDimension"></param>
    /// <returns></returns>
    public static bool GreaterThan( this SKBitmap bmp, int maxDimension )
    {
        if ( maxDimension < 1 )
            maxDimension = 1;

        return bmp.MaxDimension() > maxDimension;
    }

}
