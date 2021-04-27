using System;

namespace Server.Models.VO
{
    public class TencentCosTokenModel
    {
        public TencentCosCredentialsModel Credentials { get; private set; }
        public long ExpiredTime { get; private set; }
        public DateTime Expiration { get; private set; }
        public string RequestId { get; private set; }
        public long StartTime { get; private set; }

        public TencentCosTokenModel(TencentCosCredentialsModel credentials, long expiredTime, DateTime expiration, string requestId, long startTime)
        {
            this.Credentials = credentials;
            this.ExpiredTime = expiredTime;
            this.Expiration = expiration;
            this.RequestId = requestId;
            this.StartTime = startTime;
        }
    };
}