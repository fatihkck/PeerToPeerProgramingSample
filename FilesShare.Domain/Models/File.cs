namespace FilesShare.Domain.Models
{
    public class File
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int FileLenght { get; set; }
        public byte[] FileContent { get; set; }
    }
}
