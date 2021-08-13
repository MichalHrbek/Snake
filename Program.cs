using System;
using Raylib_cs;
using System.Collections.Generic;

namespace Snake
{
    static class Program
    {
        public static void Main()
        {
            Random rnd = new Random(); // Random for apple

            const int gridWidth = 15, gridHeigth = 15, gridSize = 50 /* In pixels */, fps = 60;

            int width = gridSize * gridWidth, heigth = gridSize * gridHeigth, f = 0; // Window size

            bool dead = false; // Death state

            int speed = 7; // Snake speed

            Vector2i head = new Vector2i(0, 0); // Player position

            Vector2i apple = new Vector2i(rnd.Next(0, gridWidth - 1), rnd.Next(0, gridHeigth - 1)); // Aplle position
            
            List<Vector2i> body = new List<Vector2i>(); // Positions of body cells

            Raylib.SetTargetFPS(fps);
            Raylib.InitWindow(width, heigth, "Snake");

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);

                Raylib.DrawRectangle(apple.X * gridSize, apple.Y * gridSize, gridSize, gridSize, Color.RED); // Draw apple
                Raylib.DrawRectangle(head.X*gridSize, head.Y*gridSize, gridSize, gridSize, Color.GREEN); // Draw head
                
                for (int i = 0; i < body.Count; i++)
                {
                    Raylib.DrawRectangle(body[i].X * gridSize, body[i].Y * gridSize, gridSize, gridSize, Color.LIME);
                } // Draw body
                
                if (apple.X == head.X && apple.Y == head.Y)
                {
                    apple.X = rnd.Next(0, gridWidth - 1);
                    apple.Y = rnd.Next(0, gridHeigth - 1);
                    body.Add(new Vector2i(-1, -1));
                } // Apple pickup

                if (!dead)
                {
                    f++;
                    Vector2i direction = Input.GetInput();
                    if (f == fps / speed)
                    {
                        f = 0;
                        if (body.Count > 0)
                        {
                            for (int i = 2; i < body.Count + 1; i++)
                            {
                                body[body.Count - i + 1] = body[body.Count - i];
                            }
                            body[0] = new Vector2i(head.X, head.Y);
                        } // Changes bodys position to body
                        
                        // Player movement
                        head.X += direction.X;
                        head.Y += direction.Y;
                        
                        // Checks if snake isn't outside the windows
                        if (head.X > gridWidth - 1) dead = true;
                        if (head.Y > gridHeigth - 1) dead = true;
                        if (head.X < 0) dead = true;
                        if (head.Y < 0) dead = true;

                        for (int i = 0; i < body.Count; i++)
                        {
                            if (head.X == body[i].X && head.Y == body[i].Y)
                            {
                                dead = true;
                            }
                        } // Kill the snake if he collides with his body
                    } // Moves every fps/speed frames 
                } // Movement

                for (int x = 0; x < gridWidth; x++)
                {
                    for (int y = 0; y < gridHeigth; y++)
                    {
                        Raylib.DrawLine(x * gridSize, y * gridSize, x * gridSize + gridSize, y * gridSize, Color.BLACK);
                        Raylib.DrawLine(x * gridSize, y * gridSize, x * gridSize, y * gridSize + gridSize, Color.BLACK);
                    }
                } // Draw grid lines

                if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
                {
                    dead = false;
                } // Invincible

                if (dead)
                {
                    Raylib.DrawText("Game Over", 0, 0 / 2, 48, Color.WHITE);
                } // Game Over text

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}