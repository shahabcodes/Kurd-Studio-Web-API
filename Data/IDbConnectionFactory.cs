using System.Data;

namespace KurdStudio.Api.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
