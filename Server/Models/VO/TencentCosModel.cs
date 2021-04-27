using Server.Models.VO;

namespace Server.Models.DTO
{
    public class TencentCosModel
    {
        public string Region { get; set; }

        public string Bucket { get; set; }

        public TencentCosModel(TencentCosManagementModel tencentCos)
        {
            this.Bucket = tencentCos.Bucket;
            this.Region = tencentCos.Region;
        }
    }
}