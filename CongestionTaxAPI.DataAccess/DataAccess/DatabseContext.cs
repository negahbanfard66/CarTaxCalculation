using CongestionTaxAPI.DTO;
using Microsoft.Data.Sqlite;

namespace CongestionTaxAPI.DataAccess
{
    public class DatabaseContext : IDatabaseContext
    {
        private const string ConnectionString = "Data Source=./congestion_tax.db";
        private readonly SqliteConnection _connection;

        public DatabaseContext()
        {
            _connection = new SqliteConnection(ConnectionString);
            _connection.Open();
        }

        public void Initialize()
        {
            var command = _connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS tax_rules (
                    id INTEGER PRIMARY KEY,
                    city TEXT,
                    start_time TEXT,
                    end_time TEXT,
                    amount INTEGER
                );

                CREATE TABLE IF NOT EXISTS exempt_vehicles (
                    id INTEGER PRIMARY KEY,
                    vehicle_type TEXT
                );

                CREATE TABLE IF NOT EXISTS holiday_rules (
                    id INTEGER PRIMARY KEY,
                    date TEXT,
                    city TEXT
                );

                CREATE TABLE IF NOT EXISTS city_config (
                    id INTEGER PRIMARY KEY,
                    city TEXT,
                    max_daily_charge INTEGER
                );
            ";
            command.ExecuteNonQuery();
        }

        public void SeedData(string city, List<TaxRules> taxRules, int maxDailyCharge)
        {
            var command = _connection.CreateCommand();
            command.CommandText = @$"
                INSERT INTO city_config (city, max_daily_charge) VALUES ('{city}', '{maxDailyCharge}')";
            command.ExecuteNonQuery();

            foreach (var item in taxRules)
            {
                var command2 = _connection.CreateCommand();
                command2.CommandText =
                @$"INSERT INTO tax_rules(city, start_time, end_time, amount) VALUES
                ('{item.City}', '{item.StartTime}', '{item.EndTime}', {item.Amount})";
                command2.ExecuteNonQuery();
            }

            var command3 = _connection.CreateCommand();
            command3.CommandText =
                 @$"INSERT INTO exempt_vehicles(vehicle_type) VALUES
            ('Emergency'), ('Bus'), ('Diplomat'), ('Motorcycle'), ('Military'), ('Foreign');";
            command3.ExecuteNonQuery();
        }

        public List<Tuple<string, string, int>> GetTaxRules(string city)
        {
            var taxRules = new List<Tuple<string, string, int>>();
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT start_time, end_time, amount FROM tax_rules WHERE city = @city";
            command.Parameters.AddWithValue("@city", city);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var startTime = reader.GetString(0);
                    var endTime = reader.GetString(1);
                    var amount = reader.GetInt32(2);
                    taxRules.Add(new Tuple<string, string, int>(startTime, endTime, amount));
                }
            }
            return taxRules;
        }

        public int GetMaxDailyCharge(string city)
        {
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT max_daily_charge FROM city_config WHERE city = @city";
            command.Parameters.AddWithValue("@city", city);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public bool IsVehicleExempt(string vehicleType)
        {
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT 1 FROM exempt_vehicles WHERE vehicle_type = @vehicleType";
            command.Parameters.AddWithValue("@vehicleType", vehicleType);
            return command.ExecuteScalar() != null;
        }

        public bool IsHoliday(string date, string city)
        {
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT 1 FROM holiday_rules WHERE date = @date AND city = @city";
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@city", city);
            return command.ExecuteScalar() != null;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
