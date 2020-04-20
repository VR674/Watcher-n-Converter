using System;
using EasyNetQ;

namespace ConverterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Converter converter = new Converter();
            converter.Run();
        }
    }
}