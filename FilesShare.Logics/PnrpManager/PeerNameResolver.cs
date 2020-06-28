using FilesShare.Contracts.Repository;
using System;
using System.Linq;
using FilesShare.Domain.Models;
using System.Net.PeerToPeer;

namespace FilesShare.Logics.PnrpManager
{
    public class PeerNameResolver : IPeerNameResolverRepository
    {
        private PeerEndPointsCollection _peers;

        private string _username;



        public PeerNameResolver(string username)
        {
            _username = username;
        }

        public PeerEndPointsCollection PeerEndPointCollection { get; set; }

        public void ResolvePeerName(string peerId)
        {
            if (string.IsNullOrEmpty(_username))
                throw new ArgumentNullException(nameof(_username));

            System.Net.PeerToPeer.PeerNameResolver resolver = new System.Net.PeerToPeer.PeerNameResolver();
            var result = resolver.Resolve(new PeerName(peerId, PeerNameType.Unsecured), Cloud.AllLinkLocal);

            //if (result.Any())
            //{
            //    PeerEndPointCollection = new PeerEndPointsCollection(result[0].PeerName, result[0].EndPointCollection);
            //}
        }




    }
}
