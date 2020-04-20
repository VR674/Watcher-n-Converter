using System;
using System.Collections.Generic;
using System.Text;

namespace WatcherApp
{
    interface IProducer
    {
        public void Send<T>(T SandingObj) where T : class;
    }
}
