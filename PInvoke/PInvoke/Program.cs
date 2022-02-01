using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new List<string>();

            Parallel.Invoke(
                () => data.Add(new WebClient().DownloadString("http://www.linqpad.net")),
                () => data.Add(new WebClient().DownloadString("http://www.jaoo.dk")),
                () => data.Add(new WebClient().DownloadString("http://www.albahari.com")));

            foreach(var item in data)
            {
                Console.WriteLine(item);
            }
        }
    }
}
