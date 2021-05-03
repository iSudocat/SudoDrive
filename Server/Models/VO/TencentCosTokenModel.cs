using System;

namespace Server.Models.VO
{
    public class TencentCosTokenModel
    {
        public TencentCosCredentialsModel Credentials { get; private init; }
        public long ExpiredTime { get; private init; }
        public DateTime Expiration { get; private init; }
        public string RequestId { get; private init; }
        public long StartTime { get; private init; }

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