using System.Net.PeerToPeer;
using System.Net;
using System.Collections.ObjectModel;

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
