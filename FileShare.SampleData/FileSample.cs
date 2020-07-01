using FilesShare.Domain.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FileShare.SampleData
{
    public static class FileSample
    {
        private static ObservableCollection<File> _availableFiles = new ObservableCollection<File>();
        private static ObservableCollection<FileMetaData> _metaDatas = new ObservableCollection<FileMetaData>();

        public static ObservableCollection<File> GetAvailableFiles()
        {
            if (!_availableFiles.Any())
            {
                _availableFiles.Add(new Task<File>(() =>
                {
                    var bytes = new byte[23234345];
                    var file = new File {
                        FileId = Guid.NewGuid().ToString().Split('-')[4],
                        FileName = "Max Payne",
                        FileContent = bytes,
                        FileLenght = bytes.Length,
                        FileType = "video/mp4"
                        
                    };
                    _metaDatas.Add(file.GetFileMeta());

                    return file;
                }).Result);

                _availableFiles.Add(new Task<File>(() =>
                {
                    var bytes = new byte[8234345];
                    var file = new File
                    {
                        FileId = Guid.NewGuid().ToString().Split('-')[4],
                        FileName = "Max Payne 1",
                        FileContent = bytes,
                        FileLenght = bytes.Length,
                        FileType = "video/mp4"

                    };
                    _metaDatas.Add(file.GetFileMeta());

                    return file;
                }).Result);

                _availableFiles.Add(new Task<File>(() =>
                {
                    var bytes = new byte[8284345];
                    var file = new File
                    {
                        FileId = Guid.NewGuid().ToString().Split('-')[4],
                        FileName = "Max Payne 2",
                        FileContent = bytes,
                        FileLenght = bytes.Length,
                        FileType = "video/mp4"

                    };
                    _metaDatas.Add(file.GetFileMeta());

                    return file;
                }).Result);

                _availableFiles.Add(new Task<File>(() =>
                {
                    var bytes = new byte[5234345];
                    var file = new File
                    {
                        FileId = Guid.NewGuid().ToString().Split('-')[4],
                        FileName = "Max Payne 3",
                        FileContent = bytes,
                        FileLenght = bytes.Length,
                        FileType = "video/mp4"

                    };
                    _metaDatas.Add(file.GetFileMeta());

                    return file;
                }).Result);

                _availableFiles.Add(new Task<File>(() =>
                {
                    var bytes = new byte[5234348];
                    var file = new File
                    {
                        FileId = Guid.NewGuid().ToString().Split('-')[4],
                        FileName = "Max Payne 4",
                        FileContent = bytes,
                        FileLenght = bytes.Length,
                        FileType = "video/mp4"

                    };
                    _metaDatas.Add(file.GetFileMeta());

                    return file;
                }).Result);
            }
            return _availableFiles;
        }
    }
}
