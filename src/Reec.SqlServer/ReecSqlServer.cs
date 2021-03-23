using Microsoft.Data.SqlClient;
using Reec.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reec.SqlServer
{
    public class ReecSqlServer : DbBase<SqlConnection, SqlCommand, SqlParameter, SqlTransaction>, IReecSqlServer
    {
        public ReecSqlServer(string connection, int commandTimeOut = 30) 
            : base(connection, commandTimeOut)
        {
        
        }

        public ReecSqlServer(SqlTransaction transaction, int commandTimeOut = 30)
            : base(transaction, commandTimeOut)
        {
        
        }

    }

}
