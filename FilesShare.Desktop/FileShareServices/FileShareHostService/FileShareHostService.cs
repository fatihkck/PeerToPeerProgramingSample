using FilesShare.Contracts.FileShare;
using FilesShare.Logics.FileShareManager;
using System;
using System.ServiceModel;

namespace FilesShare.Desktop.FileShareServices.FileShareHostService
{
    public class FileShareHostService : IFileShareHostService
    {

        private ServiceHost _host;

        public FileShareHostService(int port,string uri)
        {
            Port = port;
            Uri = uri;
            IsStarted = false;

        }

        public int Port { get; set; }
        public string Uri { get; set; }
        public bool IsStarted { get; set; }

        public bool Start()
        {
            var uri = new Uri[1];
            if (!string.IsNullOrEmpty(Uri) && Port > 0)
            {
                var address = $"net.tcp://{Uri}:{Port}/FileShare";
                uri[0] = new Uri(address);
                IFileShareService fileShare = new FileManager();
                _host = new ServiceHost(fileShare, uri);
                var binding = new NetTcpBinding(SecurityMode.None);
                _host.AddServiceEndpoint(typeof(IFileShareService), binding, "");
                _host.Opened += HostOnOpened;
                _host.Open();
            }

            return IsStarted;
        }

        public bool Stop()
        {
            if (_host != null)
            {
                _host.Closed += _hostOnClosed;
                _host.Close();
                _host = null;
            }

            return IsStarted;
        }

        private void _hostOnClosed(object sender, EventArgs e)
        {
            IsStarted = true;
        }

        private void HostOnOpened(object sender, EventArgs e)
        {
            IsStarted = true;
        }
    }
}
