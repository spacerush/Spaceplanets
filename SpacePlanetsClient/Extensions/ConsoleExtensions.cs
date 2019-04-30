using SadConsole;
using Microsoft.Xna.Framework;

namespace SpacePlanetsClient.Extensions
{
    public static class ConsoleExtensions
    {

        public static void CenterWithinParent(this Console console)
        {
            var parent = console.Parent;
            console.Position = new Point((parent.Width / 2) - (console.Width / 2), (parent.Height / 2) - (console.Height / 2));
        }

    }
}
