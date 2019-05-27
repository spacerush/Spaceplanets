using SadConsole;
using Microsoft.Xna.Framework;
using SadConsole.Controls;
using SpacePlanetsClient.Extensions;
using System;

namespace SpacePlanetsClient.Consoles
{
    public class ScanResultConsole : SadConsole.ControlsConsole
    {

        Button btnOk;
        Button btnTakeAll;
        SadConsole.Console _toReturnTo;

        public override void Update(TimeSpan timeElapsed)
        {
            try
            {
                base.Update(timeElapsed);
            }
            catch (Exception ex)
            {
                GameState.WriteGeneralMessageToLog("Exception in update method of ScanResultConsole: " + ex.Message);
            }
        }

        public ScanResultConsole(int width, int height, SadConsole.Console toReturnTo) : base(width, height)
        {
            _toReturnTo = toReturnTo;
            this.Fill(Color.Black, Color.Black, 255);
            this.DrawBox(new Rectangle(0, 0, this.Width, this.Height), new Cell(), null, ConnectedLineThin);
            btnOk = new Button(13, 1);
            btnOk.Text = "Close";
            btnOk.Position = new Point((width / 2) - (btnOk.Width/2), height - 2);
            btnOk.Click += (s, a) =>
            {
                toReturnTo.IsFocused = true;
                this.Parent.Parent.Children.Remove(this.Parent);
            };
            this.Add(btnOk);
            btnOk.CenterWithinParentHorizontally();
        }

        public void AddTakeAllButton()
        {
            Button btnTakeAll = new Button(13, 1);
            btnTakeAll.Text = "Take all";
            btnTakeAll.Position = new Point(btnOk.Position.X + btnOk.Width + 3, btnOk.Position.Y);
            btnTakeAll.Click += (s, a) =>
            {
                _toReturnTo.IsFocused = true;
                this.Parent.Parent.Children.Remove(this.Parent);
            };
            this.Add(btnTakeAll);
        }

    }
}
