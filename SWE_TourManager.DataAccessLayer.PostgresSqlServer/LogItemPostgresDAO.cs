using SWE_TourManager.DataAccessLayer.Common;
using SWE_TourManager.DataAccessLayer.DAO;
using SWE_TourManager.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.DataAccessLayer.PostgresSqlServer
{
    public class LogItemPostgresDAO : ILogDAO
    {
        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"Logs\" WHERE \"LogId\"= @Id;";
        private const string SQL_FIND_BY_TOUR_ID = "SELECT * FROM public.\"Logs\" WHERE \"TourId\"= @TourId;";
        private const string SQL_INSERT_NEW_LOG = "INSERT INTO public.\"Logs\" (\"LogReport\", \"TourId\", VALUES (@Report, @TourId) RETURNING \"LogId\";";

        private IDatabase database;
        private ITourDAO tourDAO;

        public LogItemPostgresDAO()
        {
            this.database = DALFactory.GetDatabase();
            this.tourDAO = DALFactory.CreateTourDAO();
        }

        public LogItemPostgresDAO(IDatabase database, ITourDAO tourDAO)
        {
            this.database = database;
            this.tourDAO = tourDAO;

        }

        public LogItem AddNewLogItem(string report, TourItem logTourItem)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_LOG);
            database.DefineParameter(insertCommand, "@Report", DbType.String, report);
            database.DefineParameter(insertCommand, "@TourId", DbType.Int32, logTourItem.Id);

            return FindById(database.ExecuteScalar(insertCommand));
        }

        public LogItem FindById(int logId)
        {
            DbCommand findCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findCommand, "@Id", DbType.Int32, logId);

            IEnumerable<LogItem> logItemsList = QueryLogsFromDB(findCommand);
            return logItemsList.FirstOrDefault();
        }

        public IEnumerable<LogItem> GetLogForTourItem(TourItem tour)
        {
            DbCommand getLogsCommand = database.CreateCommand(SQL_FIND_BY_TOUR_ID);
            database.DefineParameter(getLogsCommand, "@TourId", DbType.Int32, tour.Id);
            return QueryLogsFromDB(getLogsCommand);
        }

        private IEnumerable<LogItem> QueryLogsFromDB(DbCommand command)
        {
            List<LogItem> logItemList = new List<LogItem>();

            using (IDataReader reader = database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    logItemList.Add(new LogItem((int)reader["LogId"], (string)reader["LogReport"], tourDAO.FindById((int)reader["TourId"])));
                }
            }
            return logItemList;
        }
    }
}
