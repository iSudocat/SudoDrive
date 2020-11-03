using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.Models.VO;

namespace Server.Services.Implements
{
    public class PostgreSqlDataBaseService : DataBaseService
    {
        private readonly DatabaseManagementModel _connectionInfo;

        public PostgreSqlDataBaseService(IOptions<DatabaseManagementModel> databaseManagementModel)
        {
            _connectionInfo = databaseManagementModel.Value;
        }

        public PostgreSqlDataBaseService(DatabaseManagementModel databaseManagementModel)
        {
            _connectionInfo = databaseManagementModel;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(_connectionInfo.ConnectionInformation);
    }
}
