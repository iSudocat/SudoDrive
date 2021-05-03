namespace Server.Models.VO
{
    public class ResultModel
    {
        public int? Status { get; private init; }

        public string Message { get; private init; }

        public object Data { get; private init; }


        public ResultModel(int? status, string message, object data)
        {
            this.Status = status;
            this.Message = message;
            this.Data = data;
        }
    }
}
