using FilesShare.Contracts.Services;
using FilesShare.Domain.FileSearch;
using FilesShare.Domain.Models;
using System;
using System.Collections.ObjectModel;
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

        public PingService()
        {

        }
        public PingService(HostInfo info)
        {
            FileServiceHost = info;
            ClientHostDetails = new ObservableCollection<HostInfo>();
        }
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
        public HostInfo FileServiceHost { get; set; }
        public ObservableCollection<FileMetaData> AvailableFileMetaData { get; set; }
        public ObservableCollection<HostInfo> ClientHostDetails { get; set; }
        public void SearchFiles(string searchTerm, string peerId)
        {
            if (ClientHostDetails.Any())
            {
                var info = ClientHostDetails.First(p => p.Id == peerId);
                var result = (from file in AvailableFileMetaData where searchTerm == file.FileName select file);

                if (info != null)
                {
                    if (result.Any())
                    {
                        FileSearchResultModel search = new FileSearchResultModel
                        {
                            SerivceHost = FileServiceHost,
                            Files = (ObservableCollection<FileMetaData>)result
                        };
                    }
                }
            }
        }
    }
}
