using AddService.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;

namespace HistoryService.Controllers;

[ApiController]
[Route("[controller]")]
public class HistoryController : ControllerBase
{
    private IDbConnection historyCache = new MySqlConnection("Server=history-db;Database=history-database;Uid=historydb;Pwd=C@ch3d1v;");

    public HistoryController() {
        historyCache.Open();
        var tables = historyCache.Query<string>("SHOW TABLES LIKE 'historylogs'");
        if (!tables.Any())
        {
            historyCache.Execute("CREATE TABLE historylogs (id INT NOT NULL AUTO_INCREMENT, inputone INT NOT NULL, inputtwo INT NOT NULL, output INT NOT NULL, operation VARCHAR(50) NOT NULL, PRIMARY KEY (id))");
        }
    }


    [HttpGet("/get/addition")]
    public IEnumerable<History> GetAdditions()
    {
        //return where opretaion = addition
        //lav 2 get endpoints 1 til sub og en add.
        //var result = historyCache.QueryFirstOrDefault<History>("SELECT * FROM historylogs WHERE inputone = @inputone", new { inputone = inputone });
       // var result1 =  historyCache.Query<string>("SELECT inputone, inputtwo, output FROM historylogs WHERE operation = 'addition'");
        var result2 = historyCache.ExecuteReader("SELECT inputone, inputtwo, output FROM historylogs WHERE operation = 'addition'");
        DataTable dataTable = new DataTable();

        for (int i = 0;i<result2.FieldCount;i++)
        {
            DataColumn column = new DataColumn();
            column.ColumnName=result2.GetName(i);
            dataTable.Columns.Add(column);
        }

        while(result2.Read())
        {
            DataRow dataRow = dataTable.NewRow();
            for(int i = 0; i<result2.FieldCount;i++)
            {
                try
                {
                    Console.WriteLine(result2[i].ToString());
                    dataRow[i] = result2[i].ToString();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
            dataTable.Rows.Add(dataRow);
            dataRow = null;
        }
        Console.WriteLine("rows" + dataTable.Rows);
        Console.WriteLine("cols" + dataTable.Columns);

        //foreach (var x in result1) { Console.WriteLine(x); }
        //        foreach (var item in list) { Console.WriteLine("ListItem: " + item); }

        //Console.WriteLine("first: " + result1.First());

        //Console.WriteLine("last: " + result1.Last());//.ToString());
        // Console.WriteLine((IEnumerable<History>)result1.ToList());
        //var aHistory = new History(){ Num1=long.Parse(result1.First()),Num2=long.Parse(result1.First()),Result=long.Parse(result1.First()),Operation="addition"};
        // Console.WriteLine("Hist OBJ: "+ aHistory.Num1 +" " +aHistory.Num2 );



        Console.WriteLine("TESTER CONSOLE");
        return (IEnumerable<History>)result2;

    }

    [HttpGet("/get/subtraction")]
    public long GetSubtractions(long inputone, long inputtwo)
    {
        //return where opretaion = addition
        //lav 2 get endpoints 1 til sub og en add.
        return historyCache.QueryFirstOrDefault<int>("SELECT output FROM historylogs WHERE inputone = @inputone", new { inputone = inputone });

    }

    [HttpPost("/post/addition")]
    public void SaveAddition([FromQuery] long inputone, [FromQuery] long inputtwo, [FromQuery] long output)
    {
        //var output = inputone + inputtwo;
        string operation = "addition";
       // string id = inputone + "_" + inputtwo + "_addition";
        historyCache.Execute("REPLACE INTO historylogs (inputone, inputtwo, output, operation) VALUES (@inputone, @inputtwo, @output, @operation)", new { inputone = inputone, inputtwo = inputtwo, output = output, operation = operation });
    }

    [HttpPost("/post/subtraction")]
    public void SaveSubtraction([FromQuery] long inputone, [FromQuery] long inputtwo, [FromQuery] long output)
    {
        //var output = inputone - inputtwo;
        string operation = "subtraction";
        historyCache.Execute("REPLACE INTO historylogs (inputone, inputtwo, output, operation) VALUES (@number, @divisors, @output, @operation)", new { inputone = inputone, inputtwo = inputtwo, output = output, operation = operation });
    }

}
