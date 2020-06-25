using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilesShare.Logics.PnrpManager;
using FilesShare.Logics.ServiceManager;
using FilesShare.Domain.Models;
using System.Diagnostics;
using FilesShare.Contracts.Services;
using FilesShare.Contracts.Repository;
using System.Net;

namespace FilesShare.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Count() <= 2)
            {
                Process.Start("FilesShare.Test.exe");
            }

            new Program().Run();

        }

        private void Run()
        {
            Peer<IPingService> peer = new Peer<IPingService>
            {
                UserName = Guid.NewGuid().ToString().Split('-')[4]
            };
            IPeerRegistrationRepository peerRegistration = new PeerRegistrationManager();
            IPeerNameResolverRepository peerNameResolverRepository = new PeerNameResolver(peer.UserName);
            IPeerConfigurationService peerConfigurationService = new PeerConfigurationService(peer);

            peerRegistration.StartPeerRegistration(peer.UserName, peerConfigurationService.Port);

            Console.WriteLine("Peer Information");
            Console.WriteLine($"Peer Uri : {peerRegistration.PeerUri}   Port : {peerConfigurationService.Port}");

            var host = Dns.GetHostEntry(peerRegistration.PeerUri);
            host.AddressList?.ToList().ForEach(p => Console.WriteLine($"\t\t IP:{p}"));



            Console.ReadLine();




        }
    }
}
