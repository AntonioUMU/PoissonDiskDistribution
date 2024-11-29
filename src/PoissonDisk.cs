using System;

namespace PoissonDiskDistribution.src
{
    public static class PoissonDisk
    {
        public const int MAX_SAMPLES = 20;

        public static List<Point> Distribution(Point initialPoint, int width, int height, int max_samples = MAX_SAMPLES)
        {
            Queue<Point>  points = new Queue<Point>();
            SpawnPoints spawnPoints = new SpawnPoints(width, height);

            points.Enqueue(initialPoint);
            spawnPoints.AddPoint(initialPoint);

            Random random = new Random();
            float radius = initialPoint.radius;

            while (points.Count > 0)
            {
                Point active = points.Dequeue();

                for (int i = 0; i < max_samples; i++)
                {
                    float angle = (float)(random.NextDouble() * Math.PI * 2);
                    float dis = (float)(2 * radius + random.NextDouble() * (2 * radius - radius) + radius);
                    Point newPoint = new Point(
                        (float)(active.x + dis * Math.Cos(angle)),
                        (float)(active.y + dis * Math.Sin(angle)),
                        radius
                        );

                    if (spawnPoints.ValidPoint(newPoint))
                    {
                        spawnPoints.AddPoint(newPoint);
                        points.Enqueue(newPoint);
                    }
                }
            }

            return spawnPoints.GetPoints();
        }
       
        public static List<Point> Distribution(int width, int height, float radius = 1, int max_samples = MAX_SAMPLES)
        {
            Random random = new Random();
            int x = random.Next(0, width);
            int y = random.Next(0, height);
            Point initialPoint = new Point(x, y, radius);
            return Distribution(initialPoint, width, height, max_samples);  
        }
    }
}
