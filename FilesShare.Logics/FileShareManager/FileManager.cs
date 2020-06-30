using FilesShare.Contracts.FileShare;
using FilesShare.Domain.FileSearch;
using FilesShare.Domain.Models;
using System;

namespace FilesShare.Logics.FileShareManager
{
    public class FileManager : IFileShareService
    {
        public void ForwardResult(FileSearchResultModel result)
        {
            throw new NotImplementedException();
        }

        public FilePartModel GetAllFileByte(FileMetaData fileMeta)
        {
            throw new NotImplementedException();
        }

        public FilePartModel GetFilePartBytes(FilePart filePart, FileMetaData fileMeta)
        {
            throw new NotImplementedException();
        }
    }
}
