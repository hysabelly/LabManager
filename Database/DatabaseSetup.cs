using Microsoft.Data.Sqlite;

namespace LabManager.Database;

class DatabaseSetup
{
    private DatabaseConfig databaseConfig; 

    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        this.databaseConfig=databaseConfig;
        CreateComputerTable();
    }
    public void CreateComputerTable()
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Computers(
                id int not null primary key, 
                ram varchar(100) not null,
                processador varchar(100) not null
            );
        ";
        command.ExecuteNonQuery();

        connection.Close();
    }
}
