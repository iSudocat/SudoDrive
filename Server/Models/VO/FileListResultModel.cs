using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Server.Exceptions;
using Server.Models.DTO;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class FileListResultModel
    {
        public List<FileModel> Files { get; private set; }

        public int Amount { get; private set; }

        public int Offset { get; private set; }

        public TencentCosModel TencentCos { get; private set; }

        public TencentCosTokenType Token { get; private set; }

        public FileListResultModel(IEnumerable<File> files, int amount, int offset, Dictionary<string, object> token, TencentCosManagementModel tencentCos)
        {
            List<FileModel> ret = new List<FileModel>();

            foreach (var t in files)
            {
                ret.Add(t.ToVo());
            }

            this.Files = ret;
            this.Amount = ret.Count;
            this.Offset = offset;
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
                expiredTime = (long)token["ExpiredTime"];
                expiration = (DateTime)token["Expiration"];
                requestId = (string)token["RequestId"];
                startTime = (int)token["StartTime"];
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
