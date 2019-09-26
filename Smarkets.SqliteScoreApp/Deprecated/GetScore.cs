using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Smarkets.SqliteScoreApp
{

        public class ScoreScraper
        {

            static string eventurl = "https://smarkets.com/event";

            static string loginurl = "https://smarkets.com/login";


            public ScoreScraper()
            {

                web = new HtmlWeb();

            }

            HtmlWeb web;

            public int[] ScrapeResultFromPage(long id, string urlev)
            {
                string url = $@"{eventurl}/{id}{urlev}";

            var httpClient = new HttpClient();
            var x = GetResponse(url, httpClient);
            x.Wait();
            var xx = x.Result;

            var doc = web.Load(url);
                if (web.StatusCode != HttpStatusCode.OK)
                    return null;



            string s = "div.scores.block.no-sets > div.score-pair > div.score.numeric-value";
                return doc.DocumentNode
                    .QuerySelectorAll(s)?
                    .Select(sc => int.Parse(sc.InnerText.Trim()))
                    .ToArray();

            //      System.Threading.Thread.Sleep(1000);
        }

        private static async System.Threading.Tasks.Task<string> GetResponse(string url, HttpClient client)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                //request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                //request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                //request.Headers.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

                using (var response = await client.SendAsync(request).ConfigureAwait(false))
                {
                    //response.EnsureSuccessStatusCode();
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var decompressedStream = new System.IO.Compression.GZipStream(responseStream, System.IO.Compression.CompressionMode.Decompress))
                    using (var streamReader = new StreamReader(decompressedStream))
                    {
                        return await streamReader.ReadToEndAsync().ConfigureAwait(false);
                    }
                }
            }
        }


        //public async Task LoginAsync()
        //{


        //    HttpClient hc = new HttpClient();

        //    HttpResponseMessage resultLogin = await hc.PostAsync(loginurl, new StringContent("login=myUserName&password=myPaswordValue", Encoding.UTF8, "application/x-www-form-urlencoded"));

        //    //HttpResponseMessage resultPlaylist = await hc.GetAsync(urlData);

        //    //Stream stream = await resultPlaylist.Content.ReadAsStreamAsync();

        //    //HtmlDocument doc = new HtmlDocument();

        //    //doc.Load(stream);

        //    //string webContent = doc.DocumentNode.InnerHtml;  => it works



        //    CookieContainer cookieJar = new CookieContainer();
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.google.com");
        //    request.CookieContainer = cookieJar;

        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    int cookieCount = cookieJar.Count;

        //}


    }
}
