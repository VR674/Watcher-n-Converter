using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace WatcherApp
{
    class Producer : IProducer
    {
        private string host;

        public Producer()
        {
            host = "localhost";
        }

        // Отправляет объект брокеру 
        public void Send<T>(T SandingObj) where T : class
        {
            using (var bus = RabbitHutch.CreateBus("host=" + host))
            {
                bus.Publish(SandingObj);
            }
        }
            
    }
}
