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
            historyCache.Execute("CREATE TABLE historylogs (inputone INT NOT NULL, inputtwo INT NOT NULL, output INT NOT NULL, operation STRING NOT NULL)");
        }
    }


    [HttpGet("/get/addition")]
    public IEnumerable<History> GetAdditions(long inputone, long inputtwo)
    {
        //return where opretaion = addition
        //lav 2 get endpoints 1 til sub og en add.
        var result = historyCache.QueryFirstOrDefault<History>("SELECT output FROM historylogs WHERE inputone = @inputone", new { inputone = inputone });
        
        return (IEnumerable<History>)result;

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
        historyCache.Execute("REPLACE INTO historylogs (inputone, inputtwo, output, operation) VALUES (@number, @divisors, @output, @operation)", new { inputone = inputone, inputtwo = inputtwo, output = output, operation = operation });
    }

    [HttpPost("/post/subtraction")]
    public void SaveSubtraction([FromQuery] long inputone, [FromQuery] long inputtwo, [FromQuery] long output)
    {
        //var output = inputone - inputtwo;
        string operation = "subtraction";
        historyCache.Execute("REPLACE INTO historylogs (inputone, inputtwo, output, operation) VALUES (@number, @divisors, @output, @operation)", new { inputone = inputone, inputtwo = inputtwo, output = output, operation = operation });
    }

}
