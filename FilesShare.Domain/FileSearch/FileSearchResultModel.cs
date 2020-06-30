using FilesShare.Domain.Models;
using System.Collections.ObjectModel;

namespace FilesShare.Domain.FileSearch
{
    public class FileSearchResultModel
    {
        public string SerivceHost { get; set; }
        public ObservableCollection<FileMetaData> Files { get; set; }
    }
}
