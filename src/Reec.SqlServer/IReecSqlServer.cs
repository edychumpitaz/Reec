using Microsoft.Data.SqlClient;
using Reec.DataBase;

namespace Reec.SqlServer
{
    public interface IReecSqlServer : IDbBase<SqlConnection, SqlCommand, SqlParameter, SqlTransaction>
    {

    }

}
