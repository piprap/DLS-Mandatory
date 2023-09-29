using AddService.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;

namespace HistoryService.Controllers;

[ApiController]
[Route("[controller]")]
public class HistoryController : ControllerBase
{
    private IDbConnection historyCache = new MySqlConnection("Server=history-db;Database=history-database;Uid=historydb;Pwd=C@ch3d1v;");

    public HistoryController() {
        historyCache.Open();
        var tables = historyCache.Query<string>("SHOW TABLES LIKE 'historylogs'");
        if (!tables.Any())
        {
            historyCache.Execute("CREATE TABLE historylogs (id INT NOT NULL AUTO_INCREMENT, inputone INT NOT NULL, inputtwo INT NOT NULL, output INT NOT NULL, operation VARCHAR(50) NOT NULL, PRIMARY KEY (id))");
        }
    }


    [HttpGet("/get/addition")]
    public IEnumerable<History> GetAdditions()
    {
        //return where opretaion = addition
        //lav 2 get endpoints 1 til sub og en add.
        //var result = historyCache.QueryFirstOrDefault<History>("SELECT * FROM historylogs WHERE inputone = @inputone", new { inputone = inputone });
        var result1 =  historyCache.Query<IEnumerable<History>>("SELECT inputone, inputtwo, output FROM historylogs WHERE operation = 'addition'");
        Console.WriteLine("last: " + result1.Last());//.ToString());
       // Console.WriteLine((IEnumerable<History>)result1.ToList());
        foreach (var x in result1) { Console.WriteLine(x); }
        
        Console.WriteLine("TESTER CONSOLE");
        return (IEnumerable<History>)result1;

    }

    [HttpGet("/get/subtraction")]
    public long GetSubtractions(long inputone, long inputtwo)
    {
        //return where opretaion = addition
        //lav 2 get endpoints 1 til sub og en add.
        return historyCache.QueryFirstOrDefault<int>("SELECT output FROM historylogs WHERE inputone = @inputone", new { inputone = inputone });

    }

    [HttpPost("/post/addition")]
    public void SaveAddition([FromQuery] long inputone, [FromQuery] long inputtwo, [FromQuery] long output)
    {
        //var output = inputone + inputtwo;
        string operation = "addition";
       // string id = inputone + "_" + inputtwo + "_addition";
        historyCache.Execute("REPLACE INTO historylogs (inputone, inputtwo, output, operation) VALUES (@inputone, @inputtwo, @output, @operation)", new { inputone = inputone, inputtwo = inputtwo, output = output, operation = operation });
    }

    [HttpPost("/post/subtraction")]
    public void SaveSubtraction([FromQuery] long inputone, [FromQuery] long inputtwo, [FromQuery] long output)
    {
        //var output = inputone - inputtwo;
        string operation = "subtraction";
        historyCache.Execute("REPLACE INTO historylogs (inputone, inputtwo, output, operation) VALUES (@number, @divisors, @output, @operation)", new { inputone = inputone, inputtwo = inputtwo, output = output, operation = operation });
    }

}
