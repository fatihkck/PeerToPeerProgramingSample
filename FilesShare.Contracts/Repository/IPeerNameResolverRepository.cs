using FilesShare.Domain.Models;

namespace FilesShare.Contracts.Repository
{
    public interface IPeerNameResolverRepository
    {
        void ResolvePeerName();

        PeerEndPointsCollection PeerEndPointCollection { get; set; }
    }
}
