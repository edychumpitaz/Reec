using Oracle.ManagedDataAccess.Client;
using Reec.DataBase;


namespace Reec.MySql
{
    public interface IReecOracle: IDbBase<OracleConnection, OracleCommand, OracleParameter, OracleTransaction>
    {
    
    }

}
