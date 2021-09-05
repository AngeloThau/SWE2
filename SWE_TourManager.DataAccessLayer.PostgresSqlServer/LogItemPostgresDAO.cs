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
        private const string SQL_INSERT_NEW_LOG = "INSERT INTO public.\"Logs\"( \"TourId\", \"LogName\", \"LogDistance\", \"LogTime\", \"LogRating\", \"LogSpeed\", \"LogVerUp\", \"LogVerDown\", \"LogDiff\", \"LogDate\", \"LogReport\") VALUES( @TourId, @LogName, @LogDistance, @LogTime, @LogRating, @LogSpeed, @LogVerUp, @LogVerDown, @LogDiff, @LogDate, @LogReport);";

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

        public LogItem AddNewLogItem(TourItem logTourItem, string logName, double logDistance, int logTime, int logRating, int logSpeed, int logVerUp, int logVerDown, int logDiff, DateTime logDate, string logReport)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_LOG);
            database.DefineParameter(insertCommand, "@TourId", DbType.Int32, logTourItem.Id);
            database.DefineParameter(insertCommand, "@LogName", DbType.String, logName);
            database.DefineParameter(insertCommand, "@LogDistance", DbType.Double, logDistance);
            database.DefineParameter(insertCommand, "@LogTime", DbType.Int32, logTime);
            database.DefineParameter(insertCommand, "@LogRating", DbType.Int32, logRating);
            database.DefineParameter(insertCommand, "@LogSpeed", DbType.Int32, logSpeed);
            database.DefineParameter(insertCommand, "@LogVerUp", DbType.Int32, logVerUp);
            database.DefineParameter(insertCommand, "@LogVerDown", DbType.Int32, logVerDown);
            database.DefineParameter(insertCommand, "@LogDiff", DbType.Int32, logDiff);
            database.DefineParameter(insertCommand, "@LogDate", DbType.DateTime, logDate);
            database.DefineParameter(insertCommand, "@LogReport", DbType.String, logReport);


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
                    logItemList.Add(new LogItem(
                        tourDAO.FindById((int)reader["TourId"]),
                        (int)reader["LogId"],
                        (string)reader["LogName"],
                        (double)reader["LogDistance"],
                        (int)reader["LogTime"],
                        (int)reader["LogRating"],
                        (int)reader["LogSpeed"],
                        (int)reader["LogVerUp"],
                        (int)reader["LogVerDown"],
                        (int)reader["LogDiff"],
                        (DateTime)reader["LogDate"],
                        (string)reader["LogReport"]                       
                        ));
                }
            }
            return logItemList;
        }
    }
}
