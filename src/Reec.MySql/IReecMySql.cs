using MySql.Data.MySqlClient;
using Reec.DataBase;


namespace Reec.MySql
{
    public interface IReecMySql : IDbBase<MySqlConnection, MySqlCommand, MySqlParameter, MySqlTransaction>
    {

    }

}
