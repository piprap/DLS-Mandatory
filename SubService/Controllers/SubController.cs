using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;

namespace SubService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubController : ControllerBase
    {
        private IDbConnection historyCache = new MySqlConnection("Server=history-db;Database=history-database;Uid=historydb;Pwd=C@ch3d1v;");

        [HttpPost]
        public long Post([FromQuery] long inputone, [FromQuery] long inputtwo)
        {
            //Lav beregning:
            long output = inputone - inputtwo;

            //Kald history service post:
            var client = new HttpClient();
            var baseAddress = "http://history-service/post/subtraction";
            var uri = new Uri($"{baseAddress}?inputone={inputone}&inputtwo={inputtwo}&output={output}");
            client.BaseAddress = uri;

            var x = client.SendAsync(new HttpRequestMessage(HttpMethod.Post, uri)).Result;
            Console.WriteLine("TESTER ADDITION SERVICE - - - " + x.ToString());


            return output;
        }
    }
}