using SadConsole;
using Microsoft.Xna.Framework;
using SadConsole.Controls;
using System;
using SpacePlanetsClient.Extensions;
using Console = SadConsole.Console;

namespace SpacePlanetsClient.Consoles
{
    public class ErrorWindow : Window
    {
        private readonly ErrorConsole errorConsole;

        public ErrorWindow(int width, int height, string errorMessage, string errorId, Console toReturnTo) : base(width, height)
        {
            this.errorConsole = new ErrorConsole(this.Width - 4, this.Height - 4, errorMessage, errorId, toReturnTo);
            this.Children.Add(this.errorConsole);
            this.errorConsole.IsVisible = true;
            this.errorConsole.IsFocused = true;
            this.errorConsole.CenterWithinParent();
        }

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);
        }

        protected override void OnFocused()
        {
            IsFocused = false;
            this.errorConsole.IsFocused = true;
            base.OnFocused();
        }

    }
}
