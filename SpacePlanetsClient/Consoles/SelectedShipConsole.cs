using Microsoft.Xna.Framework;
using SadConsole;
using Console = SadConsole.Console;
using System;
using System.Collections.Generic;
using System.Text;
using SadConsole.Input;
using SpacePlanetsClient.Extensions;
using System.Linq;
using SpacePlanets.SharedModels.ServerToClient;
using SadConsole.Controls;

namespace SpacePlanetsClient.Consoles
{
    public class SelectedShipConsole : ControlsConsole
    {
        SadConsole.Console mouseCursor;

        public SelectedShipConsole(int width, int height) : base(width, height)
        {
            Width = width;
            Height = height;
            mouseCursor = new SadConsole.Console(1, 1);
            mouseCursor.SetGlyph(0, 0, 178, new Color(255, 255, 255, 255));
            mouseCursor.UseMouse = false;
            Children.Add(mouseCursor);

            // Set up controls:
            Button btnShip = new Button(14, 1);
            btnShip.Name = "HeaderButton";
            btnShip.Position = new Point(0, 0);
            btnShip.IsEnabled = true;
            btnShip.IsVisible = true;
            btnShip.Text = "Ship Console";
            this.Add(btnShip);
            btnShip.Click += BtnShip_Click;
        }

        private void BtnShip_Click(object sender, EventArgs e)
        {
            GameState.RefreshShipConsole();
            var b = sender as Button;
            b.Text = Guid.NewGuid().ToString();
        }

        protected override void OnMouseLeftClicked(SadConsole.Input.MouseConsoleState state)
        {
            base.OnMouseLeftClicked(state);
        }

        public override bool ProcessMouse(MouseConsoleState state)
        {
            mouseCursor.IsVisible = state.IsOnConsole;
            mouseCursor.Position = state.ConsoleCellPosition;
            return base.ProcessMouse(state);
        }

        public override void Draw(TimeSpan delta)
        {
            base.Draw(delta);
        }


    }
}
