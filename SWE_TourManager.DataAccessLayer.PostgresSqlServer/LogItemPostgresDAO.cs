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
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"Logs\" WHERE \"LogId\"= @Id;";
        private const string SQL_FIND_BY_TOUR_ID = "SELECT * FROM public.\"Logs\" WHERE \"TourId\"= @TourId;";
        private const string SQL_INSERT_NEW_LOG = "INSERT INTO public.\"Logs\"( \"TourId\", \"LogName\", \"LogDistance\", \"LogTime\", \"LogRating\", \"LogSpeed\", \"LogVerUp\", \"LogVerDown\", \"LogDiff\", \"LogDate\", \"LogReport\") VALUES( @TourId, @LogName, @LogDistance, @LogTime, @LogRating, @LogSpeed, @LogVerUp, @LogVerDown, @LogDiff, @LogDate, @LogReport);";
        private const string SQL_DELETE_BY_TOUR_ID = "DELETE FROM public.\"Logs\" WHERE \"TourId\"= @TourId;";
        private const string SQL_DELETE_BY_LOG_ID = "DELETE FROM public.\"Logs\" WHERE \"LogId\"= @LogId;";
        private const string SQL_UPDATE_LOG = "UPDATE public.\"Logs\" SET \"LogName\"= @LogName, \"LogDistance\"= @LogDistance, \"LogTime\"= @LogTime, \"LogSpeed\"= @LogSpeed , \"LogVerUp\"= @LogVerUp , \"LogVerDown\"= @LogVerDown , \"LogDiff\"= @LogDiff , \"LogDate\"= @LogDate , \"LogReport\"= @LogReport , \"LogRating\"= @LogRating WHERE \"LogId\"= @Id;";

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
            logger.Info("DAL: Adding Log to Database");
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
            logger.Info("DAL: Finding log with ID: " + logId);

            DbCommand findCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findCommand, "@Id", DbType.Int32, logId);

            IEnumerable<LogItem> logItemsList = QueryLogsFromDB(findCommand);
            return logItemsList.FirstOrDefault();
        }

        public IEnumerable<LogItem> GetLogForTourItem(TourItem tour)
        {
            logger.Info("DAL: Finding logs with from Tour with ID: " + tour.Id);
            DbCommand getLogsCommand = database.CreateCommand(SQL_FIND_BY_TOUR_ID);
            database.DefineParameter(getLogsCommand, "@TourId", DbType.Int32, tour.Id);
            return QueryLogsFromDB(getLogsCommand);
        }

        private IEnumerable<LogItem> QueryLogsFromDB(DbCommand command)
        {
            logger.Info("DAL: getting all Logs ");
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

        public int DeleteTourLogs(TourItem tour)
        {
            logger.Info("DAL: Deleting logs from Tour with ID: " + tour.Id);
            DbCommand deleteLogsCommand = database.CreateCommand(SQL_DELETE_BY_TOUR_ID);
            database.DefineParameter(deleteLogsCommand, "@TourId", DbType.Int32, tour.Id);

            return database.ExecuteNonQuery(deleteLogsCommand);
            

        }

        public int DeleteLogItem(int logId)
        {
            logger.Info("DAL: Deleting log with ID: " + logId);
            DbCommand deleteLogsCommand = database.CreateCommand(SQL_DELETE_BY_LOG_ID);
            database.DefineParameter(deleteLogsCommand, "@LogId", DbType.Int32, logId);

            return database.ExecuteNonQuery(deleteLogsCommand);
        }

        public LogItem UpdateLogItem(int logId, string logName, double logDistance, int logTime, int logRating, int logSpeed, int logVerUp, int logVerDown, int logDiff, DateTime logDate, string logReport)
        {
            logger.Info("DAL: Updating log with ID: " + logId);
            DbCommand updateCommand = database.CreateCommand(SQL_UPDATE_LOG);

            database.DefineParameter(updateCommand, "@LogName", DbType.String, logName);
            database.DefineParameter(updateCommand, "@LogDistance", DbType.Double, logDistance);
            database.DefineParameter(updateCommand, "@LogTime", DbType.Int32, logTime);
            database.DefineParameter(updateCommand, "@LogRating", DbType.Int32, logRating);
            database.DefineParameter(updateCommand, "@LogSpeed", DbType.Int32, logSpeed);
            database.DefineParameter(updateCommand, "@LogVerUp", DbType.Int32, logVerUp);
            database.DefineParameter(updateCommand, "@LogVerDown", DbType.Int32, logVerDown);
            database.DefineParameter(updateCommand, "@LogDiff", DbType.Int32, logDiff);
            database.DefineParameter(updateCommand, "@LogDate", DbType.DateTime, logDate);
            database.DefineParameter(updateCommand, "@LogReport", DbType.String, logReport);
            database.DefineParameter(updateCommand, "@Id", DbType.Int32, logId);



            return FindById(database.ExecuteScalar(updateCommand));
        }
    }
}
