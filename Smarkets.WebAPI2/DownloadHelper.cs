using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Smarkets.WebAPI
{
    public static class DownloadHelper
    {

        public static async Task<T> DownloadAsync<T>(string url) where T : new()
        {
            using (var w = new HttpClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data =await w.GetResponse(url);
                }
                catch (Exception e) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }



        public static async System.Threading.Tasks.Task<string> GetResponse(this HttpClient client, string url, params Tuple<string, string>[] headers)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                foreach(var _  in headers) request.Headers.TryAddWithoutValidation(_.Item1, _.Item2);
                //request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                //request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                //request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                //request.Headers.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                using (var response = await client.SendAsync(request).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var decompressedStream = new System.IO.Compression.GZipStream(responseStream, System.IO.Compression.CompressionMode.Decompress))
                    using (var streamReader = new StreamReader(decompressedStream))
                    {
                        return await streamReader.ReadToEndAsync().ConfigureAwait(false);
                    }
                }
            }
        }
    }
}
