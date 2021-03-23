using Oracle.ManagedDataAccess.Client;
using Reec.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reec.MySql
{
    public class ReecOracle : DbBase<OracleConnection, OracleCommand, OracleParameter, OracleTransaction>, IReecOracle
    {
        public ReecOracle(string connection, int commandTimeOut = 30) 
            : base(connection, commandTimeOut)
        {
        
        }

        public ReecOracle(OracleTransaction transaction, int commandTimeOut = 30)
            : base(transaction, commandTimeOut)
        {
        
        }

    }

}
