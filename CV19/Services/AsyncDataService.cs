using CV19.Services.Interfaces;
using System;
using System.Threading;

namespace CV19.Services
{
    internal class AsyncDataService : IAsyncDataService
    {
        private const int _sleepTime = 5000;

        public string GetResult(DateTime time)
        {
            Thread.Sleep(_sleepTime);

            return $"Result = {time}";
        }
    }
}
