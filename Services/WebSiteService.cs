using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Microsoft.Extensions.Options;
using WorkingMonintor.Models;

namespace WorkingMonintor.Services {
    public class WebSiteService {
        public Setting_WebSite _Setting_WebSite { get; }

        public WebSiteService (IOptions<Setting_WebSite> Iopts) {
            _Setting_WebSite = Iopts.Value;
        }

        /// <summary>
        ///     取得網頁狀態
        /// </summary>
        /// <returns>
        ///     [{"url":"","statusCode":"","msg":""}]
        /// </returns>
        public List<WebSite> GetWebSiteStatus () {
            //回傳值
            List<WebSite> reWebSiteLists = new List<WebSite> ();

            foreach (var WebSite in _Setting_WebSite.WebSiteSettings) {
                WebSite reWebSite = new WebSite ();

                //測量已耗用時間
                Stopwatch SW = new Stopwatch ();
                SW.Start ();

                /*參考：https://docs.microsoft.com/zh-tw/dotnet/api/system.net.httpwebresponse.statuscode?view=netframework-4.8*/
                try {
                    // Creates an HttpWebRequest for the specified URL. 
                    HttpWebRequest myHttpWebRequest = (HttpWebRequest) WebRequest.Create (WebSite.URL);
                    //myHttpWebRequest.Timeout = 3000; //要求超時前等待的毫秒數。預設值為100000毫秒（100秒）

                    // Sends the HttpWebRequest and waits for a response.
                    HttpWebResponse myHttpWebResponse = (HttpWebResponse) myHttpWebRequest.GetResponse ();

                    if (myHttpWebResponse.StatusCode == HttpStatusCode.OK) {
                        SW.Stop ();
                        // Get the elapsed time as a TimeSpan value.
                        TimeSpan TS = SW.Elapsed;

                        reWebSite.WebSiteName = WebSite.WebSite;
                        reWebSite.URL = WebSite.URL;
                        reWebSite.StatusCode = myHttpWebResponse.StatusCode.ToString ();
                        reWebSite.TimeSpan_Second = TS.TotalSeconds.ToString ();
                        reWebSite.Msg = myHttpWebResponse.StatusDescription;
                        Console.WriteLine ("\r\n{0} : Response Status Code is OK and StatusDescription is: {1}", WebSite.WebSite, myHttpWebResponse.StatusDescription);
                    }

                    // Releases the resources of the response.
                    myHttpWebResponse.Close ();
                } catch (WebException e) {
                    SW.Stop ();
                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan TS = SW.Elapsed;

                    reWebSite.WebSiteName = WebSite.WebSite;
                    reWebSite.URL = WebSite.URL;
                    reWebSite.StatusCode = e.Status.ToString ();
                    reWebSite.TimeSpan_Second = TS.TotalSeconds.ToString ();
                    reWebSite.Msg = e.Message;
                    Console.WriteLine ("\r\n{0} : WebException Raised. The following error occurred : {1}", WebSite.WebSite, e.Status);
                } catch (Exception e) {
                    SW.Stop ();
                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan TS = SW.Elapsed;

                    reWebSite.WebSiteName = WebSite.WebSite;
                    reWebSite.URL = WebSite.URL;
                    reWebSite.StatusCode = "Exception";
                    reWebSite.TimeSpan_Second = TS.TotalSeconds.ToString ();
                    reWebSite.Msg = e.Message;
                    Console.WriteLine ("\n{0} : The following Exception was raised : {1}", WebSite.WebSite, e.Message);
                }

                reWebSiteLists.Add (reWebSite);
            }

            return reWebSiteLists;
        }
    }
}