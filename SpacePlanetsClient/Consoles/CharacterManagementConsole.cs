using SadConsole;
using Microsoft.Xna.Framework;
using SadConsole.Controls;
using SpacePlanetsClient.Extensions;
using System;

namespace SpacePlanetsClient.Consoles
{
    public class CharacterManagementConsole : SadConsole.ControlsConsole
    {

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);
        }

        public CharacterManagementConsole(int width, int height, SadConsole.Console toReturnTo) : base(width, height)
        {
            this.Fill(Color.Black, Color.Black, 255);

            Button btnOk = new Button(13, 1);
            btnOk.Text = "Close";
            btnOk.Position = new Point(width - 20, height - 2);
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
