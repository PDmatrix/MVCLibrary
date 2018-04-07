using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Pract.Modules
{
    

    public class RequestTimerEventArgs : EventArgs
    {
        public float Duration { get; set; }
    }
    public class TimerModule : IHttpModule
    {
        

        private Stopwatch timer;
        public event EventHandler<RequestTimerEventArgs> RequestTimed;
        public void Init(HttpApplication app)
        {
            app.BeginRequest += HandleBeginRequest;
            app.EndRequest += HandleEndRequest;
        }
        private void HandleBeginRequest(object src, EventArgs args)
        {
            timer = Stopwatch.StartNew();
        }
        private void HandleEndRequest(object src, EventArgs args)
        {
            float duration = ((float)timer.ElapsedTicks) / Stopwatch.Frequency;
            JavaScript.ConsoleLog($"Время обработки запроса: {duration:F5} секунд");
            if (RequestTimed != null)
            {
                RequestTimed(this,
                    new RequestTimerEventArgs { Duration = duration });
            }
        }
        public void Dispose()
        {}
    }
}