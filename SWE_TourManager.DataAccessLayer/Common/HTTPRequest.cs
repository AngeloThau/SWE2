using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.DataAccessLayer.Common
{
    public class HTTPRequest : IHTTPRequest
    {

        private string url;        
        private string response;

        public HTTPRequest()
        {
            url = ConfigurationManager.AppSettings["DataUrl"].ToString();             
        }
        public string GetJsonResponse(string Start, string Destination)
        {
            Uri requestUri = RequestBuilder(Start, Destination);
            SendRequest(requestUri);
            return response;
        }

        public Uri RequestBuilder(string Start, string Destination)
        {
            string requesturi = url + "&from=" + Start + "&to=" + Destination;

            return new Uri(requesturi);
        }

        public void SendRequest(Uri Request)
        {
            WebRequest request = WebRequest.Create(Request);
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream);
                this.response = reader.ReadToEnd();
            }
            response.Close();

        }
       
    }
}
