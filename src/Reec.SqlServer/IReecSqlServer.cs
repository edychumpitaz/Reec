using Microsoft.Data.SqlClient;
using Reec.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reec.SqlServer
{
    public interface IReecSqlServer: IDbBase<SqlConnection, SqlCommand, SqlParameter, SqlTransaction>
    {
    
    }

}
