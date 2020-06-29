using FilesShare.Contracts.Repository;
using FilesShare.Contracts.Services;
using FilesShare.Logics.ServiceManager;

namespace FilesShare.Desktop.PnrpManager
{
    public class PeerServiceHost
    {
        public PeerServiceHost(IPeerRegistrationRepository peerRegistration, IPeerNameResolverRepository peerNameResolver, IPeerConfigurationService<PingService> peerConfigurationService)
        {
            RegisterPeer = peerRegistration;
            ResolvePeer = peerNameResolver;
            ConfigurePeer = peerConfigurationService;
        }

        public IPeerRegistrationRepository RegisterPeer { get; set; }
        public IPeerNameResolverRepository ResolvePeer { get; set; }
        public IPeerConfigurationService<PingService> ConfigurePeer { get; set; }

        public void RunPeerServices()
        {
            if (ConfigurePeer.Peer != null)
            {

            }
        }
    }
}
