using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;


namespace VehicleService
{
    //SQlite Vehicle Database Access
    public class SqliteDataAccess
    {
        //GET all vehicles
        public static List<Vehicle> GetAllVehicles()
        {
            try
            {
                //Close connection upon program finish or crash
                using (IDbConnection Connection = new SQLiteConnection(LoadConnectionString()))
                {
                    //QUERY: Select all vehicles from database
                    var AllVehicles = Connection.Query<Vehicle>("SELECT * FROM Vehicle", new DynamicParameters());

                    return AllVehicles.ToList();
                }
            }
            catch (Exception)
            {

                throw; //throw excecption to nearest try-catch up the stack
            }
        }

        //GET vehicles by ID
        public static List<Vehicle> GetVehiclesById(int VehicleId)
        {
            string SqlQuery = "SELECT * FROM Vehicle WHERE Id=@Id;";

            using (IDbConnection Connection = new SQLiteConnection(LoadConnectionString()))
            {
                //QUERY: Select  vehicle by Id from database
                var VehicleById = Connection.Query<Vehicle>(SqlQuery, new {Id = VehicleId});

                //If the query returns nothing, then that entity does not exist in the database, throw exception
                if (!VehicleById.Any())
                {                  
                    //bubble up the stack with an exception to nearest try-catch
                    throw new Exception("Vehicle with that ID does not exist in the database");
                }

                return VehicleById.ToList();
            }
        }

        //GET vehicles by FILTERING search options
        public static List<Vehicle> FilterVehicles(Vehicle vehicle)
        {
           //Start building SQL query based on filters
            string SqlQuery = "SELECT * FROM Vehicle WHERE 1=1";

            //Check ID filter
            if ( vehicle.Id != 0)
            {
                SqlQuery = SqlQuery + " AND Id=@Id";
            }

            //Check Year filter
            if ( vehicle.Year >= 1950 && vehicle.Year <= 2050)
            {
                SqlQuery = SqlQuery + " AND Year=@Year";
            }

            //Check Make filter
            if (!string.IsNullOrEmpty(vehicle.Make))
            {
                SqlQuery = SqlQuery + " AND Make=@Make";
            }

            //Check Model filter
            if(!string.IsNullOrEmpty(vehicle.Model))
            {
                SqlQuery = SqlQuery + " AND Model=@Model";
            }

            SqlQuery = SqlQuery + ";";

            try
            {
                using (IDbConnection Connection = new SQLiteConnection(LoadConnectionString()))
                {
                    //First check if that vehicle exists in the database by ID
                    var ListOfVehicles = Connection.Query<Vehicle>(SqlQuery, new { Id = vehicle.Id, Year = vehicle.Year, Make = vehicle.Make, Model = vehicle.Model });

                    //If the query returns nothing, then that entity does not exist in the database, throw exception
                    if (!ListOfVehicles.Any())
                    {
                        //bubble up the stack with an exception to nearest try-catch
                        throw new Exception("Vehicle with that ID does not exist in the database");
                    }
              
                    return ListOfVehicles.ToList();
                }
            }
            catch (Exception)
            {
                throw; //throw excecption to nearest try-catch up the stack
            }

        }

        //CREATE vehicles
        public static void CreateVehicles(Vehicle vehicle)
        {
            try
            {
                using (IDbConnection Connection = new SQLiteConnection(LoadConnectionString()))
                {
                    //QUERY: Insert a vehicle into database
                    Connection.Execute("insert into Vehicle (Id, Year, Make, Model) values (@Id, @Year, @Make, @Model)", vehicle);
                }
            }
            catch (Exception)
            {
                throw; //throw excecption to nearest try-catch up the stack
            }
        }

        //UPDATE vehicles
        public static void UpdateVehicles(Vehicle vehicle)
        {
            int VehicleId = vehicle.Id;
            string SqlQuery = "SELECT * FROM Vehicle WHERE Id=@Id;";

            try
            {
                using (IDbConnection Connection = new SQLiteConnection(LoadConnectionString()))
                {
                    //First check if that vehicle exists in the database by ID
                    var VehicleById = Connection.Query<Vehicle>(SqlQuery, new { Id = VehicleId });
                   
                    //If the query returns nothing, then that entity does not exist in the database, throw exception
                    if (!VehicleById.Any())
                    {
                        //bubble up the stack with an exception to nearest try-catch
                        throw new Exception("Vehicle with that ID does not exist in the database");
                    }
                    else //UPDATE
                    {
                        // Update a vehicle in the database
                        Connection.Execute("UPDATE Vehicle SET Id = @Id, Year = @Year, Make = @Make, Model = @Model WHERE Id = @Id", vehicle);
                    }
                }
            }
            catch (Exception)
            {
                throw; //throw excecption to nearest try-catch up the stack
            }
        }

        //DELETE vehicles by ID
        public static void DeleteVehiclesById(int VehicleId)
        {
            try
            {
                string SqlQuery = "DELETE FROM Vehicle WHERE Id=@Id;";

                using (IDbConnection Connection = new SQLiteConnection(LoadConnectionString()))
                {
                    //QUERY: Select  vehicle by Id from database
                    Connection.Execute(SqlQuery, new { Id = VehicleId });
                }
            }
            catch (Exception)
            {

                throw; //throw excecption to nearest try-catch up the stack
            }
        }

        //Find and load default connection string to SQLite vehicle database
        private static string LoadConnectionString(string ConnectionStringId = "Default")
        {
            return ConfigurationManager.ConnectionStrings[ConnectionStringId].ConnectionString;
        }
    }
}
