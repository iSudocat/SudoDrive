using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.Models.VO;

namespace Server.Services.Implements
{
    public class DataBaseService : DbContext, IDatabaseService
    {
        private DatabaseManagementModel _connectionInfo;

        public DataBaseService(IOptions<DatabaseManagementModel> databaseManagementModel)
        {
            _connectionInfo = databaseManagementModel.Value;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql(_connectionInfo.ConnectionInfo);

    }
}
