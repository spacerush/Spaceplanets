using Microsoft.Xna.Framework;
using SadConsole;
using Console = SadConsole.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePlanetsClient.Consoles
{
    class MessageLogConsole : Console
    {
        Color _semiTransparentBlack;

        public enum MessageTypes
        {
            Warning,
            Status,
            Problem,
            Battle,
            AdminOnlyMessage
        }

        public MessageLogConsole(int width, int height) : base(width, height)
        {
            this.Font = SadConsole.Global.FontDefault.Master.GetFont(Font.FontSizes.One);
            IsCursorDisabled = true;
            Cursor.IsVisible = false;
            UseKeyboard = false;

            _semiTransparentBlack = Color.Black;
            _semiTransparentBlack.A = 128;

            DefaultBackground = _semiTransparentBlack;

            Fill(Color.White, _semiTransparentBlack, 0);
            this[0].CopyAppearanceTo(Cursor.PrintAppearance);
        }

        public void Write(string text)
        {
            this.Print(DateTime.UtcNow.ToShortTimeString() + " UTC: " + text, MessageTypes.Status);
        }

        public void Write(string text, MessageTypes messageType)
        {
            this.Print(DateTime.UtcNow.ToShortTimeString() + " UTC: " + text, messageType);
        }

        public void Print(string text, MessageTypes type)
        {
            Color color;

            switch (type)
            {
                case MessageTypes.Warning:
                    color = Color.PaleVioletRed;
                    break;
                case MessageTypes.Problem:
                    color = Color.Orange;
                    break;
                case MessageTypes.Battle:
                    color = Color.LawnGreen;
                    break;
                case MessageTypes.Status:
                    color = Color.White;
                    break;
                case MessageTypes.AdminOnlyMessage:
                    color = Color.HotPink;
                    break;
                default:
                    color = Color.LightGray;
                    break;
            }

            Cursor.NewLine().Print(new ColoredString("* " + text, color, Color.Transparent) { IgnoreBackground = true });
        }

        public void Reset()
        {
            this.Clear();
            Cursor.Position = new Point(0, 0);
            Cursor.PrintAppearance = new Cell(Color.Yellow, Color.Black, 123);
        }
    }
}
