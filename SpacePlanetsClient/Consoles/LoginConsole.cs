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

            Button btnLocal = new Button(20, 1);
            btnLocal.Text = "Use Online Server";
            btnLocal.Position = new Point(width - 20, height - 1);
            btnLocal.Click += (s, a) =>
            {
                GameState.SetApiEndpoint("https://dev.spacerush.app/");
                this.Remove(btnLocal);
            };
            this.Add(btnLocal);

            Button btnLogin = new Button(10, 3);
            btnLogin.Text = "Log in";
            btnLogin.Position = new Point(width - 10, height - 4);
            btnLogin.Click += (s, a) =>
            {
                GameState.DoLogin(txtUsername.Text, txtPassword.Text);
            };
            this.Add(btnLogin);
        }
    }
}
