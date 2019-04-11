using Microsoft.Xna.Framework;
using SadConsole;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsClient.Consoles
{
    public class GalaxyMapConsole : ScrollingConsole
    {
        private readonly Console _scrollingConsole;
        public void SetStars(SpLib.Objects.Galaxy galaxy)
        {
            this.Clear();
            foreach (var item in galaxy.Stars)
            {
                this.Print(item.X, item.Y, "*", Color.Yellow, Color.Black);
            }

        }
        public GalaxyMapConsole(int width, int height) : base(width, height)
        {
            this.ViewPort = new Rectangle(0, 0, width, height);
        }
    }
}
