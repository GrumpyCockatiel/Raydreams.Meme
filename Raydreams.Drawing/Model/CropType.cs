namespace Raydreams.Drawing.Model;

/// <summary>Standard cropping formats</summary>
public enum CropType
{
    /// <summary>None</summary>
    Original = 0,
    /// <summary>Square center out crop</summary>
    CenterSquare = 1,
    /// <summary>Square crop from the left or top depending orientation</summary>
    TopLeftSquare = 2,
    /// <summary>Square crop from the bottom or right depending orientation</summary>
    BottomRightSquare = 3
}
