using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlackboardDownloader
{
    // A modified WebClient class that keeps track of cookies (using CookieContainer)
    // Based on code from: http://stackoverflow.com/questions/2798610/login-to-website-and-use-cookie-to-get-source-for-another-page
    public class WebClientEx : WebClient
    {
        private Uri responseURL; // Keep track of final URL for redirects

        public WebClientEx(CookieContainer container)
        {
            this.container = container;
        }

        // The final response URL for a request, after all redirects    
        public Uri ResponseURL
        {
            get { return responseURL; }
        }
        private readonly CookieContainer container = new CookieContainer();

        // Overrides WebClient's GetWebRequest to add cookie container to the request
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest r = base.GetWebRequest(address);
            var request = r as HttpWebRequest;
            if (request != null)
            {
                request.CookieContainer = container;
            }
            return r;
        }

        // Overrides WebClien'ts GetWebResponse to add response cookies to the cookie container
        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            WebResponse response = base.GetWebResponse(request, result);
            ReadCookies(response);
            responseURL = response.ResponseUri;
            return response;
        }

        // Overrides WebClien'ts GetWebResponse to add response cookies to the cookie container
        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            ReadCookies(response);
            return response;
        }

        // Reads cookies from a WebResponse and adds them to the WebClientEx's cookie container
        private void ReadCookies(WebResponse r)
        {
            var response = r as HttpWebResponse;
            if (response != null)
            {
                CookieCollection cookies = response.Cookies;
                container.Add(cookies);
            }
        }
    }
}
