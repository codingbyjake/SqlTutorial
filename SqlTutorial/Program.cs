
using Microsoft.Data.SqlClient;

string connectionString = "server=localhost\\sqlexpress;" +
                            "database=SalesDb;" +
                            "trusted_connection=true;" +
                            "trustServerCertificate=true;";

SqlConnection sqlConn = new SqlConnection(connectionString);

sqlConn.Open();

if(sqlConn.State != System.Data.ConnectionState.Open) {
    throw new Exception("I screwed up my connection string!");
}
Console.WriteLine("Connection opened successfully!");

string sql = "SELECT * from Customers WHERE sales > 90000 ORDER BY sales desc;";
//string sql = "SELECT * from Customers ORDER BY sales desc;";
//string sql = "SELECT * from Customers;";

SqlCommand cmd = new SqlCommand(sql, sqlConn);

SqlDataReader reader = cmd.ExecuteReader();

while (reader.Read()) {
    var id = Convert.ToInt32(reader["Id"]);
    var name = Convert.ToString(reader["Name"]);
    var city = Convert.ToString(reader["City"]);
    var state = Convert.ToString(reader["State"]);
    var sales = Convert.ToDecimal(reader["Sales"]);
    var active = Convert.ToBoolean(reader["Active"]);
    Console.WriteLine($"{id} | {name} | {city} | {state} | {sales:C} | {(active ? "YES" : "NO")}");
}

reader.Close();

string sql2 = "SELECT * from Orders;";

SqlCommand cmd2 = new SqlCommand(sql2, sqlConn);

SqlDataReader reader2 = cmd2.ExecuteReader();

while (reader2.Read()) {
    var id = Convert.ToInt32(reader2["Id"]);
    var customerId = (reader2["CustomerId"].Equals(System.DBNull.Value))
        ? (int?)null
        : Convert.ToInt32(reader["CustomerId"]);
    var date = Convert.ToDateTime(reader2["Date"]);
    var description = Convert.ToString(reader2["Description"]);

    Console.WriteLine($"{id} | {customerId} | {date: MMMM, yyyy} | {description}");
}
reader2.Close();

sqlConn.Close();