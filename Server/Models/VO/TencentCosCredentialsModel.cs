namespace Server.Models.VO
{
    public class TencentCosCredentialsModel
    {
        public string Token { get; private init; }
        public string TmpSecretId { get; private init; }
        public string TmpSecretKey { get; private init; }

        public TencentCosCredentialsModel(string token, string tmpSecretId, string tmpSecretKey)
        {
            this.Token = token;
            this.TmpSecretId = tmpSecretId;
            this.TmpSecretKey = tmpSecretKey;
        }
    }
}