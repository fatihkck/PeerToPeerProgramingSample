using FilesShare.Contracts.Repository;
using FilesShare.Contracts.Services;
using FilesShare.Domain.Models;
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

        public PeerServiceHost(IPeerRegistrationRepository peerRegistration,IPeerNameResolverRepository peerNameResolver,IPeerConfigurationService peerConfigurationService)
        {
            RegisterPeer = peerRegistration;
            ResolvePeer = peerNameResolver;
            ConfigurePeer = peerConfigurationService;
        }

        public IPeerRegistrationRepository RegisterPeer { get; set; }
        public IPeerNameResolverRepository ResolvePeer { get; set; }
        public IPeerConfigurationService ConfigurePeer { get; set; }

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

                }
                else
                {
                    Console.WriteLine("error starting up peer services");
                }


                //if (result != null)
                //{
                //    result.PeerEndPoints.ToList().ForEach(p => Console.WriteLine($"IP : {p.Address}"));
                //}
                //else
                //{
                //    Console.WriteLine($"\t\t no record for {peer.UserName}");
                //}
            }

        }
    }
}
