namespace Zomb
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelAmmo = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelHealth = new System.Windows.Forms.Label();
            this.barHealth = new System.Windows.Forms.ProgressBar();
            this.boxPlayer = new System.Windows.Forms.PictureBox();
            this.labelZombie = new System.Windows.Forms.Label();
            this.labelGameOver = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.boxPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // labelAmmo
            // 
            this.labelAmmo.AutoSize = true;
            this.labelAmmo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAmmo.ForeColor = System.Drawing.Color.Transparent;
            this.labelAmmo.Location = new System.Drawing.Point(10, 16);
            this.labelAmmo.Name = "labelAmmo";
            this.labelAmmo.Size = new System.Drawing.Size(83, 20);
            this.labelAmmo.TabIndex = 0;
            this.labelAmmo.Text = "Lőszer: 0";
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScore.ForeColor = System.Drawing.Color.Transparent;
            this.labelScore.Location = new System.Drawing.Point(380, 16);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(85, 20);
            this.labelScore.TabIndex = 1;
            this.labelScore.Text = "Pontok: 0";
            // 
            // labelHealth
            // 
            this.labelHealth.AutoSize = true;
            this.labelHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHealth.ForeColor = System.Drawing.Color.Transparent;
            this.labelHealth.Location = new System.Drawing.Point(619, 16);
            this.labelHealth.Name = "labelHealth";
            this.labelHealth.Size = new System.Drawing.Size(77, 20);
            this.labelHealth.TabIndex = 2;
            this.labelHealth.Text = "Életerő: ";
            // 
            // barHealth
            // 
            this.barHealth.Location = new System.Drawing.Point(713, 13);
            this.barHealth.Name = "barHealth";
            this.barHealth.Size = new System.Drawing.Size(199, 23);
            this.barHealth.TabIndex = 3;
            // 
            // boxPlayer
            // 
            this.boxPlayer.Image = ((System.Drawing.Image)(resources.GetObject("boxPlayer.Image")));
            this.boxPlayer.Location = new System.Drawing.Point(410, 539);
            this.boxPlayer.Name = "boxPlayer";
            this.boxPlayer.Size = new System.Drawing.Size(71, 100);
            this.boxPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.boxPlayer.TabIndex = 4;
            this.boxPlayer.TabStop = false;
            // 
            // labelZombie
            // 
            this.labelZombie.AutoSize = true;
            this.labelZombie.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZombie.ForeColor = System.Drawing.Color.Transparent;
            this.labelZombie.Location = new System.Drawing.Point(191, 16);
            this.labelZombie.Name = "labelZombie";
            this.labelZombie.Size = new System.Drawing.Size(87, 20);
            this.labelZombie.TabIndex = 5;
            this.labelZombie.Text = "Zombik: 0";
            // 
            // labelGameOver
            // 
            this.labelGameOver.AutoSize = true;
            this.labelGameOver.Location = new System.Drawing.Point(285, 253);
            this.labelGameOver.Name = "labelGameOver";
            this.labelGameOver.Size = new System.Drawing.Size(0, 13);
            this.labelGameOver.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ClientSize = new System.Drawing.Size(924, 661);
            this.Controls.Add(this.labelGameOver);
            this.Controls.Add(this.labelZombie);
            this.Controls.Add(this.boxPlayer);
            this.Controls.Add(this.barHealth);
            this.Controls.Add(this.labelHealth);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.labelAmmo);
            this.Name = "Form1";
            this.Text = "Zomb";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyUp);
            ((System.ComponentModel.ISupportInitialize)(this.boxPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAmmo;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelHealth;
        private System.Windows.Forms.ProgressBar barHealth;
        private System.Windows.Forms.PictureBox boxPlayer;
        private System.Windows.Forms.Label labelZombie;
        private System.Windows.Forms.Label labelGameOver;
    }
}

