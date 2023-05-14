using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zomb
{
    internal class Bullet
    {
        public Direction direction;
        public int bulletLeft;
        public int bulletTop;
        private int Speed;
        private PictureBox bulletBox;
        private Timer bulletTimer;
        private Form form;

        public Bullet(Form Form)
        {
            form = Form;
            bulletTimer = new Timer();
            bulletBox = new PictureBox();
            Speed = 20;
            bulletBox.BackColor = Color.White;
            bulletBox.Size = new Size(5, 5);
            bulletBox.Tag = "bullet";
            direction = Player.Direction;
            bulletBox.Left = bulletLeft = Player.box.Left + (Player.box.Width / 2);
            bulletBox.Top = bulletTop = Player.box.Top + (Player.box.Height / 2);
            bulletBox.BringToFront();
            form.Controls.Add(bulletBox);

            bulletTimer.Interval = Speed;
            bulletTimer.Tick += new EventHandler(BulletTick);
            bulletTimer.Start();
        }

        private void BulletTick(object sender, EventArgs e)
        {
            switch (direction)
            {
                case Direction.Left:
                    bulletBox.Left -= Speed;
                    break;
                case Direction.Right:
                    bulletBox.Left += Speed;
                    break;
                case Direction.Up:
                    bulletBox.Top -= Speed;
                    break;
                case Direction.Down:
                    bulletBox.Top += Speed;
                    break;
            }

            if (bulletBox.Left < 10 || bulletBox.Left > form.ClientSize.Width - 10
                || bulletBox.Top < 10 || bulletBox.Top > form.ClientSize.Height - 10)
            {
                bulletTimer.Stop();
                bulletTimer.Dispose();
                bulletBox.Dispose();
                bulletTimer = null;
                bulletBox = null;
            }
        }
    }
}