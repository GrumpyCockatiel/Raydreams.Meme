using System.Net;

namespace Raydreams.Meme;

/// <summary>Fetches a web page</summary>
public static class HttpFetch
{
    /// <summary>The user agent name to use in the requests</summary>
    /// <remarks>"RaydreamsClient/1.0.0"</remarks>
    public static string UserAgent { get; set; } = "Mozilla/5.0 (iPad; U; CPU OS 3_2_1 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Mobile/7B405";

    public static List<string> Allowed = new List<string> { @"image/png", @"image/jpeg", @"image/jpg", @"image/gif" };

    /// <summary></summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async Task<string> Fetch( string url )
    {
        if ( String.IsNullOrWhiteSpace( url ) )
            return String.Empty;

        string page = String.Empty;

        try
        {
            Uri address = new Uri( url );

            if ( !address.IsAbsoluteUri )
                return String.Empty;

            using HttpClient client = new HttpClient();

            HttpRequestMessage message = new HttpRequestMessage( HttpMethod.Get, address.AbsoluteUri );
            message.Headers.Clear();
            message.Headers.Add( "User-Agent", UserAgent.Trim() );

            HttpResponseMessage response = await client.SendAsync( message );
            page = await response.Content.ReadAsStringAsync();
        }
        catch
        {
            return String.Empty;
        }

        return page;
    }

    /// <summary>Fetches a bitmap from the specificed URL</summary>
    /// <param name="url">URL of image to grab</param>
    /// <param name="byteMax">Max size of image to return. < 1 will return any size.</param>
    /// <returns></returns>
    public static async Task<byte[]> FetchImageFromURL( string url, long byteMax = 0 )
    {
        if ( String.IsNullOrWhiteSpace(url) )
            return [];

        Uri address = new Uri( url );

        if ( !address.IsAbsoluteUri )
            return [];

        using HttpClient client = new HttpClient();
        HttpRequestMessage message = new HttpRequestMessage( HttpMethod.Get, address.AbsoluteUri );
        message.Headers.Clear();
        message.Headers.Add( "User-Agent", UserAgent.Trim() );
        message.Headers.Add( "Access-Control-Allow-Origin", "*" );
        message.Headers.Add( "Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept" );

        HttpResponseMessage response = await client.SendAsync( message );
        string? type = response.Content.Headers.ContentType?.MediaType?.ToLowerInvariant();

        if ( response.StatusCode != HttpStatusCode.OK || String.IsNullOrWhiteSpace(type) || !Allowed.Contains( type ) )
            return [];

        byte[] data = await response.Content.ReadAsByteArrayAsync();

        return byteMax < 1 ? data : ( data.Length < byteMax + 1 ) ? data : [];
    }
}
