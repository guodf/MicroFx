using MicroFx.Thumbnails;
using System;

namespace MediaStreamSample
{
    internal class ThumbnailSample
    {
        private static void Main(string[] args)
        {
            var size = new System.Drawing.Size(300, 300);
            Thumbnails.FromIamge("1.jpg", "image.jpg",size);
            Thumbnails.FromGif("1.gif", "gif.jpg",size);
            Thumbnails.FromVideo("1.mp4","video.jpg");
            Console.WriteLine("Hello World!");
        }
    }
}
