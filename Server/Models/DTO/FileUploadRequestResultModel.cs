using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Server.Exceptions;
using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Models.DTO
{
    public class FileUploadRequestResultModel
    {
        public FileModel File { get; private set; }

        public class CredentialsType
        {
            public string Token { get; private set; }

            public CredentialsType(string token)
            {
                this.Token = token;
            }
        }

        public class TokenType
        {
            public CredentialsType Credentials { get; private set; }
            public long ExpiredTime { get; private set; }
            public DateTime Expiration { get; private set; }
            public string RequestId { get; private set; }
            public long StartTime { get; private set; }

            public TokenType(CredentialsType credentials, long expiredTime, DateTime expiration, string requestId, long startTime)
            {
                this.Credentials = credentials;
                this.ExpiredTime = expiredTime;
                this.Expiration = expiration;
                this.RequestId = requestId;
                this.StartTime = startTime;
            }
        };

        public TokenType Token {get; private set;}

        public FileUploadRequestResultModel(File file, Dictionary<string, object> token)
        {
            this.File = file.ToVo();

            if (token == null)
            {
                this.Token = null;
                return;
            }

            JObject resCredentials = token["Credentials"] as JObject;
            if (resCredentials == null)
            {
                // TODO 不知道这里会不会出错
                throw new UnexpectedException();
            }
            CredentialsType credentials =new  CredentialsType(resCredentials["Token"].ToString());

            long expiredTime;
            DateTime expiration;
            string requestId;
            long startTime;
            try
            {
                expiredTime = (long) token["ExpiredTime"];
                expiration = (DateTime) token["Expiration"];
                requestId = (string) token["RequestId"];
                startTime = (int) token["StartTime"];
            }
            catch (Exception)
            {
                // TODO 不知道这里会不会出错
                throw new UnexpectedException();
            }

            this.Token = new TokenType(credentials, expiredTime, expiration, requestId, startTime);
        }

    }
}
