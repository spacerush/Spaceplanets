using SadConsole;
using Microsoft.Xna.Framework;
using SadConsole.Controls;
using System;
using SpacePlanetsClient.Extensions;
using Console = SadConsole.Console;

namespace SpacePlanetsClient.Consoles
{
    public class CharacterManagementWindow : Window
    {
        private readonly CharacterManagementConsole _characterConsole;

        public CharacterManagementWindow(int width, int height, Console toReturnTo) : base(width, height)
        {
            this.CanDrag = true;
            _characterConsole = new CharacterManagementConsole(this.Width - 8, this.Height - 8, toReturnTo);
            this.Children.Add(_characterConsole);
            _characterConsole.IsVisible = true;
            _characterConsole.IsFocused = true;
            _characterConsole.CenterWithinParent();
        }

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);
        }

        protected override void OnFocused()
        {
            IsFocused = false;
            _characterConsole.IsFocused = true;
            base.OnFocused();
        }
    }
}
