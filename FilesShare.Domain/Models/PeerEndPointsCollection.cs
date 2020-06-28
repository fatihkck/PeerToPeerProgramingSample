using System.Collections.ObjectModel;
using System.Net.PeerToPeer;

namespace FilesShare.Domain.Models
{
    public class PeerEndPointsCollection
    {
        public PeerEndPointsCollection(PeerName peer)
        {
            PeerHostName = peer;
            PeerEndPoints = new ObservableCollection<PeerEndPointInfo>();
        }

        public PeerName PeerHostName { get; }
        public ObservableCollection<PeerEndPointInfo> PeerEndPoints { get; set; }
    }
}
