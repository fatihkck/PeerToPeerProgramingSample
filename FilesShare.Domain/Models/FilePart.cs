namespace FilesShare.Domain.Models
{
    public class FilePart
    {
        public FilePart(int take, int skip = 0)
        {
            Take = take;
            Skip = skip;
        }

        public int Take { get;  set; }
        public int Skip { get;  set; }
    }
}
