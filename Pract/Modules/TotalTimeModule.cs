using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pract.Modules
{
    public class TotalTimeModule : IHttpModule 
    {
        private static float totalTime = 0;
        private static int requestCount = 0;
        public void Init(HttpApplication app) 
        {
            IHttpModule module = app.Modules["Timer"];
            if (module != null && module is TimerModule) 
            {
                TimerModule timerModule = (TimerModule)module;
                timerModule.RequestTimed += HandleRequestTimed;
            }
            app.EndRequest += HandleEndRequest;
        }
        private void HandleRequestTimed(object src, RequestTimerEventArgs e)
        {
            totalTime += e.Duration;
            requestCount++;
        }
        private void HandleEndRequest(object src, EventArgs e)
        {
            JavaScript.ConsoleLog($"Количество обращений: {requestCount}");
            JavaScript.ConsoleLog($"Общее время обработки запросов: {totalTime:F5} секунд");
        }
        public void Dispose()
        {}
    }
}