using FilesShare.Domain.Models;

namespace FilesShare.Contracts.Services
{
    public interface IPeerConfigurationService<T>
    {
        int Port { get; }
        Peer<IPingService> Peer { get; }
        T PingService { get; set; }
        bool StartPeerServices();
        bool StopPeerService();
    }
}
