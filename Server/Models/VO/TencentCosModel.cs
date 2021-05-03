using Server.Models.VO;

namespace Server.Models.DTO
{
    public class TencentCosModel
    {
        public string Region { get; init; }

        public string Bucket { get; init; }

        public TencentCosModel(TencentCosManagementModel tencentCos)
        {
            this.Bucket = tencentCos.Bucket;
            this.Region = tencentCos.Region;
        }
    }
}