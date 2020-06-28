using FilesShare.Contracts.Repository;
using FilesShare.Contracts.Services;
using FilesShare.Domain.Models;
using FilesShare.Logics.ServiceManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FilesShare.Test.PeerHostServices
{
    public class PeerServiceHost
    {
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);

        public PeerServiceHost(IPeerRegistrationRepository peerRegistration, IPeerNameResolverRepository peerNameResolver, IPeerConfigurationService<PingService> peerConfigurationService)
        {
            RegisterPeer = peerRegistration;
            ResolvePeer = peerNameResolver;
            ConfigurePeer = peerConfigurationService;
        }

        public IPeerRegistrationRepository RegisterPeer { get; set; }
        public IPeerNameResolverRepository ResolvePeer { get; set; }
        public IPeerConfigurationService<PingService> ConfigurePeer { get; set; }

        public void RunPeerServiceHost(Peer<IPingService> peer)
        {
            if (peer == null)
                throw new ArgumentNullException(nameof(peer));

            RegisterPeer.StartPeerRegistration(peer.PeerId, ConfigurePeer.Port);

            if (RegisterPeer.IsPeerRegistered)
            {
                Console.WriteLine($"{peer.UserName} Registration completed");
                Console.WriteLine($"Peer Uri : {RegisterPeer.PeerUri}  Port : {ConfigurePeer.Port}");
            }

            if (ResolvePeer != null)
            {
                Console.WriteLine($"Resolving {peer.UserName}");
                ResolvePeer.ResolvePeerName(peer.PeerId);
                var result = ResolvePeer.PeerEndPointCollection;

                Console.WriteLine($"\t\t EndPoints for {RegisterPeer.PeerUri}");
                if (ConfigurePeer.StartPeerServices())
                {
                    Console.WriteLine("Peer services started");

                    peer.Channel.Ping(ConfigurePeer.Port, RegisterPeer.PeerUri);

                    if (ConfigurePeer.PingService != null)
                    {
                        ConfigurePeer.PingService.PeerEndPointInformation += PingService_PeerEndPointInformation;
                    }

                }
                else
                {
                    Console.WriteLine("error starting up peer services");
                }
            }

        }

        private void PingService_PeerEndPointInformation(PeerEndPointInfo endPointInfo)
        {
            if (endPointInfo != null)
            {
                Console.WriteLine($"New Peer EndPoint ********** {endPointInfo.PeerUri}");
                endPointInfo.PeerIpCollection.ToList()?.ForEach(p => Console.WriteLine($"\t\t\t IP: {p.Address}:{p.Port}"));
            }
        }
    }
}
