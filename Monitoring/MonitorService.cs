using Serilog;
using System.Net.NetworkInformation;

namespace Monitoring;

public static class MonitorService
{
    public static ILogger Log => Serilog.Log.Logger;
    static MonitorService()
    {
            Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq("http://seq-service:80")
            .CreateLogger();
    }
}
