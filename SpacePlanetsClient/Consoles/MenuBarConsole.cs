using Microsoft.Xna.Framework;
using SadConsole.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsClient.Consoles
{
    public class MenuBarConsole : SadConsole.ControlsConsole
    {
        private int R;
        TextBox txtR;


        public MenuBarConsole(int width, int height) : base(width, height)
        {
            R = 0;
            // Button for choosing character.
            Button character = new Button(12, 1);
            character.Text = "Characters";
            character.Position = new Point(0, 0);
            character.Click += Character_Click;
            character.IsVisible = true;
            character.CanFocus = false;
            this.Add(character);

            // Button for choosing ship.
            Button ships = new Button(6, 1);
            ships.Text = "Ships";
            ships.Position = new Point(12, 0);
            ships.Click += Ship_Click;
            ships.IsVisible = true;
            ships.CanFocus = false;
            this.Add(ships);

            // Button for choosing organization.
            Button organization = new Button(14, 1);
            organization.Text = "Organization";
            organization.Position = new Point(18, 0);
            organization.Click += Organization_Click;
            organization.IsVisible = true;
            organization.CanFocus = false;
            this.Add(organization);

            Button btnAdmin = new Button(10, 1);
            btnAdmin.Text = "Admin";
            btnAdmin.Position = new Point(this.Width - 12, 0);
            btnAdmin.Click += BtnAdmin_Click;
            btnAdmin.IsVisible = true;
            btnAdmin.CanFocus = true;
            this.Add(btnAdmin);

        }

        private void BtnAdmin_Click(object sender, EventArgs e)
        {
            if (GameState.DisplayingAdminMenu)
            {
                GameState.SetMenusHidden();
            }
            else
            {
                GameState.SetMenusHidden();
                GameState.ActivateAdminMenu();
            }
            GameState.WriteGeneralMessageToLog("Admin button clicked!");
        }

        private void Organization_Click(object sender, EventArgs e)
        {
            GameState.WriteGeneralMessageToLog("Organization click.");
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
