using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;

namespace AddService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddController : ControllerBase
    {
        private IDbConnection historyCache = new MySqlConnection("Server=history-db;Database=history-database;Uid=historydb;Pwd=C@ch3d1v;");

        [HttpGet]
        public long Get(long num1, long num2)
        {
            return num1 + num2;
        }
    }
}