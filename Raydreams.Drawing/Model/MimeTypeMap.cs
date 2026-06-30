namespace Raydreams.Drawing.Model;

/// <summary>What type of file is this</summary>
public enum FileCategory
{
    /// <summary></summary>
    Unknown = -1,
    /// <summary></summary>
    Data = 0,
    /// <summary></summary>
    Document = 1,
    /// <summary></summary>
    Image = 2,
    /// <summary></summary>
    Archive = 3,
    /// <summary></summary>
    Code = 10
}

/// <summary></summary>
public record FileType
{
    public FileCategory Category { get; set; } = FileCategory.Data;

    public string[] Extensions { get; set; } = [];

    public string ContentType { get; set; } = "text/plain";

    public bool IsText { get; set; }
}

/// <summary>Maps extensions to Mime Types</summary>
public static class MimeTypeMap
{
    #region [ Fields ]

    /// <summary>the default mime type to use if no matches</summary>
    public static string DefaultMIMEType = "text/plain";

    /// <summary>The dictionary of mime types</summary>
    private static readonly Lazy<IList<FileType>> _mappings = new Lazy<IList<FileType>>(BuildMappings);

    /// <summary>Alter the default MIME type</summary>
    /// <param name="defMime"></param>
    public static void SetDefault(string defMime)
    {
        DefaultMIMEType = !String.IsNullOrWhiteSpace(defMime) ? defMime : "text/plain";
    }

    #endregion [ Fields ]

    /// <summary>Get by the file extension where the dot prefix is optional</summary>
    private static FileType? Get(string ext)
    {
        // validate the input
        if (String.IsNullOrWhiteSpace(ext))
            return null;

        // always use a . prefix
        if (!ext.StartsWith("."))
            ext = $".{ext}".ToLowerInvariant();

        return _mappings.Value.FirstOrDefault(t => t.Extensions.Contains(ext));
    }

    /// <summary>Test the mime type is of type text</summary>
    /// <param name="mimeType"></param>
    /// <returns></returns>
    public static bool IsText(string mimeType)
    {
        var mime = _mappings.Value.FirstOrDefault(t => t.ContentType.Equals(mimeType, StringComparison.InvariantCultureIgnoreCase));

        if (mime == null)
            return false;

        return mime.IsText;
    }
    
    /// <summary>Simple test the file type is supported</summary>
    /// <param name="ext">The file extension optionally prefixed with a .</param>
    /// <returns>True if supposrted</returns>
    public static bool Supported(string ext) => Get(ext) != null;

    /// <summary>Get the category of this file type</summary>
    public static FileCategory Category(string ext)
    {
        var mime = Get(ext);
        return mime?.Category ?? FileCategory.Unknown;
    }

    /// <summary>Gets the actual MIME Type based on the file extension</summary>
    /// <param name="extension">file extension optionally prefixed with a .</param>
    /// <returns>The mime type or the default mime type if not found</returns>
    public static string GetMimeType(string ext)
    {
        var mime = Get(ext);
        return mime?.ContentType ?? DefaultMIMEType;
    }

    /// <summary>Does a reverse lookup by mime type for the associated extension</summary>
    public static string GetExtension(string mimeType)
    {
        if (String.IsNullOrWhiteSpace(mimeType))
            throw new System.ArgumentNullException(nameof(mimeType), "No MIME type passed.");

        if (!mimeType.Contains('/'))
            throw new System.ArgumentException($"MIME type is not valid: {mimeType}");

        var mime = _mappings.Value.FirstOrDefault(t => t.ContentType.Equals(mimeType, StringComparison.InvariantCultureIgnoreCase));

        if (mime == null)
            throw new System.ArgumentException($"Requested mime type was not found: {mimeType}");
            
        return mime.Extensions.First();
    }

    /// <summary>Build the supported extensions</summary>
    /// <returns>Lazy loaded list of extensions</returns>
    /// <remarks>Added extensions to support to this list</remarks>
    private static IList<FileType> BuildMappings()
    {
        // dictionary built to lookup using ignore case
        // comment out any types you don't want to support
        return new List<FileType>()
        {
            new FileType {Category = FileCategory.Image, Extensions = [".bmp"], ContentType = "image/bmp", IsText = false},
            new FileType {Category = FileCategory.Code, Extensions = [".css"], ContentType = "text/css", IsText = true},
            new FileType {Category = FileCategory.Data, Extensions = [".csv"], ContentType = "text/csv", IsText = true },
            new FileType {Category = FileCategory.Image, Extensions = [".gif"], ContentType = "image/gif", IsText = false },
            new FileType {Category = FileCategory.Document, Extensions = [".html", ".htm"], ContentType = "text/html", IsText = true},
            new FileType {Category = FileCategory.Image, Extensions = [".ico"], ContentType = "image/x-icon", IsText = false},
            new FileType {Category = FileCategory.Image, Extensions = [".jpg", ".jpeg"], ContentType = "image/jpg", IsText = false },
            new FileType {Category = FileCategory.Code, Extensions = [".js"], ContentType ="text/javascript", IsText = true },
            new FileType {Category = FileCategory.Data, Extensions = [".json"], ContentType = "application/json", IsText = true},
            new FileType {Category = FileCategory.Document, Extensions = [".md", ".markdown"], ContentType = "text/html", IsText = true},
            new FileType {Category = FileCategory.Document, Extensions = [".pdf"], ContentType = "application/pdf", IsText = false },
            new FileType {Category = FileCategory.Image, Extensions = [".png"], ContentType = "image/png", IsText = false },
            new FileType {Category = FileCategory.Document, Extensions = [".rayx"], ContentType = "application/octet-stream", IsText = false },
            new FileType {Category = FileCategory.Image, Extensions = [".svg"], ContentType = "image/svg+xml", IsText = false },
            new FileType {Category = FileCategory.Image, Extensions = [".tif", ".tiff"], ContentType = "image/tiff", IsText = false },
            new FileType {Category = FileCategory.Document, Extensions = [".txt"], ContentType = "text/plain", IsText = true },
            new FileType {Category = FileCategory.Data, Extensions = [".xml"], ContentType = "application/xml", IsText = true },
            new FileType {Category = FileCategory.Archive, Extensions = [".zip"], ContentType = "application/zip", IsText = false }
        };
    }
}
