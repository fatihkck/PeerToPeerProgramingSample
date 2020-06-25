using System.Net.PeerToPeer;
using System.Net;

namespace FilesShare.Domain.Models
{
    public class PeerEndPointsCollection
    {
        public PeerEndPointsCollection(PeerName peer, IPEndPointCollection iPEndPoints)
        {
            PeerHostName = peer;
            PeerEndPoints = iPEndPoints;
        }

        public PeerName PeerHostName { get; }
        public IPEndPointCollection PeerEndPoints { get; }
    }
}
