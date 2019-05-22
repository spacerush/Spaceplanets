using Microsoft.Xna.Framework;
using SadConsole;
using Console = SadConsole.Console;
using System;
using System.Collections.Generic;
using System.Text;
using SadConsole.Input;
using SpacePlanetsClient.Extensions;
using System.Linq;
using SpacePlanets.SharedModels.ServerToClient;

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

        public static List<string> GetToolTipItems(int cellX, int cellY) {
            var results = new List<string>();
            if (GameState.cachedMapData != null && GameState.cachedMapData.MapDataCells != null)
            {
                List<MapDataCell> cells = GameState.cachedMapData.MapDataCells.Where(w => w.CellX == cellX && w.CellY == cellY).ToList();
                foreach (var item in cells)
                {
                    if (item.Stars != null && item.Stars.Count > 0)
                    {
                        foreach (var star in item.Stars)
                        {
                            results.Add("Star - " + star.Name);
                        }
                    }
                    if (item.SpaceObjects != null && item.SpaceObjects.Count > 0)
                    {
                        foreach (var spaceobj in item.SpaceObjects.OrderBy(o => o.ObjectType))
                        {
                            results.Add(spaceobj.ObjectType + " - " + spaceobj.Name);
                        }
                    }
                    if (item.Ships != null && item.Ships.Count > 0)
                    {
                        foreach (var ship in item.Ships.OrderBy(o => o.Name))
                        {
                            results.Add("Ship - " + ship.Type + " - " + ship.Name);
                        }
                    }
                }

            }
            return results;
        }

        public override bool ProcessMouse(MouseConsoleState state)
        {
            mouseCursor.IsVisible = state.IsOnConsole;
            mouseCursor.Position = state.ConsoleCellPosition;
            List<string> tooltipItems = GetToolTipItems(state.ConsoleCellPosition.X, state.ConsoleCellPosition.Y);
            if (tooltipItems.Count > 0)
            {
                tooltip.Resize(tooltipItems.GetLengthOfLongestItem(), tooltipItems.Count, true);
                int lineCounter = 0;
                foreach (var item in tooltipItems)
                {
                    if (item.StartsWith("Ship"))
                    {
                        tooltip.Print(0, lineCounter, item, Color.White, Color.Black);
                    }
                    if (item.StartsWith("Star"))
                    {
                        tooltip.Print(0, lineCounter, item, Color.Yellow, Color.Black);
                    }
                    if (item.StartsWith("Planet"))
                    {
                        tooltip.Print(0, lineCounter, item, Color.CornflowerBlue, Color.Black);
                    }
                    if (item.StartsWith("Asteroid"))
                    {
                        tooltip.Print(0, lineCounter, item, Color.SandyBrown, Color.Black);
                    }
                    if (item.StartsWith("Moon"))
                    {
                        tooltip.Print(0, lineCounter, item, Color.GhostWhite, Color.Black);
                    }
                    if (item.StartsWith("Warpgate"))
                    {
                        tooltip.Print(0, lineCounter, item, Color.GreenYellow, Color.Black);
                    }
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
