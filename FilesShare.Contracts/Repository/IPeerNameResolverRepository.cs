using FilesShare.Domain.Models;

namespace FilesShare.Contracts.Repository
{
    public interface IPeerNameResolverRepository
    {
        void ResolvePeerName(string peerId);

        PeerEndPointsCollection PeerEndPointCollection { get; set; }
    }
}
