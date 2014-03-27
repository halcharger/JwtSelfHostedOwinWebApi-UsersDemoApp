using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace service
{
    public class Program
    {
        static void Main()
        {
            string baseAddress = "http://localhost:9001/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                var client = new HttpClient();

                var response = client.GetAsync(baseAddress + "api/values").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            Console.ReadLine();
        }          
    }
}