using FilesShare.Contracts.Services;
using System;
using System.Linq;
using System.Net;

namespace FilesShare.Logics.ServiceManager
{

    public class PingService : IPingService
    {
        public void Ping(int port, string peerUri)
        {
            var host = Dns.GetHostEntry(peerUri);
            Console.WriteLine("New Peer Entered Peer Endpoint Details");
            host.AddressList?.ToList().ForEach(p => Console.WriteLine($"\t\t\t EndPoint : {p}{port}"));

            //Console.WriteLine($"Yay ! From {peerUri}");
        }
    }
}
