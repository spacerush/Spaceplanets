using SadConsole;
using Microsoft.Xna.Framework;
using SadConsole.Controls;
using System;
using SpacePlanetsClient.Extensions;

namespace SpacePlanetsClient.Consoles
{
    public class LoginWindow : Window
    {
        private readonly LoginConsole _loginConsole;

        public LoginWindow(int width, int height) : base(width, height)
        {
            _loginConsole = new LoginConsole(this.Width - 4, this.Height - 4);
            _loginConsole.UseKeyboard = true;
            this.Children.Add(_loginConsole);
            _loginConsole.IsVisible = true;
            _loginConsole.IsFocused = true;
            _loginConsole.CenterWithinParent();
        }

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);
        }

        protected override void OnFocused()
        {
            IsFocused = false;
            _loginConsole.IsFocused = true;
            base.OnFocused();
        }

    }
}
