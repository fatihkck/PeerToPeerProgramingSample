using FilesShare.Contracts.Repository;
using FilesShare.Contracts.Services;
using FilesShare.Domain.Models;
using FilesShare.Logics.PnrpManager;
using FilesShare.Logics.ServiceManager;
using FilesShare.Test.PeerHostServices;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;

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
            Console.WriteLine("Enter your username");
            string username = Console.ReadLine();

            Peer<IPingService> peer = new Peer<IPingService>
            {
                PeerId = Guid.NewGuid().ToString().Split('-')[4],
                UserName = username
            };
            IPeerRegistrationRepository peerRegistration = new PeerRegistrationManager();
            IPeerNameResolverRepository peerNameResolverRepository = new PeerNameResolver(peer.PeerId);
            IPeerConfigurationService peerConfigurationService = new PeerConfigurationService(peer);

            //peerRegistration.StartPeerRegistration(peer.UserName, peerConfigurationService.Port);

            //Console.WriteLine("Peer Information");
            //Console.WriteLine($"Peer Uri : {peerRegistration.PeerUri}   Port : {peerConfigurationService.Port}");

            //var host = Dns.GetHostEntry(peerRegistration.PeerUri);
            //host.AddressList?.ToList().ForEach(p => Console.WriteLine($"\t\t IP:{p}"));

            PeerServiceHost psh = new PeerServiceHost(peerRegistration, peerNameResolverRepository, peerConfigurationService);
            //psh.RunPeerServiceHost(peer);
            Thread thd = new Thread(() =>
            {
                psh.RunPeerServiceHost(peer);
            })
            { IsBackground = true };
            thd.Start();


            Console.ReadLine();




        }
    }
}
