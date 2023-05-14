using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zomb.Properties;

namespace Zomb
{
    public enum Direction { Up, Down, Left, Right }
    public partial class Form1 : Form
    {
        internal bool GameOver;
        internal int ZombieSpeed = 4;
        internal int MedkitStrength = 50;
        internal int BombInterval = 30000;
        internal int SupplyInterval = 6000;
        internal int ZombieInterval = 2500;
        internal Random rng = new Random();
        internal static List<PictureBox> Zombies = new List<PictureBox>();
        internal System.Windows.Forms.Timer blinker = new System.Windows.Forms.Timer();
        internal System.Windows.Forms.Timer Timer = new System.Windows.Forms.Timer();
        internal System.Windows.Forms.Timer SupplyTimer = new System.Windows.Forms.Timer();
        internal System.Windows.Forms.Timer ZombieTimer = new System.Windows.Forms.Timer();
        internal System.Windows.Forms.Timer BombTimer = new System.Windows.Forms.Timer();

        internal void PlaySound(Stream stream)
        {
            new SoundPlayer(stream).Play();
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelGameOver.Visible = false;
            labelGameOver.ForeColor = Color.Red;
            labelGameOver.Font = new Font("Times", 34, FontStyle.Bold);
            labelGameOver.Text = "Game Over";
            labelGameOver.Location = new Point((ClientSize.Width / 2) - labelGameOver.Width / 2, (ClientSize.Height / 2) - labelGameOver.Height);

            Player.box = boxPlayer;
            Timer.Enabled = true;
            Timer.Interval = 20;
            Timer.Tick += TimerTick;
            blinker = new System.Windows.Forms.Timer()
            {
                Interval = 500,
                Enabled = true
            };
            blinker.Tick += blinker_Tick;
            SupplyTimer = new System.Windows.Forms.Timer()
            {
                Interval = SupplyInterval,
                Enabled = true
            };
            SupplyTimer.Tick += SpawnTimer_Tick;
            ZombieTimer = new System.Windows.Forms.Timer()
            {
                Interval = ZombieInterval,
                Enabled = true
            };
            ZombieTimer.Tick += ZombieTimer_Tick;
            BombTimer = new System.Windows.Forms.Timer() { Interval = BombInterval, Enabled = true };
            BombTimer.Tick += BombTimer_Tick;
            ResetGame();
        }

        private void BombTimer_Tick(object sender, EventArgs e)
        {
            PictureBox bomb = new PictureBox();
            bomb.Tag = "bomb";
            bomb.Left = rng.Next(40, ClientSize.Width - 80);
            bomb.Top = rng.Next(40, ClientSize.Height - 100);
            //medkit.Size = new Size(ClientSize.Width / 10, ClientSize.Height / 10);
            bomb.SizeMode = PictureBoxSizeMode.Zoom;
            bomb.Image = Resources.bomb;
            Controls.Add(bomb);
            Player.box.BringToFront();
        }

        private void ZombieTimer_Tick(object sender, EventArgs e)
        {
            SpawnZombies(2);
        }

        private void SpawnTimer_Tick(object sender, EventArgs e)
        {
            DropAmmo();
            SpawnMedkit();
        }

        private void SpawnMedkit()
        {
            PictureBox medkit = new PictureBox();
            medkit.Tag = "medkit";
            medkit.Left = rng.Next(40, ClientSize.Width - 80);
            medkit.Top = rng.Next(40, ClientSize.Height - 100);
            //medkit.Size = new Size(ClientSize.Width / 10, ClientSize.Height / 10);
            medkit.SizeMode = PictureBoxSizeMode.Zoom;
            medkit.Image = Resources.medkit;
            Controls.Add(medkit);
            Player.box.BringToFront();

            /*                PictureBox box;
                box = new PictureBox();
                box.Tag = "zombie";
                box.Image = Resources.zombie_down;
                box.Left = rng.Next(10, ClientSize.Width - 100);
                box.Top = rng.Next(50, ClientSize.Height - 100);
                box.SizeMode = PictureBoxSizeMode.AutoSize;
                Zombies.Add(box);
                Controls.Add(box);
                Player.box.BringToFront();*/
        }

