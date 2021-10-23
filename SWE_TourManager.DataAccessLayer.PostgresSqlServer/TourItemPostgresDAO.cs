using SWE_TourManager.DataAccessLayer.Common;
using SWE_TourManager.DataAccessLayer.DAO;
using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.DataAccessLayer.PostgresSqlServer
{
    public class TourItemPostgresDAO : ITourDAO
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IDatabase database;

        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"Tours\" WHERE \"TourId\"= @Id;";
        private const string SQL_GET_ALL_TOURS = "SELECT * FROM public.\"Tours\";";
        private const string SQL_INSERT_NEW_TOUR = "INSERT INTO public.\"Tours\" (\"TourName\", \"TourDescription\", \"TourDistance\", \"TourStart\" , \"TourDestination\" , \"TourImgPath\") VALUES (@Name, @Description, @Distance, @Start, @Destination, @ImgPath) RETURNING \"TourId\";";
        private const string SQL_UPDATE_TOUR = "UPDATE public.\"Tours\" SET \"TourName\"=@Name, \"TourDescription\"=@Description WHERE \"TourId\"= @Id;";
        private const string SQL_DELETE = "DELETE FROM public.\"Tours\" WHERE \"TourId\"= @Id;";

        public TourItemPostgresDAO()
        {
            this.database = DALFactory.GetDatabase();
        }

        public TourItemPostgresDAO(IDatabase database)
        {
            this.database = database;
        }

        public TourItem AddNewItem(string name, string description, double distance, string start, string destination, string imgPath)
        {
            logger.Info("DAL: Adding new Tour");
            DbCommand instertCommand = database.CreateCommand(SQL_INSERT_NEW_TOUR);
            database.DefineParameter(instertCommand, "@Name", DbType.String, name);
            database.DefineParameter(instertCommand, "@Description", DbType.String, description);
            database.DefineParameter(instertCommand, "@Distance", DbType.Double, distance);
            database.DefineParameter(instertCommand, "@Start", DbType.String, start);
            database.DefineParameter(instertCommand, "@Destination", DbType.String, destination);
            database.DefineParameter(instertCommand, "@ImgPath", DbType.String, imgPath);


            return FindById(database.ExecuteScalar(instertCommand));
        }

        public TourItem FindById(int tourId)
        {
            logger.Info("DAL: Finding Tour with ID: " + tourId);
            DbCommand findCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findCommand, "@Id", DbType.Int32, tourId);

            IEnumerable<TourItem> tourItems = QueryTourItemsFromDatabase(findCommand);
            return tourItems.FirstOrDefault();
        }

        public IEnumerable<TourItem> GetTourItems()
        {
            logger.Info("DAL: Getting all Tours");
            DbCommand tourCommand = database.CreateCommand(SQL_GET_ALL_TOURS);

            
            return QueryTourItemsFromDatabase(tourCommand);
        }

        private IEnumerable<TourItem> QueryTourItemsFromDatabase(DbCommand command)
        {
            logger.Info("DAL: Returning Tours as IEnumerable ");
            List<TourItem> tourItemList = new List<TourItem>();

            using (IDataReader reader = database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    tourItemList.Add(new TourItem((int)reader["TourId"], (string)reader["TourName"], (string)reader["TourDescription"], (double)reader["TourDistance"], (string)reader["TourStart"], (string)reader["TourDestination"],  (string)reader["TourImgPath"]));
                }
            }

            return tourItemList;
        }

        public TourItem ModifyTourItem(string name, string description, int id)
        {
            logger.Info("DAL: Modifying Tour with ID: " + id);

            DbCommand updateCommand = database.CreateCommand(SQL_UPDATE_TOUR);
            
            database.DefineParameter(updateCommand, "@Name", DbType.String, name);
            database.DefineParameter(updateCommand, "@Description", DbType.String, description);            
            database.DefineParameter(updateCommand, "@Id", DbType.Int32, id);


            return FindById(database.ExecuteScalar(updateCommand));
        }

        public int DeleteTourItem(int tourId)
        {
            logger.Info("DAL: deleting Tour with ID: " + tourId);

            DbCommand deleteCommand = database.CreateCommand(SQL_DELETE);
            database.DefineParameter(deleteCommand, "@Id", DbType.Int32, tourId);

            return database.ExecuteNonQuery(deleteCommand);            
        }
    }
}
