using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;

namespace LabManager.Repositories;

class ComputerRepository
{
    private DatabaseConfig  databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig=databaseConfig;
    }

    public List<Computer> GetAll()
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();  
        
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers;";

        var reader = command.ExecuteReader();
        
        var computers = new List<Computer>(); 

        while(reader.Read())
        {
            computers.Add(readerToComputer(reader));
        }

        connection.Close();
        return computers;
    }
    
    public Computer Save(Computer computer)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processador);";
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processador", computer.Processador);
        command.ExecuteNonQuery();
        
        connection.Close();
        reader.Close();
        return computer;
    }
    
    public Computer GetById(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers WHERE Id=$id;";
        command.Parameters.AddWithValue("$id", id);

        var reader = command.ExecuteReader();

        reader.Read();
        
        var computer = readerToComputer(reader);
        Console.WriteLine("{0},{1},{2}", computer.Id, computer.Ram, computer.Processador);
        
        connection.Close();
        return computer;
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Computers WHERE Id=$id;";
        command.Parameters.AddWithValue("$id", id);

        command.ExecuteNonQuery();
        connection.Close();
    }

     public Computer Update(Computer computer)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();;

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE Computers SET Ram = $ram, Processador = $processador WHERE Id = $id;";
        command.Parameters.AddWithValue("$id", computer.Id);
        command.Parameters.AddWithValue("$ram", computer.Ram);
        command.Parameters.AddWithValue("$processador", computer.Processador);
        
        command.ExecuteNonQuery();
        connection.Close();
        
        return computer;
    }

    public bool existsById (int id)
    {
 
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM Computers WHERE id=$id;";
        command.Parameters.AddWithValue("$id", id);

        bool result = Convert.ToBoolean(command.ExecuteScalar());

        return result; 
    }
    
  private Computer readerToComputer(SqliteDataReader reader)
    {
        var computer = new Computer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));

        return computer;
    }


}
