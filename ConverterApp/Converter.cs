using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using EasyNetQ;
using ImageContainer;

namespace ConverterApp
{
    class Converter
    {

        private IBus bus;

        public Converter()
        {
            bus = RabbitHutch.CreateBus("host=localhost");
        }

        // Запуск ожидания информации от шины
        public void Run()
        {
            // подключаемся к шине
            bus.Subscribe<ImgContainer>("Img", HandleImage);

            // Программа работает, пока пользователь не остановит
            Console.WriteLine("Введите 'q', чтобы выйти");
            while (Console.Read() != 'q') ;
        }

        // Наложение вотермарки на полученную картинку и сохранение ее
        private void HandleImage(ImgContainer imgContainer)
        {
            Console.WriteLine($"получено изображение {imgContainer.Name}");

            using (var watermark = new Bitmap(@"C:\Users\Winnie\source\repos\ConverterApp\logo.png")) // берем вотермарку
            using (var graph = Graphics.FromImage(imgContainer.Img)) 
            {
                // накладываем вотермарку на полученное изображение
                graph.DrawImage(watermark, new Rectangle(0, 0, watermark.Width, watermark.Height));

                // сохранение получившегося изображения
                imgContainer.Img.Save(@"C:\Users\Winnie\source\repos\ConverterApp\outcoming\" + imgContainer.Name);

            }
        }
    }
}
