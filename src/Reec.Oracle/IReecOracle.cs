using Oracle.ManagedDataAccess.Client;
using Reec.DataBase;


namespace Reec.Oracle
{
    public interface IReecOracle : IDbBase<OracleConnection, OracleCommand, OracleParameter, OracleTransaction>
    {

    }

}
