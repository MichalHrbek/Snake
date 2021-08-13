using Raylib_cs;

namespace Snake
{
    class Input
    {
        public static Vector2i direction = new Vector2i(0,0);
        public static Vector2i GetInput()
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                direction = new Vector2i(1,0);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                direction = new Vector2i(-1, 0);
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
                direction = new Vector2i(0, -1);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                direction = new Vector2i(0, 1);
            }
            return direction;
        }
    } // Arrow input
}
