using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTrans
{
    using System;
    using System.Net;
    using System.Web.Script.Serialization; // requires the reference 'System.Web.Extensions'
    using System.IO;
    using System.Text;


    public class Translator
    {
        // When you have your own cliend ID and secret, specify them here:
        private static string CLIENT_ID = "th_kk@abv.bg";
        private static string CLIENT_SECRET = "c2a52fae305348968d56d3f682d95cab";

        private static string API_URL = "http://api.whatsmate.net/v1/translation/translate";

        public string translate(string fromLang, string toLang, string text)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    client.Headers["X-WM-CLIENT-ID"] = CLIENT_ID;
                    client.Headers["X-WM-CLIENT-SECRET"] = CLIENT_SECRET;
                    client.Encoding = Encoding.UTF8;

                    Payload payloadObj = new Payload() { fromLang = fromLang, toLang = toLang, text = text };
                    string postData = (new JavaScriptSerializer()).Serialize(payloadObj);

                    string response = client.UploadString(API_URL, postData);
                    return response;
                }
            }
            catch (WebException webEx)
            {
                Console.WriteLine(((HttpWebResponse)webEx.Response).StatusCode);
                Stream stream = ((HttpWebResponse)webEx.Response).GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                String body = reader.ReadToEnd();
                Console.WriteLine(body);
                return null;
            }           
        }

        public class Payload
        {
            public string fromLang { get; set; }
            public string toLang { get; set; }
            public string text { get; set; }
        }

    }
}
