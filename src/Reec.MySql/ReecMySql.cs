using MySql.Data.MySqlClient;
using Reec.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reec.MySql
{
    public class ReecMySql : DbBase<MySqlConnection, MySqlCommand, MySqlParameter, MySqlTransaction>, IReecMySql
    {
        public ReecMySql(string connection, int commandTimeOut = 30) 
            : base(connection, commandTimeOut)
        {
        
        }

        public ReecMySql(MySqlTransaction transaction, int commandTimeOut = 30)
            : base(transaction, commandTimeOut)
        {
        
        }

    }

}
