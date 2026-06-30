namespace Raydreams.Drawing;

/// <summary>enumerations of all the possible space icons</summary>
public enum FilterType
{
    Identity = 0,
    BW = 1,
    Pastel = 2,
    Sepia = 3
}

/// <summary>A collection of various image filters</summary>
public static class ImageFilters
{
    public static Dictionary<FilterType, float[]> Matrices = new Dictionary<FilterType, float[]>
        {
            { FilterType.BW, new float[]
                {
                    0.21F, 0.72F, 0.07F, 0, 0,
                    0.21F, 0.72F, 0.07F, 0, 0,
                    0.21F, 0.72F, 0.07F, 0, 0,
                    0, 0, 0, 1, 0
                } },
            { FilterType.Identity, new float[]
                {
                    1, 0, 0, 0, 0,
                    0, 1, 0, 0, 0,
                    0, 0, 1, 0, 0,
                    0, 0, 0, 1, 0
                } },
            { FilterType.Pastel, new float[]
                {
                    0.75f, 0.25f, 0.25f, 0, 0,
                    0.25f, 0.75f, 0.25f, 0, 0,
                    0.25f, 0.25f, 0.75f, 0, 0,
                    0, 0, 0, 1, 0
                } },
            { FilterType.Sepia, new float[]
                {
                    0.393F, 0.769F, 0.189F, 0, 0,
                    0.349F, 0.686F, 0.168F, 0, 0,
                    0.272F, 0.534F, 0.131F, 0, 0,
                    0, 0, 0, 1, 0
                } }
        };
}