using SadConsole;
using Microsoft.Xna.Framework;
using SadConsole.Controls;
using SpacePlanetsClient.Extensions;
using System;

namespace SpacePlanetsClient.Consoles
{
    public class ErrorConsole : SadConsole.ControlsConsole
    {

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);
        }

        public ErrorConsole(int width, int height, string errorMessage, string errorId, SadConsole.Console toReturnTo) : base(width, height)
        {
            this.Fill(Color.Black, Color.Black, 255);
            this.Print(1, 1, "Error received:", Color.WhiteSmoke, Color.Black);
            this.Print(1, 3, errorMessage, Color.HotPink, Color.Black);
            string footer = "Support ID #" + errorId;
            this.Print(1, this.Height - 3, footer, Color.DimGray, Color.Black);

            Button btnOk = new Button(13, 1);
            btnOk.Text = "Dismiss";
            btnOk.Position = new Point(width - 20, height -2);
            btnOk.Click += (s, a) =>
            {
                toReturnTo.IsFocused = true;
                this.Parent.Parent.Children.Remove(this.Parent);
            };
            this.Add(btnOk);
            btnOk.CenterWithinParentHorizontally();
        }

    }
}
