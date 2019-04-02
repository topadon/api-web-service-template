using System;
using System.Net;
using System.Net.Sockets;

namespace API.Gateway.App_Start
{
    public class AppConfig
    {
        public static string LocalIP = GetLocalIPAddress();
        public static int RequestTimeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RequestTimeout"]);

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}