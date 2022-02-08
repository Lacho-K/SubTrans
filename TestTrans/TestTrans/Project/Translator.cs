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
        private static string CLIENT_ID = "kolev_art@abv.bg";
        private static string CLIENT_SECRET = "c0e9906b63f84d7aa0b29753b3b36bf1";

        private static string API_URL = "http://api.whatsmate.net/v1/translation/translate";

        public string translate(string fromLang, string toLang, string text)
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

        public class Payload
        {
            public string fromLang { get; set; }
            public string toLang { get; set; }
            public string text { get; set; }
        }

    }
}
