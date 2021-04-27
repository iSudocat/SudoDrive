namespace Server.Models.VO
{
    public class ResultModel
    {
        public int? Status { get; private set; }

        public string Message { get; private set; }

        public object Data { get; private set; }


        public ResultModel(int? status, string message, object data)
        {
            this.Status = status;
            this.Message = message;
            this.Data = data;
        }
    }
}
