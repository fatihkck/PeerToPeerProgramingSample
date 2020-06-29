using FilesShare.Contracts.Services;
using FilesShare.Domain.FileSearch;
using FilesShare.Domain.Models;
using System;
using System.Linq;
using System.Net;

namespace FilesShare.Logics.ServiceManager
{
    public delegate void OnPeerInfo(PeerEndPointInfo endPointInfo);
    public delegate void FileSearchResultDelegate(FileSearchResultModel fileSearch);

    public class PingService : IPingService
    {
        public event OnPeerInfo PeerEndPointInformation;
        public event FileSearchResultDelegate FileSearchResult;

        public void Ping(int port, string peerUri)
        {
            var host = Dns.GetHostEntry(peerUri);
            IPEndPointCollection ips = new IPEndPointCollection();
            host.AddressList?.ToList().ForEach(p => { ips.Add(new IPEndPoint(p, port)); });
            var peerInfo = new PeerEndPointInfo
            {
                LastUpdated = DateTime.UtcNow,
                PeerUri = peerUri,
                PeerIpCollection = ips
            };

            PeerEndPointInformation?.Invoke(peerInfo);
        }

        public void SearchFiles(string searchTerm)
        {
            
        }
    }
}
