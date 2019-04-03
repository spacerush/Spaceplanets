using SadConsole;
using Microsoft.Xna.Framework;
using SadConsole.Controls;
using System;
using SpacePlanetsClient.Extensions;

namespace SpacePlanetsClient.Consoles
{
    public class ErrorWindow : Window
    {
        private readonly ErrorConsole _errorConsole;

        public ErrorWindow(int width, int height, string errorMessage, string errorId) : base(width, height)
        {
            _errorConsole = new ErrorConsole(this.Width - 4, this.Height - 4, errorMessage, errorId);
            this.Children.Add(_errorConsole);
            _errorConsole.IsVisible = true;
            _errorConsole.IsFocused = true;
            _errorConsole.CenterWithinParent();
        }

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);
        }

        protected override void OnFocused()
        {
            IsFocused = false;
            _errorConsole.IsFocused = true;
            base.OnFocused();
        }

    }
}
