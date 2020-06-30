using FilesShare.Domain.FileSearch;
using FilesShare.Domain.Models;
using System.ServiceModel;

namespace FilesShare.Contracts.FileShare
{
    [ServiceContract]
    public interface IFileShareService
    {
        [OperationContract(IsOneWay = false)]
        FilePartModel GetAllFileByte(FileMetaData fileMeta);

        [OperationContract(IsOneWay = false)]
        FilePartModel GetFilePartBytes(FilePart filePart, FileMetaData fileMeta);

        [OperationContract(IsOneWay = false)]
        void ForwardResult(FileSearchResultModel result);
    }
}
