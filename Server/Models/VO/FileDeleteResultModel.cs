namespace Server.Models.VO
{
    public class FileDeleteResultModel
    {
        public long Count { get; private set; }

        public FileDeleteResultModel(long count)
        {
            this.Count = count;
        }
    }
}
