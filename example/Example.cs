using System;
using System.Drawing;
using PoissonDiskDistribution.src;

namespace PoissonDiskDistribution.example
{
    public class Example
    {
        static void Main(string[] args)
        {
            int width = 100;
            int height = 100;
            float radius = 1;
            int max_samples = 50;

            // Generate points

            List<src.Point> distribution = PoissonDisk.Distribution(width, height, radius, max_samples);

            // Generate Image

            Bitmap bmp = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bmp);

            Brush brush = new SolidBrush(Color.Blue);

            foreach (var item in distribution)
            {
                int x = (int)(item.x - item.radius);
                int y = (int)(item.y - item.radius);
                Rectangle rect = new Rectangle(x, y, 1, 1);

                graphics.FillRectangle(brush, rect);
            }

            string filePath = "image.png";
            bmp.Save(filePath);

            graphics.Dispose();
            bmp.Dispose();
        }
        
    }
}
