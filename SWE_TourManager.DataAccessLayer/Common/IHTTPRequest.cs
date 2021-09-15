using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.DataAccessLayer.Common
{
    public interface IHTTPRequest
    {
        public string GetJsonResponse(string Start, string Destination);
        public Uri RequestBuilder(string Start, string Destination);
        public void SendRequest(Uri Request);
    }
}
