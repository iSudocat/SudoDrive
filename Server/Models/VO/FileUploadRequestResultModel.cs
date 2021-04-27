using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Server.Exceptions;
using Server.Models.DTO;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class FileUploadRequestResultModel
    {
        public FileModel File { get; private set; }

        public TencentCosTokenType Token {get; private set;}

        public TencentCosModel TencentCos { get; private set; }


        public FileUploadRequestResultModel(File file, Dictionary<string, object> token, TencentCosManagementModel tencentCos)
        {
            this.File = file.ToVo();
            this.TencentCos = new TencentCosModel(tencentCos);

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
            TencentCosCredentialsModel tencentCosCredentials = new TencentCosCredentialsModel(resCredentials["Token"].ToString(), resCredentials["TmpSecretId"].ToString(), resCredentials["TmpSecretKey"].ToString());

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

            this.Token = new TencentCosTokenType(tencentCosCredentials, expiredTime, expiration, requestId, startTime);
        }

    }
}
