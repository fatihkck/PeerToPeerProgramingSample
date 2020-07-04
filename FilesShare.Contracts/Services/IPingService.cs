using FilesShare.Domain.Models;
using System.ServiceModel;

namespace FilesShare.Contracts.Services
{
    [ServiceContract(CallbackContract = typeof(IPingService))]
    public interface IPingService
    {
        [OperationContract(IsOneWay = true)]
        void Ping(HostInfo info);

        [OperationContract(IsOneWay = true)]
        void SearchFiles(string searchTerm, string peerId);
    }
}
