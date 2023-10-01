using AddService.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Data;

namespace HistoryService.Controllers;

[ApiController]
[Route("[controller]")]
public class HistoryController : ControllerBase
{
    private IDbConnection historyCache = new MySqlConnection("Server=history-db;Database=history-database;Uid=historydb;Pwd=C@ch3d1v;");

    public HistoryController()
    {
        historyCache.Open();
        var tables = historyCache.Query<string>("SHOW TABLES LIKE 'historylogs'");
        if (!tables.Any())
        {
            historyCache.Execute("CREATE TABLE historylogs (id INT NOT NULL AUTO_INCREMENT,timestamp DATETIME DEFAULT CURRENT_TIMESTAMP, inputone INT NOT NULL, inputtwo INT NOT NULL, output INT NOT NULL, operation VARCHAR(50) NOT NULL, PRIMARY KEY (id))");
        }
    }


    [HttpGet("/get/addition")]
    public async Task<ActionResult<List<History>>> GetAdditions()
    {

        var additionHistory = await historyCache.QueryAsync<History>("SELECT * FROM historylogs WHERE operation = 'addition'");

        foreach (var item in additionHistory)
        {
            Console.WriteLine("inputone: " + item.inputone + " - inputtwo: " + item.inputtwo + " - output: " + item.output);

        }

        //var JsonConv =
        //  new Response(additionHistory, 200);
        return Ok(additionHistory);
    }

    [HttpGet("/get/subtraction")]
    public IActionResult GetSubtractions()
    {
        var subtractionHistory = historyCache.Query<History>("SELECT * FROM historylogs WHERE operation = 'subtraction'");

        foreach (var item in subtractionHistory)
        {
            Console.WriteLine("inputone: " + item.inputone + " - inputtwo: " + item.inputtwo + " - output: " + item.output);

        }

        return Ok(subtractionHistory);
    }

    [HttpPost("/post/addition")]
    public void SaveAddition([FromQuery] long inputone, [FromQuery] long inputtwo, [FromQuery] long output)
    {
        historyCache.Execute("REPLACE INTO historylogs (inputone, inputtwo, output, operation) VALUES (@inputone, @inputtwo, @output, 'addition')", new { inputone = inputone, inputtwo = inputtwo, output = output, operation = "addition" });

    }

    [HttpPost("/post/subtraction")]
    public void SaveSubtraction([FromQuery] long inputone, [FromQuery] long inputtwo, [FromQuery] long output)
    {
        historyCache.Execute("REPLACE INTO historylogs (inputone, inputtwo, output, operation) VALUES (@inputone, @inputtwo, @output, 'subtraction')", new { inputone = inputone, inputtwo = inputtwo, output = output, operation = "subtraction" });
    }

}