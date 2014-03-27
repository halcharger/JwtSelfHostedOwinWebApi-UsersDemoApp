using System;
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
                Console.WriteLine("service running and listening on: " + baseAddress);
                Console.WriteLine("press any key to exit... ");

                Console.ReadLine();
            }
        }          
    }
}