using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.DataAccessLayer.Common
{
    public interface IDatabase
    {
        DbCommand CreateCommand(string genericCommandText);
        int DeclareParameter(DbCommand command, string name, DbType type);
        void DefineParameter(DbCommand command, string name, DbType type, object value);
        void SetParameter(DbCommand command, string name, object value);

        IDataReader ExecuteReader(DbCommand command);
        int ExecuteScalar(DbCommand command);
        int ExecuteNonQuery(DbCommand command);
    }
}
