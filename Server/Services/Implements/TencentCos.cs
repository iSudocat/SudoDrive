using System;
using System.Collections.Generic;
using System.IO;
using COSSTS;
using Microsoft.Extensions.Options;
using EntityFile = Server.Models.Entities.File;
using Server.Models.VO;

namespace Server.Services.Implements
{
    public class TencentCos : ITencentCos
    {
        private static int _uploadDuration = 1800;
        private static int _downloadDuration = 86400;

        private readonly TencentCosManagementModel _tencentCosManagement;

        public TencentCos(IOptions<TencentCosManagementModel> TencentCosManagement)
        {
            _tencentCosManagement = TencentCosManagement.Value;
        }

        public Dictionary<string, object> GetUploadToken(EntityFile file)
        {
            string bucket = _tencentCosManagement.Bucket;
            string region = _tencentCosManagement.Region;
            string allowPrefix = file.StorageName;
            string[] allowActions = new string[] {
                "name/cos:PutObject",
                "name/cos:PostObject",
                "name/cos:InitiateMultipartUpload",
                "name/cos:ListMultipartUploads",
                "name/cos:ListParts",
                "name/cos:UploadPart",
                "name/cos:CompleteMultipartUpload"
            };
            string secretId = _tencentCosManagement.SecretId;
            string secretKey = _tencentCosManagement.SecretKey;

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("bucket", bucket);
            values.Add("region", region);
            values.Add("allowPrefix", allowPrefix);
            values.Add("allowActions", allowActions);
            values.Add("durationSeconds", _uploadDuration);

            values.Add("secretId", secretId);
            values.Add("secretKey", secretKey);

            values.Add("Domain", "sts.tencentcloudapi.com");

            Dictionary<string, object> credential = STSClient.genCredential(values);
            return credential;
        }

        public Dictionary<string, object> GetDownloadToken(List<string> allowPrefix)
        {
            string bucket = _tencentCosManagement.Bucket;
            string region = _tencentCosManagement.Region;
            string[] allowActions = new string[] {
                "name/cos:HeadObject",
                "name/cos:GetObject"
            };
            string secretId = _tencentCosManagement.SecretId;
            string secretKey = _tencentCosManagement.SecretKey;

            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("bucket", bucket);
            values.Add("region", region);
            values.Add("allowPrefixes", allowPrefix.ToArray());
            values.Add("allowActions", allowActions);
            values.Add("durationSeconds", _downloadDuration);

            values.Add("secretId", secretId);
            values.Add("secretKey", secretKey);

            values.Add("Domain", "sts.tencentcloudapi.com");

            Dictionary<string, object> credential = STSClient.genCredential(values);
            return credential;
        }


    }
}
