using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.Models.VO;

namespace Server.Services.Implements
{
    public class MySqlDataBaseService : DataBaseService
    {
        private readonly DatabaseManagementModel _connectionInfo;

        public MySqlDataBaseService(IOptions<DatabaseManagementModel> databaseManagementModel)
        {
            _connectionInfo = databaseManagementModel.Value;
        }

        public MySqlDataBaseService(DatabaseManagementModel databaseManagementModel)
        {
            _connectionInfo = databaseManagementModel;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySQL(_connectionInfo.ConnectionInformation);
    }
}