        private void blinker_Tick(object sender, EventArgs e)
        {
            if (Player.Ammunition <= 5)
            {
                if (labelAmmo.ForeColor == Color.White)
                    labelAmmo.ForeColor = Color.Red;
                else
                    labelAmmo.ForeColor = Color.White;
            }

        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (Player.IsAlive)
            {
                barHealth.Value = Player.Health;
                HandleMovement();
            }
            else
            {
                // Game Over
                PlaySound(Resources.sound_player_death);
                GameOver = true;
                labelGameOver.Visible = true;
                labelGameOver.BringToFront();
                Player.box.Image = Resources.player_dead;
                ZombieTimer.Stop();
                SupplyTimer.Stop();
                BombTimer.Stop();
                Timer.Stop();
                blinker.Stop();
                ClearEntities();
            }
            labelAmmo.Text = $"Lőszer: {Player.Ammunition}";
            labelScore.Text = $"Pontok: {Player.Score}";
            labelZombie.Text = $"Zombik: {Zombies.Count}";
            foreach (Control x in Controls)
            {
                if (x is PictureBox)
                {
                    switch ((string)x.Tag)
                    {
                        case "bomb":
                            if (Player.box.Bounds.IntersectsWith(x.Bounds))
                            {
                                Controls.Remove(x);
                                ((PictureBox)x).Dispose();
                                ClearEntities(true);
                            }
                            break;
                        case "ammo":
                            if (Player.box.Bounds.IntersectsWith(x.Bounds))
                            {
                                Controls.Remove(x);
                                ((PictureBox)x).Dispose();
                                Player.Ammunition += 5;
                                labelAmmo.ForeColor = Color.White;
                            }
                            break;
                        case "zombie":
                            if (Player.box.Bounds.IntersectsWith(x.Bounds))
                            {
                                Player.Health -= 1;
                            }

                            if (x.Left > Player.box.Left)
                            {
                                x.Left -= ZombieSpeed;
                                ((PictureBox)x).Image = Resources.zombie_left;
                            }
                            if (x.Left < Player.box.Left)
                            {
                                x.Left += ZombieSpeed;
                                ((PictureBox)x).Image = Resources.zombie_right;
                            }
                            if (x.Top > Player.box.Top)
                            {
                                x.Top -= ZombieSpeed;
                                ((PictureBox)x).Image = Resources.zombie_up;
                            }
                            if (x.Top < Player.box.Top)
                            {
                                x.Top += ZombieSpeed;
                                ((PictureBox)x).Image = Resources.zombie_down;
                            }
                            break;
                        case "medkit":
                            if (Player.box.Bounds.IntersectsWith(x.Bounds))
                            {
                                Controls.Remove(x);
                                ((PictureBox)x).Dispose();
                                if (Player.Health + MedkitStrength >= 100) Player.Health = 100;
                                else Player.Health += MedkitStrength;
                            }
                            break;
                    }
                }

                foreach (Control y in Controls)
                {
                    if (y is PictureBox && (string)y.Tag == "bullet" && x is PictureBox && (string)x.Tag == "zombie")
                    {
                        if (x.Bounds.IntersectsWith(y.Bounds))
                        {
                            PlaySound(Resources.sound_death);
                            Player.Score++;
                            Controls.Remove(y);
                            ((PictureBox)y).Dispose();
                            Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            Zombies.Remove(((PictureBox)x));
                        }
                    }

                }
            }
        }

        private void HandleMovement()
        {
            if (Player.Move && Player.Direction == Direction.Left && Player.box.Left > 0)
            {
                Player.box.Left -= Player.Speed;
            }
            if (Player.Move && Player.Direction == Direction.Right && Player.box.Width + Player.box.Left < ClientSize.Width)
            {
                Player.box.Left += Player.Speed;
            }

            if (Player.Move && Player.Direction == Direction.Up && Player.box.Top > 50)
            {
                Player.box.Top -= Player.Speed;
            }

            if (Player.Move && Player.Direction == Direction.Down && Player.box.Top
                + Player.box.Height < ClientSize.Height)
            {
                Player.box.Top += Player.Speed;
            }

        }

        private void ClearEntities(bool bomb = false)
        {
            if (!bomb)
            {
                foreach (Control x in Controls)
                {
                    if (x is PictureBox)
                    {
                        switch ((string)x.Tag)
                        {
                            case "ammo":
                            case "medkit":
                            case "bomb":
                                Controls.Remove(x);
                                ((PictureBox)x).Dispose();
                                break;
                        }
                    }
                }
            }
            if (bomb) Player.Score += (ushort)Zombies.Count; ;
            foreach (PictureBox box in Zombies)
            {
                Controls.Remove(box);
            }
            Zombies.Clear();
        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (!GameOver)
            {

                switch (e.KeyCode)
                {
                    case Keys.Left:
                        Player.Direction = Direction.Left;
                        Player.box.Image = Resources.player_left;
                        Player.Move = true;
                        break;
                    case Keys.Right:
                        Player.Direction = Direction.Right;
                        Player.box.Image = Resources.player_right;
                        Player.Move = true;
                        break;
                    case Keys.Down:
                        Player.Direction = Direction.Down;
                        Player.box.Image = Resources.player_down;
                        Player.Move = true;
                        break;
                    case Keys.Up:
                        Player.Direction = Direction.Up;
                        Player.box.Image = Resources.player_up;
                        Player.Move = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void keyUp(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Left:
                    Player.Move = false;
                    break;
                case Keys.Right:
                    Player.Move = false;
                    break;
                case Keys.Down:
                    Player.Move = false;
                    break;
                case Keys.Up:
                    Player.Move = false;
                    break;
                case Keys.Space:
                    if (GameOver) { ResetGame(); return; }
                    if (Player.Ammunition > 0)
                    { new Bullet(this); Player.Ammunition--; }
                    break;
            }
        }

        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Resources.ammo;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = rng.Next(10, this.ClientSize.Width - ammo.Width - 10);
            ammo.Top = rng.Next(40, ClientSize.Height - ammo.Height - 10);
            ammo.Tag = "ammo";
            Controls.Add(ammo);

            ammo.BringToFront();
            Player.box.BringToFront();
        }

        private void SpawnZombies(int amount = 2)
        {
            if (!GameOver)
                for (int i = 0; i < amount; i++)
                {
                    PictureBox box = new PictureBox
                    {
                        Tag = "zombie",
                        Image = Resources.zombie_down,
                        Left = rng.Next(10, ClientSize.Width - 100),
                        Top = rng.Next(50, ClientSize.Height - 100),
                        SizeMode = PictureBoxSizeMode.AutoSize
                    };
                    Zombies.Add(box);
                    Controls.Add(box);
                    Player.box.BringToFront();
                }
        }

        private void ResetGame()
        {
            ClearEntities();
            Player.box.Location = new Point(410, 540);
            Player.box.Image = Resources.player_up;
            GameOver = false;
            labelGameOver.Visible = false;
            Player.Score = 0;
            Player.Ammunition = 10;
            Player.Speed = 15;
            Player.Health = 100;
            Player.Direction = Direction.Up;
            labelAmmo.ForeColor = Color.White;
            Timer.Start();
            SupplyTimer.Start();
            BombTimer.Start();
            blinker.Start();
            ZombieTimer.Start();
            PlaySound(Resources.newgame);
        }
    }
}