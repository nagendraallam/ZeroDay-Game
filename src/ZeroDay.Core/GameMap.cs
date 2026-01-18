namespace ZeroDay.Core
{
    public class GameMap
    {
        // A simple 2D grid of tile IDs
        // 0 = Grass, 1 = Dirt, etc.
        public int[,] Tiles { get; private set; }
        public int Width => Tiles.GetLength(1);
        public int Height => Tiles.GetLength(0);

        private List<int> grass = new List<int> { 12, 67, 12, 12, 12, 12, 12, 12, 12, 12, 68, 12, 69, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12 };
        Random rand = new Random();

        public GameMap(int width, int height)
        {
            Tiles = new int[height, width];
            GenerateSimpleMap();
        }

        private void GenerateSimpleMap()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    // 1. CORNERS
                    if (x == 0 && y == 0)
                        Tiles[y, x] = 0; // Top Left Corner
                    else if (x == Width - 1 && y == 0)
                        Tiles[y, x] = 2; // Top Right Corner
                    else if (x == 0 && y == Height - 1)
                        Tiles[y, x] = 22; // Bottom Left Corner
                    else if (x == Width - 1 && y == Height - 1)
                        Tiles[y, x] = 24; // Bottom Right Corner

                    // 2. EDGES
                    else if (y == 0)
                        Tiles[y, x] = 1; // Top Edge
                    else if (y == Height - 1)
                        Tiles[y, x] = 23; // Bottom Edge
                    else if (x == 0)
                        Tiles[y, x] = 11; // Left Edge
                    else if (x == Width - 1)
                        Tiles[y, x] = 13; // Right Edge
                    else
                        Tiles[y, x] = grass[rand.Next(grass.Count)];
                }
            }
        }
    }
}
