namespace Server.Models.DTO
{
    public class RequestUploadResultModel
    {
        

        public string Path { get; private set; }

        public string Guid { get; private set; }

        public int Status { get; private set; }

        public RequestUploadResultModel(string path, string guid, int status)
        {
            this.Path = path;
            this.Guid = guid;
            this.Status = status;
        }
    }
}