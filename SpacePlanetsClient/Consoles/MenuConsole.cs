using Microsoft.Xna.Framework;
using SadConsole.Controls;
using SadConsole.Themes;
using SpacePlanetsClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpacePlanetsClient.Extensions;
using SadConsole.Effects;
using SadConsole;

namespace SpacePlanetsClient.Consoles
{
    public class MenuConsole : SadConsole.ControlsConsole
    {

        private List<MenuButtonMetadataItem> _elements;
        public Fade menuFade;
        private ButtonTheme _buttonTheme;
        private bool _alignElementsLeft;

        public void SetButtonTheme(ButtonTheme theme)
        {
            _buttonTheme = theme;
        }
        public void SetElements(List<MenuButtonMetadataItem> elements)
        {
            _elements = elements;
        }

        public void ShowMenu(string title)
        {
            this.Effects.RemoveAll();
            this.ControlsList.Clear();
            /// Find out how many characters the longest element in our list of menu items has
            int lengthOfLongestElement = _elements.GetLengthOfLongestItem();
            //this.DrawBox(new Rectangle(0, 0, this.Width, this.Height), new Cell(Color.White, Color.Black), null, ConnectedLineEmpty);
            this.DrawBox(new Rectangle(0, 0, this.Width, this.Height), new Cell(Color.White, Color.Black), null, ConnectedLineThin);

            // Print the name/prompt of the menu at the top
            this.Print(1, 0, title, Color.White, Color.Black);

            // Create a button for each element in the _elements field, which should have been populated by the SetElements method.
            int buttonY = 0;
            int buttonX = 1;
            foreach (var item in _elements.OrderBy(o => o.ButtonText))
            {
                buttonY++;
                if (buttonY == this.Height -1)
                {
                    buttonX += lengthOfLongestElement;
                    buttonY = 1;
                }
                Button button = new Button(lengthOfLongestElement, 1);
                button.Theme = _buttonTheme;
                button.Position = new Point(buttonX, buttonY);
                button.Name = item.ButtonType + " " + item.Id.ToString();
                button.Text = item.ButtonText;
                button.IsVisible = true;
                button.Click += Button_Click;
                Add(button);
            }
            this.IsVisible = true;
        }


        private void Button_Click(object sender, EventArgs e)
        {
            var s = sender as Button;
            // the button should be named for the type of entity that is being clicked on.
            // for example, if this is a character menu the button name will be : Character [Guid], where
            // [Guid] is a unique identifier for a character.
            var buttonNameParts = s.Name.Split(" ");

            GameState.WriteGeneralMessageToLog(s.Name + " clicked.");
            if (buttonNameParts.First() == "Character")
            {
                GameState.DownloadCharacterForManagement(Guid.Parse(buttonNameParts.Last()));
            }
            if (buttonNameParts.First() == "Ship")
            {
                GameState.DownloadMapAtShip(Guid.Parse(buttonNameParts.Last()));
                GameState.SetSelectedShip(Guid.Parse(buttonNameParts.Last()));
            }
            GameState.SetMenusHidden();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width">The startup width of the menu. Ideally set to the size of the parent to start out.</param>
        /// <param name="height">The startup height of the menu. Ideally set to the size of the parent to start out.</param>
        /// <param name="menuItems">An optional list of strings that will become the text displayed on a series of buttons.</param>
        public MenuConsole(int width, int height, List<MenuButtonMetadataItem> menuItems = null) : base(width, height)
        {
            if (_elements == null)
            {
                _elements = new List<MenuButtonMetadataItem>();
            }
            else
            {
                _elements = menuItems;
            }
            menuFade = GameState.DefaultFade;

            _buttonTheme = new ButtonTheme();
            _buttonTheme.ShowEnds = false; // don't show the < > on the sides of buttons in this "menu" screen.
            _alignElementsLeft = false;

        }

    }

}
