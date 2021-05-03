namespace Server.Models.VO
{
    public class FileDeleteResultModel
    {
        public long Count { get; private init; }

        public FileDeleteResultModel(long count)
        {
            this.Count = count;
        }
    }
}
