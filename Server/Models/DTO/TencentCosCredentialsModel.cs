namespace Server.Models.DTO
{
    public class TencentCosCredentialsModel
    {
        public string Token { get; private set; }
        public string TmpSecretId { get; private set; }
        public string TmpSecretKey { get; private set; }

        public TencentCosCredentialsModel(string token, string tmpSecretId, string tmpSecretKey)
        {
            this.Token = token;
            this.TmpSecretId = tmpSecretId;
            this.TmpSecretKey = tmpSecretKey;
        }
    }
}