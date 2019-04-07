using Microsoft.Xna.Framework;
using SadConsole.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsClient.Consoles
{
    public class MenuBarConsole : SadConsole.ControlsConsole
    {
        public MenuBarConsole(int width, int height) : base(width, height)
        {
            // Button for choosing character.
            Button character = new Button(12, 1);
            character.Text = "Characters";
            character.Position = new Point(0, 0);
            character.Click += Character_Click;
            character.IsVisible = true;
            character.CanFocus = false;
            this.Add(character);

            // Button for choosing ship.
            Button ship = new Button(6, 1);
            ship.Text = "Ship";
            ship.Position = new Point(13, 0);
            ship.Click += Ship_Click;
            ship.IsVisible = true;
            ship.CanFocus = false;
            this.Add(ship);
        }

        private void Character_Click(object sender, EventArgs e)
        {
            if (GameState.DisplayingCharacterMenu)
            {
                GameState.SetMenusHidden();
            }
            else
            {
                GameState.SetMenusHidden();
                GameState.RetrieveCharactersForCharacterMenu();
            }
            GameState.WriteGeneralMessageToLog("Character clicked!");
        }

        private void Ship_Click(object sender, EventArgs e)
        {
            if (GameState.DisplayingShipMenu)
            {
                GameState.SetMenusHidden();
            }
            else
            {
                GameState.SetMenusHidden();
                GameState.RetrieveShipsForShipMenu();
            }
            GameState.WriteGeneralMessageToLog("Ship clicked!");
        }

    }
}
