using System;

namespace PoissonDiskDistribution.src
{
    public sealed class SpawnPoints
    {
        int GRID_WIDTH;
        int GRID_HEIGHT;

        List<Point> _points;
        bool[] _grid;
        float _cellSize = 1;


        public SpawnPoints(int width, int height)
        {
            this.GRID_WIDTH = width;
            this.GRID_HEIGHT = height;

            _points = new List<Point>();
            _grid = new bool[GRID_WIDTH * GRID_HEIGHT];

            for (int i = 0; i < GRID_WIDTH; i++)
            {
                _grid[i] = false;
            }
        }

        public List<Point> GetPoints()
        {
            return _points;
        }

        public void AddPoint(Point point)
        {
            if (point.x < 0 || point.x >= GRID_WIDTH || point.y < 0 || point.y >= GRID_HEIGHT)
            {
                return;
            }

            foreach (int item in AdyacentsCells(point))
            {
                _grid[item] = true;
            }

            _points.Add(point);
        }

        public bool ValidPoint(Point point)
        {
            if (point.x < 0 || point.x >= GRID_WIDTH || point.y < 0 || point.y >= GRID_HEIGHT)
            {
                return false;
            }

            foreach (int cell in AdyacentsCells(point))
            {
                if (_grid[cell])
                {
                    return false;
                }
            }

            return true;
        }

        List<int> AdyacentsCells(Point point)
        {
            List<int> adyacents = new List<int>();

            int posX = (int)(point.x / _cellSize);
            int posY = (int)(point.y / _cellSize);
            posX = Math.Clamp(posX, 0, GRID_WIDTH - 1);
            posY = Math.Clamp(posY, 0, GRID_HEIGHT - 1);

            int dis = (int)Math.Ceiling(point.radius / _cellSize);

            int minX = Math.Max(posX - dis, 0);
            int maxX = Math.Min(posX + dis, GRID_WIDTH - 1);
            int minY = Math.Max(posY - dis, 0);
            int maxY = Math.Min(posY + dis, GRID_HEIGHT - 1);

            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minY; j <= maxY; j++)
                {
                    adyacents.Add(i + GRID_WIDTH * j);
                }
            }

            return adyacents;
        }

    }
}
