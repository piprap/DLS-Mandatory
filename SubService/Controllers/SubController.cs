using Microsoft.AspNetCore.Mvc;
using Monitoring;
using MySqlConnector;
using System.Data;

namespace SubService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubController : ControllerBase
    {
        
        [HttpPost]
        public long Post([FromQuery] long inputone, [FromQuery] long inputtwo)
        {
            //Tracing
            using var activity = MonitorService.ActivitySource.StartActivity();
            //Log

            MonitorService.Log.Debug($"Entered Post in SubController: Inputone: {inputone}, Inputtwo: {inputtwo}", inputone, inputtwo);
            //Lav beregning:
            long output = inputone - inputtwo;
            try 
            { 
                //Kald history service post:
                var client = new HttpClient();
                var baseAddress = "http://history-service/post/subtraction";
                var uri = new Uri($"{baseAddress}?inputone={inputone}&inputtwo={inputtwo}&output={output}");
                client.BaseAddress = uri;

                var x = client.SendAsync(new HttpRequestMessage(HttpMethod.Post, uri)).Result;
                MonitorService.Log.Debug("Exiting Post in SubController", output, x, x.ToString());
            }
            catch (Exception e)
            {
                MonitorService.Log.Debug("Exiting Post in SubController - History service failed to save entry!");
            }
            return output;

        }
    }
}