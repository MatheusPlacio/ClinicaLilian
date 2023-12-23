using Domain.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Data.RepositoryDapper
{
    public abstract class BaseRepository
    {
        protected IOptions<DatabaseSettings> _connection;

        public BaseRepository(IOptions<DatabaseSettings> connection)
        {
            _connection = connection;
        }

        protected SqlConnection GetConnection()
        {
            var connectionString = _connection.Value.DefaultConnection;
            return new SqlConnection(connectionString);
        }
    }
}
