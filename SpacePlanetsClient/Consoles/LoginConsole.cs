using SadConsole;
using Microsoft.Xna.Framework;
using SadConsole.Controls;
using System;

namespace SpacePlanetsClient.Consoles
{
    public class LoginConsole : SadConsole.ControlsConsole
    {

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);
        }

        public LoginConsole(int width, int height) : base(width, height)
        {
            this.Print(1, 1, "Please provide a username and password:", Color.WhiteSmoke, Color.Black);

            TextBox txtUsername = new TextBox(10);
            this.Print(1, height - 3, "username:", Color.WhiteSmoke, Color.Black);
            txtUsername.Position = new Point(1, height - 2);
            this.Add(txtUsername);

            TextBox txtPassword = new TextBox(20);
            this.Print(15, height - 3, "password:", Color.WhiteSmoke, Color.Black);
            txtPassword.Position = new Point(15, height - 2);
            txtPassword.PasswordChar = "*";
            this.Add(txtPassword);

            Button btnLogin = new Button(10, 3);
            btnLogin.Text = "Log in";
            btnLogin.Position = new Point(width - 10, height - 4);
            btnLogin.Click += (s, a) =>
            {
                if (GameState.Connection != null && GameState.Connection.State == Microsoft.AspNetCore.SignalR.Client.HubConnectionState.Connected)
                {
                    GameState.DoLogin(txtUsername.Text, txtPassword.Text);
                }
            };
            this.Add(btnLogin);

            Button btnRemote = new Button(20, 1);
            btnRemote.Text = "Use Online Server";
            btnRemote.Position = new Point(width - 20, height - 1);
            btnRemote.Click += (s, a) =>
            {
                GameState.SetApiEndpoint("https://dev.spacerush.app/");
                this.Remove(btnRemote);
            };
            this.Add(btnRemote);

            Button btnLocalNoTls = new Button(26, 1);
            btnLocalNoTls.Text = "Use Local Https Server";
            btnLocalNoTls.Position = new Point(width - 26, height - 5);
            btnLocalNoTls.Click += (s, a) =>
            {
                GameState.SetApiEndpoint("https://localhost:5001/");
                this.Remove(btnLocalNoTls);
            };
            this.Add(btnLocalNoTls);

        }
    }
}
