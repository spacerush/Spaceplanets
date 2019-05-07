using System;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePlanetsClient;
using Microsoft.Extensions.DependencyInjection;

namespace MyProject
{
    class Program
    {

        public const int Width = 110;
        public const int Height = 40;

        static void Main()
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create("fonts/curses_vector_16x24.font", Width, Height);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Hook the update event that happens each frame so we can trap keys and respond.

            SadConsole.Game.OnUpdate = Update;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Update(GameTime time)
        {
            // Called each logic update.

            // As an example, we'll use the F5 key to make the game full screen
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5))
            {
                SadConsole.Settings.ToggleFullScreen();
            }

            // If the space key is pressed down solid...
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                // TODO: add something
            }

            // If the space key is pressed down solid...
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.W))
            {
                GameState.MoveSelectedShipUp();
            }
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.A))
            {
                GameState.MoveSelectedShipLeft();
            }
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.S))
            {
                GameState.MoveSelectedShipDown();
            }
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.D))
            {
                GameState.MoveSelectedShipRight();
            }

        }


        private static void Init()
        {
            // TODO: stop hardcoding this endpoint!
            var startingConsole = SadConsole.Global.CurrentScreen;
            GameState.InitializeConsole(startingConsole);
        }
    }
}