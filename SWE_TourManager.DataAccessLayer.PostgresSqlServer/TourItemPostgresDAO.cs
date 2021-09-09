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
        private IDatabase database;

        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"Tours\" WHERE \"TourId\"= @Id;";
        private const string SQL_GET_ALL_TOURS = "SELECT * FROM public.\"Tours\";";
        private const string SQL_INSERT_NEW_TOUR = "INSERT INTO public.\"Tours\" (\"TourName\", \"TourDescription\", \"TourDistance\", \"TourStart\" , \"TourDestination\" , \"TourImgPath\") VALUES (@Name, @Description, @Distance, @Start, @Destination, @ImgPath) RETURNING \"TourId\";";

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
            DbCommand findCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findCommand, "@Id", DbType.Int32, tourId);

            IEnumerable<TourItem> tourItems = QueryTourItemsFromDatabase(findCommand);
            return tourItems.FirstOrDefault();
        }

        public IEnumerable<TourItem> GetTourItems()
        {
            DbCommand tourCommand = database.CreateCommand(SQL_GET_ALL_TOURS);

            
            return QueryTourItemsFromDatabase(tourCommand);
        }

        private IEnumerable<TourItem> QueryTourItemsFromDatabase(DbCommand command)
        {
            List<TourItem> tourItemList = new List<TourItem>();

            using (IDataReader reader = database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    tourItemList.Add(new TourItem((int)reader["TourId"], (string)reader["TourName"], (string)reader["TourDescription"],  (double)reader["TourDistance"], (string)reader["TourImgPath"]));
                }
            }

            return tourItemList;
        }
    }
}
