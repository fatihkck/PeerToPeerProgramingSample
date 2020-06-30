namespace FilesShare.Contracts.FileShare
{
    public interface IFileShareHostService
    {
        bool Stop();
        bool Start();
    }
}
