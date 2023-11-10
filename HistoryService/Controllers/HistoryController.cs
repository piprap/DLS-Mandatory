using AddService.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Monitoring;
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
        //Tracing
        using var activity = MonitorService.ActivitySource.StartActivity();
        //Log
        MonitorService.Log.Debug("Entered GetAdditions in HistoryController");

        var additionHistory = await historyCache.QueryAsync<History>("SELECT * FROM historylogs WHERE operation = 'addition'");

        foreach (var item in additionHistory)
        {
            //Console.WriteLine("inputone: " + item.Inputone + " - inputtwo: " + item.Inputtwo + " - output: " + item.Output);
            MonitorService.Log.Information(item.ToString());
        }

        MonitorService.Log.Debug($"Exiting GetAdditions in HistoryController AdditionHistoryCount: {additionHistory.Count()}");

        return Ok(additionHistory);
    }

    [HttpGet("/get/subtraction")]
    public async Task<ActionResult<List<History>>> GetSubtractions()
    {
        //Tracing
        using var activity = MonitorService.ActivitySource.StartActivity();
        //Log
        MonitorService.Log.Debug("Entered GetSubtractions in HistoryController");

        var subtractionHistory = await historyCache.QueryAsync<History>("SELECT * FROM historylogs WHERE operation = 'subtraction'");

        foreach (var item in subtractionHistory)
        {
            //Console.WriteLine("inputone: " + item.Inputone + " - inputtwo: " + item.inputtwo + " - output: " + item.output);
            MonitorService.Log.Information(item.ToString());
        }
        MonitorService.Log.Debug($"Exiting GetSubtractions in HistoryController SubtractionHistoryCount: {subtractionHistory.Count()}");
        return Ok(subtractionHistory);
    }

    [HttpPost("/post/addition")]
    public void SaveAddition([FromQuery] long inputone, [FromQuery] long inputtwo, [FromQuery] long output)
    {
        //Tracing
        using var activity = MonitorService.ActivitySource.StartActivity();
        //Log
        MonitorService.Log.Debug("Entered SaveAdditions in HistoryController");

        historyCache.ExecuteAsync("REPLACE INTO historylogs (inputone, inputtwo, output, operation) VALUES (@inputone, @inputtwo, @output, 'addition')", new { inputone = inputone, inputtwo = inputtwo, output = output, operation = "addition" });
        MonitorService.Log.Debug("Exiting SaveAdditions in HistoryController");
    }

    [HttpPost("/post/subtraction")]
    public void SaveSubtraction([FromQuery] long inputone, [FromQuery] long inputtwo, [FromQuery] long output)
    {
        //Tracing
        using var activity = MonitorService.ActivitySource.StartActivity();
        //Log
        MonitorService.Log.Debug("Entered SaveSubtraction in HistoryController");

        historyCache.ExecuteAsync("REPLACE INTO historylogs (inputone, inputtwo, output, operation) VALUES (@inputone, @inputtwo, @output, 'subtraction')", new { inputone = inputone, inputtwo = inputtwo, output = output, operation = "subtraction" });
        MonitorService.Log.Debug("Exiting SaveSubtraction in HistoryController");
    }

}