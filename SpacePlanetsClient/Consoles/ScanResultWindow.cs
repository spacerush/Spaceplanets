using SadConsole;
using Microsoft.Xna.Framework;
using SadConsole.Controls;
using System;
using SpacePlanetsClient.Extensions;
using Console = SadConsole.Console;
using SpacePlanets.SharedModels.ServerToClient;
using SpacePlanets.SharedModels.GameObjects;

namespace SpacePlanetsClient.Consoles
{
    public class ScanResultWindow : Window
    {
        private readonly ScanResultConsole _scanResultConsole;

        public ScanResultWindow(int width, int height, Console toReturnTo) : base(width, height)
        {
            this.CanDrag = true;
            _scanResultConsole = new ScanResultConsole(this.Width - 2, this.Height - 2, toReturnTo);
            this.Children.Add(_scanResultConsole);
            _scanResultConsole.IsVisible = true;
            _scanResultConsole.IsFocused = true;
            _scanResultConsole.CenterWithinParent();
        }

        public void SetLoot(LootScanResponse lootScanResponse)
        {
            if (lootScanResponse.SpaceLoots != null && lootScanResponse.SpaceLoots.Count > 0)
            {
                ListBox lootList = new ListBox(_scanResultConsole.Width - 4, _scanResultConsole.Height - 4);
                lootList.Position = new Point(2, 2);
                lootList.IsVisible = true;
                lootList.IsScrollBarVisible = true;
                foreach (var item in lootScanResponse.SpaceLoots)
                {
                    foreach (var shipmodule in item.ShipModules)
                    {
                        lootList.Items.Add(shipmodule);
                    }
                }
                _scanResultConsole.Add(lootList);
                lootList.MouseButtonClicked += LootList_MouseButtonClicked;
                _scanResultConsole.AddTakeAllButton();
            }
            else
            {
                _scanResultConsole.Print(2, 2, "Scans did not reveal anything useful.");
            }
        }

        private void LootList_MouseButtonClicked(object sender, SadConsole.Input.MouseEventArgs e)
        {
            var box = (sender as ListBox);
            if (box.SelectedItem != null)
            {
                var exemplar = new SpacePlanets.SharedModels.GameObjects.ShipModule();
                if (box.SelectedItem.GetType() == exemplar.GetType())
                {
                    GameState.WriteGeneralMessageToLog("Ship module :" + box.SelectedItem.ToString());
                }
            }
        }

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);
        }

        protected override void OnFocused()
        {
            IsFocused = false;
            _scanResultConsole.IsFocused = true;
            base.OnFocused();
        }
    }
}
