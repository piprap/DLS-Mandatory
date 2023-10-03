using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Monitoring;

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
        //Log
        MonitorService.Log.Debug($"Entered Post in AddController: Inputone: {inputone}, Inputtwo: {inputtwo}", inputone, inputtwo);
        //Lav beregning:
        long output = inputone + inputtwo;
        try
        {
             //Kald history service post:
            var client = new HttpClient();
            var baseAddress = "http://history-service/post/addition";
            var uri = new Uri($"{baseAddress}?inputone={inputone}&inputtwo={inputtwo}&output={output}");
            client.BaseAddress = uri;

            var x = client.SendAsync(new HttpRequestMessage(HttpMethod.Post, uri)).Result;
            //Log
            MonitorService.Log.Debug("Exiting Post in AddController", output, x, x.ToString());
        }
        catch (Exception e)
        {
            MonitorService.Log.Debug("Exiting Post in AddController - History service failed to save entry!");

        }


        return output;
    }
}