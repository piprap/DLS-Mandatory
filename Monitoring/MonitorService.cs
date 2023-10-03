using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.Span;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;

namespace Monitoring;

public static class MonitorService
{
    public static readonly string ServiceName = Assembly.GetCallingAssembly().GetName().Name ?? "unknown";
    public static TracerProvider TracerProvider;
    public static ActivitySource ActivitySource = new ActivitySource(ServiceName);
    public static ILogger Log => Serilog.Log.Logger;
    static MonitorService()
    {
        //OpenTelemetry
        TracerProvider = Sdk.CreateTracerProviderBuilder()
        .AddSource(ActivitySource.Name)
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName))
        .AddZipkinExporter()
        .Build();
        //Log
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.WithSpan()
            .WriteTo.Console()
            .WriteTo.Seq("http://seq-service:80")
            .CreateLogger();
    }
}
