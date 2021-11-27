using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using System;

namespace LocadoraVeiculos.Infra.Logging
{
    public static class LocadoraLoggerManager
    {
        public static void ConfigurarLogger()
        {
            //apiKey: D5m3cBkPbCGIBUPne3nK

            var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Verbose);

            var config = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.Seq("http://localhost:5341",
                    apiKey: "D5m3cBkPbCGIBUPne3nK", controlLevelSwitch: levelSwitch)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("ApplicationName", "Locadora de Veículos")
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .Enrich.WithProperty("ProcessId", Environment.ProcessId)
                .Enrich.WithProperty("ThreadId", Environment.CurrentManagedThreadId)
                .Enrich.WithProperty("UserName", Environment.UserName);

            Log.Logger = config.CreateLogger();
        }
    }
}
