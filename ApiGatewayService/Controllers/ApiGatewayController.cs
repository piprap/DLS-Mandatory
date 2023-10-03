using Microsoft.AspNetCore.Mvc;

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
                        Console.WriteLine("output from response: " + output);
                        return output;
                    }
                    else
                    {
                        Console.WriteLine("Failed to parse response content as long.");
                    }
                }
                else
                {
                    Console.WriteLine($"API call failed with status code: {response.StatusCode}");
                }

                // Return a default value or throw an exception based on your requirements.
                return 0;
            }
        }

        [HttpPost("/post/subtraction")]
        public long PostSubtraction([FromQuery] long inputone, [FromQuery] long inputtwo)
        {
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
                        Console.WriteLine("output from response: " + output);
                        return output;
                    }
                    else
                    {
                        Console.WriteLine("Failed to parse response content as long.");
                    }
                }
                else
                {
                    Console.WriteLine($"API call failed with status code: {response.StatusCode}");
                }

                // Return a default value or throw an exception based on your requirements.
                return 0;
            }
        }

    }
}