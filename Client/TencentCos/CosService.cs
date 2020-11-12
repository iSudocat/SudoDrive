using COSXML;
using COSXML.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.TencentCos
{
    public class CosService
    {
        private CosXmlConfig config;
        private QCloudCredentialProvider cosCredentialProvider;
        private CosXml cosXml;

        /// <summary>
        /// 初始化CosService类
        /// </summary>
        public CosService(string region)
        {
            config = new CosXmlConfig.Builder()
              .IsHttps(true)  //设置默认 HTTPS 请求
              .SetRegion(region)  //设置一个默认的存储桶地域
              .SetDebugLog(true)  //显示日志
              .Build();  //创建 CosXmlConfig 对象
        }

        /// <summary>
        /// 获取Cos操作的CosXml类
        /// </summary>
        /// <param name="tmpSecretId">临时密钥 SecretId</param>
        /// <param name="tmpSecretKey">临时密钥 SecretKey</param>
        /// <param name="tmpToken">临时密钥 token</param>
        /// <param name="tmpExpireTime">临时密钥有效截止时间，精确到秒</param>
        public CosXml getCosXml(string tmpSecretId, string tmpSecretKey, string tmpToken, long tmpExpireTime)
        {
            cosCredentialProvider = new DefaultSessionQCloudCredentialProvider(tmpSecretId, tmpSecretKey, tmpExpireTime, tmpToken);
            cosXml = new CosXmlServer(config, cosCredentialProvider);
            return cosXml;
        }
    }
}
