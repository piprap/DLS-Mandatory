using Microsoft.AspNetCore.Mvc;
using Monitoring;

namespace ApiGatewayService.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ApiGatewayController : ControllerBase
    {

        public ApiGatewayController()
        {

        }

        [HttpPost("/post/addition")]
        public long PostAddition([FromQuery] long inputone, [FromQuery] long inputtwo)
        {
            using var activity = MonitorService.ActivitySource.StartActivity();
            MonitorService.Log.Debug($"Entered PostAddition in ApiGatewayController");

            //Kald history service post:
            using (var client = new HttpClient())
            {
                var baseAddress = "http://add-service/Add";
                var uri = new Uri($"{baseAddress}?inputone={inputone}&inputtwo={inputtwo}");

                var response = client.PostAsync(uri, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (long.TryParse(result, out long output))
                    {
                       // Console.WriteLine("output from response: " + output);
                        MonitorService.Log.Information("output from response: " + output);
                        return output;
                    }
                    else
                    {
                        MonitorService.Log.Error("Failed to parse response content as long.");
                       // Console.WriteLine("Failed to parse response content as long.");
                    }
                }
                else
                {
                    MonitorService.Log.Error($"API call failed with status code: {response.StatusCode}");
                   // Console.WriteLine($"API call failed with status code: {response.StatusCode}");
                }
                MonitorService.Log.Debug($"Exiting PostAddition in ApiGatewayController");

                // Return a default value or throw an exception based on your requirements.
                return 0;
            }
        }

        [HttpPost("/post/subtraction")]
        public long PostSubtraction([FromQuery] long inputone, [FromQuery] long inputtwo)
        {
            using var activity = MonitorService.ActivitySource.StartActivity();
            MonitorService.Log.Debug($"Entered PostSubtraction in ApiGatewayController");
            //Kald history service post:
            using (var client = new HttpClient())
            {
                var baseAddress = "http://sub-service/Sub";
                var uri = new Uri($"{baseAddress}?inputone={inputone}&inputtwo={inputtwo}");

                var response = client.PostAsync(uri, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (long.TryParse(result, out long output))
                    {
                        MonitorService.Log.Information("output from response: " + output);
                        //Console.WriteLine("output from response: " + output);
                        return output;
                    }
                    else
                    {
                        MonitorService.Log.Error("Failed to parse response content as long.");
                        //Console.WriteLine("Failed to parse response content as long.");
                    }
                }
                else
                {
                    MonitorService.Log.Error($"API call failed with status code: {response.StatusCode}");
                    //Console.WriteLine($"API call failed with status code: {response.StatusCode}");
                }
                MonitorService.Log.Debug($"Exiting PostSubtraction in ApiGatewayController");
                // Return a default value or throw an exception based on your requirements.
                return 0;
            }
        }

        [HttpPost("/post/multiplication")]
        public long PostMultiplication([FromQuery] long inputone, [FromQuery] long inputtwo)
        {
            if (FeatureHub.FeatureFlag.MultiplicationFeatureIsEnabled)
            {

                using var activity = MonitorService.ActivitySource.StartActivity();
                MonitorService.Log.Debug($"Entered PostMultiplication in ApiGatewayController");

                //Kald history service post:
                using (var client = new HttpClient())
                {
                    var baseAddress = "http://multi-service/multiplication";
                    var uri = new Uri($"{baseAddress}?inputone={inputone}&inputtwo={inputtwo}");

                    var response = client.PostAsync(uri, null).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        if (long.TryParse(result, out long output))
                        {
                            // Console.WriteLine("output from response: " + output);
                            MonitorService.Log.Information("output from response: " + output);
                            return output;
                        }
                        else
                        {
                            MonitorService.Log.Error("Failed to parse response content as long.");
                            // Console.WriteLine("Failed to parse response content as long.");
                        }
                    }
                    else
                    {
                        MonitorService.Log.Error($"API call failed with status code: {response.StatusCode}");
                        // Console.WriteLine($"API call failed with status code: {response.StatusCode}");
                    }
                    MonitorService.Log.Debug($"Exiting PostMultiplication in ApiGatewayController");

                    // Return a default value or throw an exception based on your requirements.
                    return 0;
                }
            }
            else
            {
                BadRequest();
                return 0;
            }

        }

    }
}