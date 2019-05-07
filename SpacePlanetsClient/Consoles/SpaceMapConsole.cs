using Microsoft.Xna.Framework;
using SadConsole;
using Console = SadConsole.Console;
using System;
using System.Collections.Generic;
using System.Text;
using SadConsole.Input;
using SpacePlanetsClient.Extensions;

namespace SpacePlanetsClient.Consoles
{
    public class SpaceMapConsole : Console
    {

        private readonly Rectangle trackedRegion;
        SadConsole.Console mouseCursor;
        SadConsole.Console tooltip;
        public int? SelectedX;
        public int? SelectedY;
        private bool tooltipPresent = false;

        public SpaceMapConsole(int width, int height) : base(width, height)
        {
            Width = width;
            Height = height;
            trackedRegion = new Rectangle(0, 0, width, height);
            tooltip = new SadConsole.Console(10, 1);
            mouseCursor = new SadConsole.Console(1, 1);
            mouseCursor.SetGlyph(0, 0, 178, new Color(255, 255, 255, 255));
            mouseCursor.UseMouse = false;
            tooltip.UseMouse = false;
            tooltip.IsVisible = false;
            Children.Add(tooltip);
            Children.Add(mouseCursor);
        }

        protected override void OnMouseLeftClicked(SadConsole.Input.MouseConsoleState state)
        {
            if (trackedRegion.Contains(state.ConsoleCellPosition.X, state.ConsoleCellPosition.Y))
            {
                SelectedX = state.ConsoleCellPosition.X;
                SelectedY = state.ConsoleCellPosition.Y;
                //GameState.PlayerLeftClickingSystemMap(SelectedX.Value, SelectedY.Value);
            }
            base.OnMouseLeftClicked(state);
        }


        public override bool ProcessMouse(MouseConsoleState state)
        {
            mouseCursor.IsVisible = state.IsOnConsole;
            mouseCursor.Position = state.ConsoleCellPosition;
            List<string> tooltipItems = new List<string>();// formerly GameState.GetToolTipItems(state.ConsoleCellPosition.X, state.ConsoleCellPosition.Y);
            if (tooltipItems.Count > 0)
            {
                tooltip.Resize(tooltipItems.GetLengthOfLongestItem(), tooltipItems.Count, true);
                int lineCounter = 0;
                foreach (var item in tooltipItems)
                {
                    tooltip.Print(0, lineCounter, item, Color.White, Color.Black);
                    lineCounter++;
                }
                tooltip.Position = state.ConsoleCellPosition + new Point(1, 1);
                tooltip.IsVisible = true;
            }
            else
            {
                tooltip.IsVisible = false;
            }
            if ((tooltip.Position.X + tooltip.Width) > Width)
            {
                tooltip.Position = state.ConsoleCellPosition - new Point(tooltip.Width + 2, 0);
            }
            return base.ProcessMouse(state);
        }

        public override void Draw(TimeSpan delta)
        {
            base.Draw(delta);
        }


    }
}
