using System.Net.PeerToPeer;

namespace FilesShare.Domain.Models
{
    public class Peer<T>
    {
        public string UserName { get; set; }
        public PeerName PeerName { get; set; }
        public T Channel { get; set; }
        public T Host { get; set; }
    }
}
