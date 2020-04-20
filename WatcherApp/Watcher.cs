using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using EasyNetQ;
using ImageContainer;

namespace WatcherApp
{
    class Watcher
    {
        private FileSystemWatcher watcher;

        public Watcher(string path)
        {
            watcher = new FileSystemWatcher();

            // Указываем за какой папкой нужно следить
            watcher.Path = path;

            // Указываем за какими типами файлов нужно следить
            watcher.Filters.Add("*.jpg");
            watcher.Filters.Add("*.png");
            watcher.Filters.Add("*.bmp");

            // Указываем функцию, которая сработает при создании файла в указанной папке
            watcher.Created += OnCreated;
    }


        // Запуск слежения
        public void Run()
        {
            // Активируем слежение за папкой
            watcher.EnableRaisingEvents = true;

            // Программа работает, пока пользователь не остановит
            Console.WriteLine("Введите 'q', чтобы выйти");
            while (Console.Read() != 'q') ;
        }

        // Отправляет картинки по шине, после чего удаляет.
        private void OnCreated(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"Новый файл: {e.FullPath} {e.ChangeType}");

            // брем вновь появившуюся картинку
            var img = (Bitmap)Image.FromFile(@e.FullPath);

            // картинку и ее имя отправляем вместе, поэтому используем класс (easyNetq требует ссылочный тип для отправки)
            ImgContainer imgContainer = new ImgContainer(img, GetExtensionFromPath(@e.FullPath));

            // отправляем картинку
            var producer = new Producer();
            producer.Send<ImgContainer>(imgContainer);

            // чтобы удалить отправленную картинку, очищаем от ней переменную 
            img.Dispose();

            // удаляем отправленную картинку
            System.IO.File.Delete(@e.FullPath);
        }


        // Достает имя файла из его пути
        private string GetExtensionFromPath(string path)
        {
            string[] PathParts = path.Split("\\");
            string extension = PathParts[PathParts.Length - 1];

            return extension;
        }
    }
}
