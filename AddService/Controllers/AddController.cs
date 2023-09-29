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

    public AddController(){
       
    }

    [HttpGet]
    public long Get(long inputone, long inputtwo)
    {
        var result = inputone + inputtwo;

        //Kald HistoryService API post

        return result;
        //return historyCache.QueryFirstOrDefault<int>("SELECT output FROM historylogs WHERE inputone = @inputone", new { inputone = inputone });

    }

    [HttpPost]
    public void Post([FromQuery] long inputone, [FromQuery] long inputtwo)
    {
        var output = inputone + inputtwo;
        //historyCache.Execute("REPLACE INTO historylogs (inputone, inputtwo, output) VALUES (@number, @divisors, @output)", new { inputone = inputone, inputtwo = inputtwo, output  = output });
    }
}