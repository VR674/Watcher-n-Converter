using System;
using System.Drawing;

namespace ImageContainer
{
    public class ImgContainer
    {
        public Bitmap Img { get; set; }
        public string Name { get; set; }

        public ImgContainer(Bitmap img, string name)
        {
            this.Img = img;
            this.Name = name;
        }
    }
}
