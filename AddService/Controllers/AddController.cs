using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AddService.Controllers;

[ApiController]
[Route("[controller]")]
public class AddController : ControllerBase
{
    private IDbConnection historyCache = new MySqlConnection("Server=history-db;Database=history-database;Uid=historydb;Pwd=C@ch3d1v;");

    public AddController(){
        historyCache.Open();
        var tables = historyCache.Query<string>("SHOW TABLES LIKE 'historylogs'");
        if (!tables.Any())
        {
            historyCache.Execute("CREATE TABLE historylogs (inputone INT NOT NULL, inputtwo INT NOT NULL, output INT NOT NULL)");
        }
    }

    [HttpGet]
    public long Get(long inputone, long inputtwo)
    {
        //return num1 + num2;
        return historyCache.QueryFirstOrDefault<int>("SELECT output FROM historylogs WHERE inputone = @inputone", new { inputone = inputone });

    }

    [HttpPost]
    public void Post([FromQuery] long inputone, [FromQuery] long inputtwo)
    {
        var output = inputone + inputtwo;
        historyCache.Execute("REPLACE INTO historylogs (inputone, inputtwo, output) VALUES (@number, @divisors, @output)", new { inputone = inputone, inputtwo = inputtwo, output  = output });
    }
}