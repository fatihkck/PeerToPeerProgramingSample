using System.ServiceModel;

namespace FilesShare.Contracts.Services
{
    [ServiceContract(CallbackContract = typeof(IPingService))]
    public interface IPingService
    {
        [OperationContract(IsOneWay = true)]
        void Ping(int port, string peerUri);

        [OperationContract(IsOneWay = true)]
        void SearchFiles(string searchTerm);
    }
}
