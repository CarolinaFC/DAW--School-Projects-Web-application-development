using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace culinariaMVC
{
    public class GlobalVariables
    {
        public static HttpClient client = new HttpClient();

        GlobalVariables()
        {
            //Hosted web API REST Service base url  
            string Baseurl = "https://localhost:44315/";

            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            // Define request data format
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }
    }
}
