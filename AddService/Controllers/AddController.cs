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

    [HttpPost]
    public long Post([FromQuery] long inputone, [FromQuery] long inputtwo)
    {
        //Lav beregning:
        long output = inputone + inputtwo;

        //Kald history service post:
        var client = new HttpClient();
        var baseAddress = "http://history-service/post/addition";
        var uri = new Uri($"{baseAddress}?inputone={inputone}&inputtwo={inputtwo}&output={output}");
        client.BaseAddress = uri;

        var x = client.SendAsync(new HttpRequestMessage(HttpMethod.Post, uri)).Result;
        Console.WriteLine("TESTER ADDITION SERVICE - - - " + x.ToString());

 
        return output;
    }
}