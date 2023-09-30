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
            historyCache.Execute("CREATE TABLE historylogs (id INT NOT NULL AUTO_INCREMENT,timestamp DATETIME DEFAULT CURRENT_TIMESTAMP, inputone INT NOT NULL, inputtwo INT NOT NULL, output INT NOT NULL, operation VARCHAR(50) NOT NULL, PRIMARY KEY (id))");
        }
    }


    [HttpGet("/get/addition")]
    public IEnumerable<History> GetAdditions()
    {
        
        var additionHistory = historyCache.Query<History>("SELECT * FROM historylogs WHERE operation = 'addition'");

        foreach (var item in additionHistory) 
        { 
            Console.WriteLine("inputone: " + item.inputone + " - inputtwo: " + item.inputtwo + " - output: " + item.output);
        
        }

        return additionHistory;

    }

    [HttpGet("/get/subtraction")]
    public IEnumerable<History> GetSubtractions()
    {
        var subtractionHistory = historyCache.Query<History>("SELECT * FROM historylogs WHERE operation = 'subtraction'");

        foreach (var item in subtractionHistory)
        {
            Console.WriteLine("inputone: " + item.inputone + " - inputtwo: " + item.inputtwo + " - output: " + item.output);

        }

        return subtractionHistory;
    }

    [HttpPost("/post/addition")]
    public void SaveAddition([FromQuery] long inputone, [FromQuery] long inputtwo, [FromQuery] long output)
    {
        //var output = inputone + inputtwo;
        string operation = "addition";
        // string id = inputone + "_" + inputtwo + "_addition";
        historyCache.Execute("REPLACE INTO historylogs (inputone, inputtwo, output, operation) VALUES (@inputone, @inputtwo, @output, 'addition')", new { inputone = inputone, inputtwo = inputtwo, output = output, operation = operation });
        
    }

    [HttpPost("/post/subtraction")]
    public void SaveSubtraction([FromQuery] long inputone, [FromQuery] long inputtwo, [FromQuery] long output)
    {
        //var output = inputone - inputtwo;
        string operation = "subtraction";
        historyCache.Execute("REPLACE INTO historylogs (inputone, inputtwo, output, operation) VALUES (@inputone, @inputtwo, @output, 'subtraction')", new { inputone = inputone, inputtwo = inputtwo, output = output, operation = operation });
    }

}
    