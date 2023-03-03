using System;
using System.Drawing;
namespace Lab3_sem4._3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bitmap[] images = new Bitmap[]
            {
                new Bitmap("image1.jpg"),
                new Bitmap("image2.jpg"),
                new Bitmap("image3.jpg")
            };
            Func<Bitmap, Bitmap>[] imageOperations = new Func<Bitmap, Bitmap>[]
            {
                InvertColors,
                RotateImage,
            };

            foreach (var image in images)
            {
                foreach (var operation in imageOperations)
                {
                    image = operation(image);
                }
                DisplayImage(image);
            }

            Console.ReadKey();
        }


        static Bitmap InvertColors(Bitmap image)
        {
        }
        static Bitmap RotateImage(Bitmap image)
        {
        }
        static void DisplayImage(Bitmap image)
        {
        }
    }
}