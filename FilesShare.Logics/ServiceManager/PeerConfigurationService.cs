using FilesShare.Contracts.Services;
using FilesShare.Domain.Models;
using System;
using System.Net;
using System.Net.PeerToPeer;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace FilesShare.Logics.ServiceManager
{
    public class PeerConfigurationService : IPeerConfigurationService<PingService>
    {

        #region Fields
        private int _port;
        private ICommunicationObject Communication;
        private DuplexChannelFactory<IPingService> _factory;
        private bool _isServiceStarted;
        #endregion

        public PeerConfigurationService(Peer<IPingService> peer)
        {
            Peer = peer;
            PingService = new PingService();
        }
        public int Port => FindFreePort();

        private int FindFreePort()
        {
            int port;
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP))
            {
                socket.Bind(endpoint);
                IPEndPoint local = (IPEndPoint)socket.LocalEndPoint;
                port = local.Port;
            }

            if (port == 0)
                throw new ArgumentNullException(nameof(port));

            return port;
        }

        public Peer<IPingService> Peer { get; }
        public PingService PingService { get; set; }

        public bool StartPeerServices()
        {
#pragma warning disable 618
            var binding = new NetPeerTcpBinding
            {
                Security = { Mode = SecurityMode.None }

            };
#pragma warning disable 618

            var endPoint = new ServiceEndpoint(
                ContractDescription.GetContract(typeof(IPingService))
                , binding
                , new EndpointAddress("net.p2p://Fileshare")
                );
            Peer.Host = PingService;
            _factory = new DuplexChannelFactory<IPingService>(new InstanceContext(Peer.Host), endPoint);
            Peer.Channel = _factory.CreateChannel();
            Communication = (ICommunicationObject)Peer.Channel;
            if (Communication != null)
            {
                Communication.Opened += Communication_Opened;

                try
                {
                    Communication.Open();
                    if (_isServiceStarted)
                        return _isServiceStarted;
                }
                catch (PeerToPeerException ex)
                {
                    Console.WriteLine(ex);
                    throw new PeerToPeerException("error establishing peer services");
                }
            }

            return _isServiceStarted;
        }
        public bool StopPeerService()
        {
            if (Communication != null)
            {
                Communication.Close();
                Communication = null;
                _factory = null;

                return true;
            }

            return false;
        }

        private void Communication_Opened(object sender, EventArgs e)
        {
            _isServiceStarted = true;
        }
    }
}
