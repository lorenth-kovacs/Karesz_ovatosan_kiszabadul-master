using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;


namespace Karesz
{
	partial class Form1
	{
        public Form1()
		{
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-us");
            InitializeComponent();
            Saját_InitializeComponent();
		}

        void InitializeComponent()
        {
			this.képkeret = new System.Windows.Forms.PictureBox();
			this.monitorpanel2 = new System.Windows.Forms.Panel();
			this.alsokameralabel = new System.Windows.Forms.Label();
			this.mivanalattamnagyításkeret = new System.Windows.Forms.PictureBox();
			this.mivanitttextbox = new System.Windows.Forms.TextBox();
			this.mivanalattamlabel = new System.Windows.Forms.Label();
			this.pozícióYtextbox = new System.Windows.Forms.TextBox();
			this.pozícióXtextbox = new System.Windows.Forms.TextBox();
			this.pályagomb = new System.Windows.Forms.Button();
			this.pályatextbox = new System.Windows.Forms.TextBox();
			this.pályalabel = new System.Windows.Forms.Label();
			this.időtextbox = new System.Windows.Forms.TextBox();
			this.zsebeibenlabel = new System.Windows.Forms.Label();
			this.hótextbox = new System.Windows.Forms.TextBox();
			this.sárgatextbox = new System.Windows.Forms.TextBox();
			this.hólabel = new System.Windows.Forms.Label();
			this.sárgalabel = new System.Windows.Forms.Label();
			this.zöldtextbox = new System.Windows.Forms.TextBox();
			this.pirostextbox = new System.Windows.Forms.TextBox();
			this.feketetextbox = new System.Windows.Forms.TextBox();
			this.zöldlabel = new System.Windows.Forms.Label();
			this.piroslabel = new System.Windows.Forms.Label();
			this.feketelabel = new System.Windows.Forms.Label();
			this.ultrahangtextbox = new System.Windows.Forms.TextBox();
			this.hőtextbox = new System.Windows.Forms.TextBox();
			this.karesznagyításkeret = new System.Windows.Forms.PictureBox();
			this.ultrahanglabel = new System.Windows.Forms.Label();
			this.hőmérsékletlabel = new System.Windows.Forms.Label();
			this.pozíciólabel = new System.Windows.Forms.Label();
			this.idolabellabel = new System.Windows.Forms.Label();
			this.robotnévlabel = new System.Windows.Forms.Label();
			this.következőrobotgomb = new System.Windows.Forms.Button();
			this.elozorobotgomb = new System.Windows.Forms.Button();
			this.startgomb2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.képkeret)).BeginInit();
			this.monitorpanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mivanalattamnagyításkeret)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.karesznagyításkeret)).BeginInit();
			this.SuspendLayout();
			// 
			// képkeret
			// 
			this.képkeret.BackColor = System.Drawing.Color.LightGray;
			this.képkeret.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.képkeret.Location = new System.Drawing.Point(12, 12);
			this.képkeret.Name = "képkeret";
			this.képkeret.Size = new System.Drawing.Size(984, 744);
			this.képkeret.TabIndex = 0;
			this.képkeret.TabStop = false;

			this.képkeret.Paint += new System.Windows.Forms.PaintEventHandler(this.képkeret_Paint);
			this.képkeret.MouseDown += new System.Windows.Forms.MouseEventHandler(this.képkeret_MouseDown);
			// 
			// monitorpanel2
			// 
			this.monitorpanel2.BackColor = System.Drawing.SystemColors.ScrollBar;
			this.monitorpanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.monitorpanel2.Controls.Add(this.alsokameralabel);
			this.monitorpanel2.Controls.Add(this.mivanalattamnagyításkeret);
			this.monitorpanel2.Controls.Add(this.mivanitttextbox);
			this.monitorpanel2.Controls.Add(this.mivanalattamlabel);
			this.monitorpanel2.Controls.Add(this.pozícióYtextbox);
			this.monitorpanel2.Controls.Add(this.pozícióXtextbox);
			this.monitorpanel2.Controls.Add(this.pályagomb);
			this.monitorpanel2.Controls.Add(this.pályatextbox);
			this.monitorpanel2.Controls.Add(this.pályalabel);
			this.monitorpanel2.Controls.Add(this.időtextbox);
			this.monitorpanel2.Controls.Add(this.zsebeibenlabel);
			this.monitorpanel2.Controls.Add(this.hótextbox);
			this.monitorpanel2.Controls.Add(this.sárgatextbox);
			this.monitorpanel2.Controls.Add(this.hólabel);
			this.monitorpanel2.Controls.Add(this.sárgalabel);
			this.monitorpanel2.Controls.Add(this.zöldtextbox);
			this.monitorpanel2.Controls.Add(this.pirostextbox);
			this.monitorpanel2.Controls.Add(this.feketetextbox);
			this.monitorpanel2.Controls.Add(this.zöldlabel);
			this.monitorpanel2.Controls.Add(this.piroslabel);
			this.monitorpanel2.Controls.Add(this.feketelabel);
			this.monitorpanel2.Controls.Add(this.ultrahangtextbox);
			this.monitorpanel2.Controls.Add(this.hőtextbox);
			this.monitorpanel2.Controls.Add(this.karesznagyításkeret);
			this.monitorpanel2.Controls.Add(this.ultrahanglabel);
			this.monitorpanel2.Controls.Add(this.hőmérsékletlabel);
			this.monitorpanel2.Controls.Add(this.pozíciólabel);
			this.monitorpanel2.Controls.Add(this.idolabellabel);
			this.monitorpanel2.Controls.Add(this.robotnévlabel);
			this.monitorpanel2.Controls.Add(this.következőrobotgomb);
			this.monitorpanel2.Controls.Add(this.elozorobotgomb);
			this.monitorpanel2.Controls.Add(this.startgomb2);
			this.monitorpanel2.Location = new System.Drawing.Point(1004, 12);
			this.monitorpanel2.Name = "monitorpanel2";
			this.monitorpanel2.Size = new System.Drawing.Size(159, 743);
			this.monitorpanel2.TabIndex = 1;
			// 
			// alsokameralabel
			// 
			this.alsokameralabel.AutoSize = true;
			this.alsokameralabel.Location = new System.Drawing.Point(20, 322);
			this.alsokameralabel.Name = "alsokameralabel";
			this.alsokameralabel.Size = new System.Drawing.Size(74, 16);
			this.alsokameralabel.TabIndex = 38;
			this.alsokameralabel.Text = "Kiskamera:";
			// 
			// mivanalattamnagyításkeret
			// 
			this.mivanalattamnagyításkeret.BackColor = System.Drawing.Color.White;
			this.mivanalattamnagyításkeret.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mivanalattamnagyításkeret.Location = new System.Drawing.Point(84, 297);
			this.mivanalattamnagyításkeret.Name = "mivanalattamnagyításkeret";
			this.mivanalattamnagyításkeret.Size = new System.Drawing.Size(64, 64);
			this.mivanalattamnagyításkeret.TabIndex = 37;
			this.mivanalattamnagyításkeret.TabStop = false;
			this.mivanalattamnagyításkeret.Paint += new System.Windows.Forms.PaintEventHandler(this.mivanalattamnagyításkeret_Paint);
			// 
			// mivanitttextbox
			// 
			this.mivanitttextbox.Location = new System.Drawing.Point(84, 271);
			this.mivanitttextbox.Name = "mivanitttextbox";
			this.mivanitttextbox.Size = new System.Drawing.Size(64, 22);
			this.mivanitttextbox.TabIndex = 36;
			this.mivanitttextbox.Text = "semmi";
			this.mivanitttextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// mivanalattamlabel
			// 
			this.mivanalattamlabel.AutoSize = true;
			this.mivanalattamlabel.Location = new System.Drawing.Point(20, 274);
			this.mivanalattamlabel.Name = "mivanalattamlabel";
			this.mivanalattamlabel.Size = new System.Drawing.Size(61, 16);
			this.mivanalattamlabel.TabIndex = 35;
			this.mivanalattamlabel.Text = "Mi van itt:";
			// 
			// pozícióYtextbox
			// 
			this.pozícióYtextbox.Location = new System.Drawing.Point(117, 195);
			this.pozícióYtextbox.Name = "pozícióYtextbox";
			this.pozícióYtextbox.Size = new System.Drawing.Size(31, 22);
			this.pozícióYtextbox.TabIndex = 34;
			this.pozícióYtextbox.Text = "5";
			this.pozícióYtextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// pozícióXtextbox
			// 
			this.pozícióXtextbox.Location = new System.Drawing.Point(84, 195);
			this.pozícióXtextbox.Name = "pozícióXtextbox";
			this.pozícióXtextbox.Size = new System.Drawing.Size(31, 22);
			this.pozícióXtextbox.TabIndex = 33;
			this.pozícióXtextbox.Text = "12";
			this.pozícióXtextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// pályagomb
			// 
			this.pályagomb.Location = new System.Drawing.Point(9, 705);
			this.pályagomb.Name = "pályagomb";
			this.pályagomb.Size = new System.Drawing.Size(140, 26);
			this.pályagomb.TabIndex = 32;
			this.pályagomb.Text = "pályát betölt";
			this.pályagomb.UseVisualStyleBackColor = true;
			this.pályagomb.Click += new System.EventHandler(this.pályagomb_Click);
			// 
			// pályatextbox
			// 
			this.pályatextbox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.pályatextbox.Location = new System.Drawing.Point(44, 675);
			this.pályatextbox.Name = "pályatextbox";
			this.pályatextbox.Size = new System.Drawing.Size(105, 23);
			this.pályatextbox.TabIndex = 31;
			this.pályatextbox.Text = "palya01.txt";
			this.pályatextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.pályatextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pályatextbox_KeyDown);
			// 
			// pályalabel
			// 
			this.pályalabel.AutoSize = true;
			this.pályalabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.pályalabel.Location = new System.Drawing.Point(1, 676);
			this.pályalabel.Name = "pályalabel";
			this.pályalabel.Size = new System.Drawing.Size(50, 20);
			this.pályalabel.TabIndex = 30;
			this.pályalabel.Text = "Pálya";
			// 
			// időtextbox
			// 
			this.időtextbox.Location = new System.Drawing.Point(84, 66);
			this.időtextbox.Name = "időtextbox";
			this.időtextbox.Size = new System.Drawing.Size(64, 22);
			this.időtextbox.TabIndex = 29;
			this.időtextbox.Text = "0";
			this.időtextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// zsebeibenlabel
			// 
			this.zsebeibenlabel.AutoSize = true;
			this.zsebeibenlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.zsebeibenlabel.Location = new System.Drawing.Point(3, 466);
			this.zsebeibenlabel.Name = "zsebeibenlabel";
			this.zsebeibenlabel.Size = new System.Drawing.Size(172, 20);
			this.zsebeibenlabel.TabIndex = 28;
			this.zsebeibenlabel.Text = "Zsebeiben lévő kövek:";
			// 
			// hótextbox
			// 
			this.hótextbox.Location = new System.Drawing.Point(84, 604);
			this.hótextbox.Name = "hótextbox";
			this.hótextbox.Size = new System.Drawing.Size(64, 22);
			this.hótextbox.TabIndex = 26;
			this.hótextbox.Text = "5";
			this.hótextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sárgatextbox
			// 
			this.sárgatextbox.Location = new System.Drawing.Point(84, 578);
			this.sárgatextbox.Name = "sárgatextbox";
			this.sárgatextbox.Size = new System.Drawing.Size(64, 22);
			this.sárgatextbox.TabIndex = 25;
			this.sárgatextbox.Text = "13";
			this.sárgatextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// hólabel
			// 
			this.hólabel.AutoSize = true;
			this.hólabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.hólabel.ForeColor = System.Drawing.Color.White;
			this.hólabel.Location = new System.Drawing.Point(20, 605);
			this.hólabel.Name = "hólabel";
			this.hólabel.Size = new System.Drawing.Size(29, 20);
			this.hólabel.TabIndex = 23;
			this.hólabel.Text = "hó";
			// 
			// sárgalabel
			// 
			this.sárgalabel.AutoSize = true;
			this.sárgalabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.sárgalabel.ForeColor = System.Drawing.Color.Yellow;
			this.sárgalabel.Location = new System.Drawing.Point(20, 579);
			this.sárgalabel.Name = "sárgalabel";
			this.sárgalabel.Size = new System.Drawing.Size(56, 20);
			this.sárgalabel.TabIndex = 22;
			this.sárgalabel.Text = "sárga";
			// 
			// zöldtextbox
			// 
			this.zöldtextbox.Location = new System.Drawing.Point(84, 552);
			this.zöldtextbox.Name = "zöldtextbox";
			this.zöldtextbox.Size = new System.Drawing.Size(64, 22);
			this.zöldtextbox.TabIndex = 21;
			this.zöldtextbox.Text = "17";
			this.zöldtextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// pirostextbox
			// 
			this.pirostextbox.Location = new System.Drawing.Point(84, 526);
			this.pirostextbox.Name = "pirostextbox";
			this.pirostextbox.Size = new System.Drawing.Size(64, 22);
			this.pirostextbox.TabIndex = 20;
			this.pirostextbox.Text = "10";
			this.pirostextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// feketetextbox
			// 
			this.feketetextbox.Location = new System.Drawing.Point(84, 501);
			this.feketetextbox.Name = "feketetextbox";
			this.feketetextbox.Size = new System.Drawing.Size(64, 22);
			this.feketetextbox.TabIndex = 19;
			this.feketetextbox.Text = "20";
			this.feketetextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// zöldlabel
			// 
			this.zöldlabel.AutoSize = true;
			this.zöldlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.zöldlabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.zöldlabel.Location = new System.Drawing.Point(20, 553);
			this.zöldlabel.Name = "zöldlabel";
			this.zöldlabel.Size = new System.Drawing.Size(44, 20);
			this.zöldlabel.TabIndex = 18;
			this.zöldlabel.Text = "szín";
			// 
			// piroslabel
			// 
			this.piroslabel.AutoSize = true;
			this.piroslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.piroslabel.ForeColor = System.Drawing.Color.Red;
			this.piroslabel.Location = new System.Drawing.Point(20, 527);
			this.piroslabel.Name = "piroslabel";
			this.piroslabel.Size = new System.Drawing.Size(51, 20);
			this.piroslabel.TabIndex = 17;
			this.piroslabel.Text = "piros";
			// 
			// feketelabel
			// 
			this.feketelabel.AutoSize = true;
			this.feketelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.feketelabel.Location = new System.Drawing.Point(20, 502);
			this.feketelabel.Name = "feketelabel";
			this.feketelabel.Size = new System.Drawing.Size(60, 20);
			this.feketelabel.TabIndex = 16;
			this.feketelabel.Text = "fekete";
			// 
			// ultrahangtextbox
			// 
			this.ultrahangtextbox.Location = new System.Drawing.Point(84, 246);
			this.ultrahangtextbox.Name = "ultrahangtextbox";
			this.ultrahangtextbox.Size = new System.Drawing.Size(64, 22);
			this.ultrahangtextbox.TabIndex = 15;
			this.ultrahangtextbox.Text = "17";
			this.ultrahangtextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// hőtextbox
			// 
			this.hőtextbox.Location = new System.Drawing.Point(84, 221);
			this.hőtextbox.Name = "hőtextbox";
			this.hőtextbox.Size = new System.Drawing.Size(64, 22);
			this.hőtextbox.TabIndex = 14;
			this.hőtextbox.Text = "200";
			this.hőtextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// karesznagyításkeret
			// 
			this.karesznagyításkeret.Location = new System.Drawing.Point(84, 367);
			this.karesznagyításkeret.Name = "karesznagyításkeret";
			this.karesznagyításkeret.Size = new System.Drawing.Size(64, 64);
			this.karesznagyításkeret.TabIndex = 12;
			this.karesznagyításkeret.TabStop = false;
			this.karesznagyításkeret.Click += new System.EventHandler(this.karesznagyításkeret_Click);
			// 
			// ultrahanglabel
			// 
			this.ultrahanglabel.AutoSize = true;
			this.ultrahanglabel.Location = new System.Drawing.Point(20, 249);
			this.ultrahanglabel.Name = "ultrahanglabel";
			this.ultrahanglabel.Size = new System.Drawing.Size(68, 16);
			this.ultrahanglabel.TabIndex = 10;
			this.ultrahanglabel.Text = "Ultrahang:";
			// 
			// hőmérsékletlabel
			// 
			this.hőmérsékletlabel.AutoSize = true;
			this.hőmérsékletlabel.Location = new System.Drawing.Point(20, 224);
			this.hőmérsékletlabel.Name = "hőmérsékletlabel";
			this.hőmérsékletlabel.Size = new System.Drawing.Size(28, 16);
			this.hőmérsékletlabel.TabIndex = 8;
			this.hőmérsékletlabel.Text = "Hő:";
			// 
			// pozíciólabel
			// 
			this.pozíciólabel.AutoSize = true;
			this.pozíciólabel.Location = new System.Drawing.Point(20, 202);
			this.pozíciólabel.Name = "pozíciólabel";
			this.pozíciólabel.Size = new System.Drawing.Size(57, 16);
			this.pozíciólabel.TabIndex = 6;
			this.pozíciólabel.Text = "Pozíció: ";
			// 
			// idolabellabel
			// 
			this.idolabellabel.AutoSize = true;
			this.idolabellabel.Location = new System.Drawing.Point(20, 69);
			this.idolabellabel.Name = "idolabellabel";
			this.idolabellabel.Size = new System.Drawing.Size(63, 16);
			this.idolabellabel.TabIndex = 4;
			this.idolabellabel.Text = "óra ideje:";
			// 
			// robotnévlabel
			// 
			this.robotnévlabel.AutoSize = true;
			this.robotnévlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.robotnévlabel.Location = new System.Drawing.Point(40, 157);
			this.robotnévlabel.Name = "robotnévlabel";
			this.robotnévlabel.Size = new System.Drawing.Size(95, 25);
			this.robotnévlabel.TabIndex = 3;
			this.robotnévlabel.Text = "0. Karesz";
			// 
			// következőrobotgomb
			// 
			this.következőrobotgomb.Location = new System.Drawing.Point(84, 101);
			this.következőrobotgomb.Name = "következőrobotgomb";
			this.következőrobotgomb.Size = new System.Drawing.Size(65, 43);
			this.következőrobotgomb.TabIndex = 2;
			this.következőrobotgomb.Text = "következő robot";
			this.következőrobotgomb.UseVisualStyleBackColor = true;
			this.következőrobotgomb.Click += new System.EventHandler(this.következőrobotgomb_Click);
			// 
			// elozorobotgomb
			// 
			this.elozorobotgomb.Location = new System.Drawing.Point(9, 101);
			this.elozorobotgomb.Name = "elozorobotgomb";
			this.elozorobotgomb.Size = new System.Drawing.Size(65, 43);
			this.elozorobotgomb.TabIndex = 1;
			this.elozorobotgomb.Text = "előző robot";
			this.elozorobotgomb.UseVisualStyleBackColor = true;
			this.elozorobotgomb.Click += new System.EventHandler(this.elozorobotgomb_Click);
			// 
			// startgomb2
			// 
			this.startgomb2.Location = new System.Drawing.Point(8, 10);
			this.startgomb2.Name = "startgomb2";
			this.startgomb2.Size = new System.Drawing.Size(140, 43);
			this.startgomb2.TabIndex = 0;
			this.startgomb2.Text = "START";
			this.startgomb2.UseVisualStyleBackColor = true;
			this.startgomb2.Click += new System.EventHandler(this.startgomb2_Click);
			// 
			// Form1
			// 
			this.ClientSize = new System.Drawing.Size(1177, 769);
			this.Controls.Add(this.monitorpanel2);
			this.Controls.Add(this.képkeret);
			this.Name = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.képkeret)).EndInit();
			this.monitorpanel2.ResumeLayout(false);
			this.monitorpanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.mivanalattamnagyításkeret)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.karesznagyításkeret)).EndInit();
			this.ResumeLayout(false);

        }

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

        private PictureBox képkeret;
        private Panel monitorpanel2;
        private Button startgomb2;
        private Button következőrobotgomb;
        private Button elozorobotgomb;
        private Label robotnévlabel;
        private Label pozíciólabel;
        private Label idolabellabel;
        private Label hőmérsékletlabel;
        private TextBox időtextbox;
        private Label zsebeibenlabel;
        private TextBox hótextbox;
        private TextBox sárgatextbox;
        private Label hólabel;
        private Label sárgalabel;
        private TextBox zöldtextbox;
        private TextBox pirostextbox;
        private TextBox feketetextbox;
        private Label zöldlabel;
        private Label piroslabel;
        private Label feketelabel;
        private TextBox ultrahangtextbox;
        private TextBox hőtextbox;
        private PictureBox karesznagyításkeret;
        private Label ultrahanglabel;
        private Button pályagomb;
        private TextBox pályatextbox;
        private Label pályalabel;
        private TextBox pozícióYtextbox;
        private TextBox pozícióXtextbox;
        private PictureBox mivanalattamnagyításkeret;
        private TextBox mivanitttextbox;
        private Label mivanalattamlabel;
        private Label alsokameralabel;
    }
}