using FilesShare.Contracts.Services;
using FilesShare.Domain.FileSearch;
using FilesShare.Domain.Models;
using System;
using System.Linq;
using System.Net;

namespace FilesShare.Logics.ServiceManager
{
    public delegate void OnPeerInfo(HostInfo endPointInfo);
    public delegate void FileSearchResultDelegate(FileSearchResultModel fileSearch);

    public class PingService : IPingService
    {
        public event OnPeerInfo PeerEndPointInformation;
        public event FileSearchResultDelegate FileSearchResult;

        public void Ping(HostInfo info)
        {
            var host = Dns.GetHostEntry(info.Uri);
            IPEndPointCollection ips = new IPEndPointCollection();
            host.AddressList?.ToList().ForEach(p => { ips.Add(new IPEndPoint(p, info.Port)); });
            var peerInfo = new PeerEndPointInfo
            {
                LastUpdated = DateTime.UtcNow,
                PeerUri = info.Uri, 
                PeerIpCollection = ips
            };

            PeerEndPointInformation?.Invoke(info);
        }

        public void SearchFiles(string searchTerm, string clientHost)
        {
            
        }
    }
}
