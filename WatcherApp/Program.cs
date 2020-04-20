using System;

namespace WatcherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // За какой папкой нужно следить
            string path = @"C:\Users\Winnie\source\repos\WatcherApp\incoming";

            Watcher watcher = new Watcher(path);
            watcher.Run();
        }
    }
}
