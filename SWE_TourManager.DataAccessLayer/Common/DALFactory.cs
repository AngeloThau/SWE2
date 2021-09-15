using SWE_TourManager.DataAccessLayer.DAO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.DataAccessLayer.Common
{
    public class DALFactory
    {
        private static string assemblyName;
        private static Assembly dalAssembly;
        private static IDatabase database;
        static DALFactory()
        {
            assemblyName = ConfigurationManager.AppSettings["DALSqlAssembly"];
            dalAssembly = Assembly.Load(assemblyName);
        }

        public static IDatabase GetDatabase()
        {
            if (database == null)
            {
                database = CreateDatabase();
            }
            return database;
        }

        private static IDatabase CreateDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PostgresSqlConnectionString"].ConnectionString;
            return CreateDatabase(connectionString);
        }

        private static IDatabase CreateDatabase(string connectionString)
        {
            string databaseClassName = assemblyName + ".Database";
            Type dbClass = dalAssembly.GetType(databaseClassName);

            return Activator.CreateInstance(dbClass, new object[] { connectionString }) as IDatabase;
        }

        public static ITourDAO CreateTourDAO()
        {
            string className = assemblyName + ".TourItemPostgresDAO";
            Type tourItemType = dalAssembly.GetType(className);

            return Activator.CreateInstance(tourItemType) as ITourDAO;
        }

        public static ILogDAO CreateLogDAO()
        {
            string className = assemblyName + ".LogItemPostgresDAO";
            Type logItemType = dalAssembly.GetType(className);

            return Activator.CreateInstance(logItemType) as ILogDAO;
        }

    }
}
