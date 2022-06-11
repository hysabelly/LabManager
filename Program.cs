using LabManager.Database;
using LabManager.Models;
using LabManager.Repositories;
using Microsoft.Data.Sqlite;



var databaseConfig = new DatabaseConfig();
var DatabaseSetup= new DatabaseSetup(databaseConfig);

var computerRepository = new ComputerRepository(databaseConfig);

// Roteamento
var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Computer List");
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processador);
        }
    }

    if(modelAction == "New")
    {
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open(); 

        Console.WriteLine("New computer");
        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processador = args[4];

        var computer = new Computer(id,ram,processador);
        computerRepository.Save(computer);
    }
    
    if(modelAction == "Show")
    {
        int id = Convert.ToInt32(args[2]);
        if (computerRepository.existsById(id))
        {
            var computer = computerRepository.GetById(id);
            Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processador);
        } 
        else
        {
            Console.WriteLine($"Computer com id {id} não existe");
        }
       
    }

    if(modelAction == "Delete")
    {
       int id = Convert.ToInt32(args[2]);
       computerRepository.Delete(id);
    }

    if(modelAction == "Update")
    {
        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processador = args[4];

        var computer = new Computer(id, ram, processador);
        computerRepository.Update(computer);   
    }
}

else if(modelName == "Lab")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Lab List");
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Labs;";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
        }

        reader.Close();
        connection.Close();
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var number = args[3];
        var name = args[4];
        var block = args[5];

        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Labs VALUES($id, $number, $name, $block);";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$number", number);
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$block", block);

        command.ExecuteNonQuery();
        connection.Close();
    }
}
