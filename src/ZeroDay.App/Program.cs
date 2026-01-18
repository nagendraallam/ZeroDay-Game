using Raylib_cs;
using ZeroDay.Core;
using ZeroDay.Entities;
using ZeroDay.App.Renderer;
using System.Numerics;

// Window Setup
const int screenWidth = 800;
const int screenHeight = 450;
Raylib.InitWindow(screenWidth, screenHeight, "ZeroDay");

Time gameTimer = new();
WorldRenderer renderer = new();

GameMap myMap = new GameMap(50, 50);

renderer.LoadContent();
Player player = new(400, 225);

// Setup Camera
Camera2D camera = new Camera2D();
camera.Target = new Vector2(player.X, player.Y);
camera.Offset = new Vector2(400, 225); // Center of screen
camera.Rotation = 0.0f;
camera.Zoom = 2.0f; // Zoom in to see the pixel art better

while (!Raylib.WindowShouldClose())
{
    float moveX = 0;
    float moveY = 0;

    if (Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Up)) { moveY = -1; }
    if (Raylib.IsKeyDown(KeyboardKey.S) || Raylib.IsKeyDown(KeyboardKey.Down)) { moveY = +1; }
    if (Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Left)) { moveX = -1; }
    if (Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.Down)) { moveX = +1; }

    // --- UPDATE ---
    gameTimer.Tick(Raylib.GetFrameTime());
    player.Update(moveX, moveY, Raylib.GetFrameTime());
    camera.Target = new Vector2(player.X, player.Y);

    // --- DRAW ---
    Raylib.BeginDrawing();
    renderer.DrawBackground(100, 100);
    Raylib.BeginMode2D(camera); // START CAMERA MODE


    renderer.DrawMap(myMap);
    renderer.DrawHUD(gameTimer); // Pass the timer to the renderer
    renderer.DrawPlayer(player);

    Raylib.EndDrawing();
}

Raylib.CloseWindow();
