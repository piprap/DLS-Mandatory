using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Monitoring;
using System.Reflection;

namespace MultiplicationService.Controllers;

[ApiController]
[Route("[controller]")]
public class MultiplicationController : ControllerBase
{
    [HttpPost]
    public long Post([FromQuery] long inputone, [FromQuery] long inputtwo)
    {
        //Tracing
        using var activity = MonitorService.ActivitySource.StartActivity();
        //Log

        MonitorService.Log.Debug($"Entered Post in MultiplicationController: Inputone: {inputone}, Inputtwo: {inputtwo}", inputone, inputtwo);
        //Lav beregning:
        long output = inputone * inputtwo;
        try
        {
            //Kald history service post:
            var client = new HttpClient();
            var baseAddress = "http://history-service/post/multiplication";
            var uri = new Uri($"{baseAddress}?inputone={inputone}&inputtwo={inputtwo}&output={output}");
            client.BaseAddress = uri;

            var x = client.SendAsync(new HttpRequestMessage(HttpMethod.Post, uri)).Result;
            //Log
            MonitorService.Log.Debug("Exiting Post in MultiplicationController", output, x, x.ToString());
        }
        catch (Exception e)
        {
            MonitorService.Log.Debug("Exiting Post in MultiplicationController - History service failed to save entry!");
            MonitorService.Log.Debug(e.Message);
        }
        return output;
    }
}