using SadConsole;
using Microsoft.Xna.Framework;
using SadConsole.Controls;

namespace SpacePlanetsClient.Extensions
{
    public static class ControlExtensions
    {

        public static void CenterWithinParent(this ControlBase control)
        {
            var parent = control.Parent;
            control.Position = new Point((parent.Width / 2) - (control.Width / 2), (parent.Height / 2) - (control.Height / 2));
        }

        public static void CenterWithinParentHorizontally(this ControlBase control)
        {
            var parent = control.Parent;
            control.Position = new Point((parent.Width / 2) - (control.Width / 2), control.Position.Y);
        }

        public static void CenterWithinParentVertically(this ControlBase control)
        {
            var parent = control.Parent;
            control.Position = new Point(control.Position.X, (parent.Height / 2) - (control.Height / 2));
        }

    }
}
