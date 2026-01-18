using Raylib_cs;
using System.Numerics;
using ZeroDay.Core;
using ZeroDay.Entities;

namespace ZeroDay.App.Renderer;

public class WorldRenderer
{
    private readonly Color _themeColor = Color.Green;

    private Texture2D _playerSprite;
    private Texture2D _tileSet;
    private Texture2D _water;
    private const int TileSize = 16; // Adjust if your tiles are 32x32
    private const int TilesPerRow = 11; // You confirmed 11 blocks per row
    private const float Scale = 3.0f;   // Makes the 16x16 tiles look bigger/better

    private const float WorldScale = 4.0f; // Scale up if the image is small
    private int _frameWidth;
    private int _frameHeight;

    public void LoadContent()
    {
        _playerSprite = Raylib.LoadTexture("Assets/player.png");
        _tileSet = Raylib.LoadTexture("Assets/Tilesets/Hills.png");
        _water = Raylib.LoadTexture("Assets/Tilesets/Water.png");
        // If your sprite sheet is 4x4 frames, divide width/height by 4
        _frameWidth = _playerSprite.Width / 4;
        _frameHeight = _playerSprite.Height / 4;
    }

    public void DrawPlayer(Player player)
    {
        // Define which part of the PNG to cut out
        Rectangle sourceRec = new Rectangle(
            player.CurrentFrame * _frameWidth,
            player.FacingRow * _frameHeight,
            _frameWidth,
            _frameHeight
        );

        // Define where to draw it on screen
        Rectangle destRec = new Rectangle(player.X, player.Y, _frameWidth * 2, _frameHeight * 2); // Scale x2

        Raylib.DrawTexturePro(_playerSprite, sourceRec, destRec, new Vector2(0, 0), 0f, Color.White);
    }

    public void DrawHUD(Time timer)
    {
        string timeText = timer.GetDisplayTime();
        int fontSize = 30;

        int textWidth = Raylib.MeasureText(timeText, fontSize);
        int posX = (Raylib.GetScreenWidth() / 2) - (textWidth / 2);

        Raylib.DrawText(timeText, posX, 20, fontSize, _themeColor);
    }

    public void DrawMap(GameMap map)
    {
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                int tileId = map.Tiles[y, x];

                // --- TARGETING LOGIC ---
                // Calculate where the tile sits in the PNG
                int column = tileId % TilesPerRow;
                int row = tileId / TilesPerRow;

                Rectangle source = new Rectangle(
                    column * TileSize,
                    row * TileSize,
                    TileSize,
                    TileSize
                );

                // Calculate where to draw it in the game world
                Rectangle dest = new Rectangle(
                    x * TileSize * Scale,
                    y * TileSize * Scale,
                    TileSize * Scale,
                    TileSize * Scale
                );

                Raylib.DrawTexturePro(_tileSet, source, dest, Vector2.Zero, 0, Color.White);
            }
        }

    }
    public void DrawBackground(int mapWidthPixels, int mapHeightPixels)
    {
        // This loop tiles your LARGE image across the entire map dimensions
        Raylib.ClearBackground(Color.SkyBlue);
    }


}
