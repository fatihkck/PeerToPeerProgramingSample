using FilesShare.Domain.Models;

namespace FilesShare.Contracts.Services
{
    public interface IPeerConfigurationService
    {
        int Port { get; }
        Peer<IPingService> Peer { get; }
        bool StartPeerServices();
        bool StopPeerService();
    }
}
