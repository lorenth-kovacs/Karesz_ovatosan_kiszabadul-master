using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karesz
{
	public partial class Form1
	{
		#region Konstansok

		const int várakozás = 5;

		//pályatartalom
		const int üres = 0;
		const int fal = 1;
		const int nincs_pálya = -1;
		const int láva = 7;

		//forgásirány
		const int jobbra = 1;
		const int balra = -1;

		//színek				
		const int fekete = 2;
		const int piros = 3;
		const int zöld = 4;
		const int sárga = 5;
		const int hó = 6;
		const int víz = 8;

		#endregion

		#region Statikus változók

		static int idő = 0;
		static int[] színkódok = new int[] { üres, fal, fekete, piros, zöld, sárga, hó, láva, víz };
		static string[] színnév = new string[] { "üres", "fal", "fekete", "piros", "zöld", "sárga", "hó", "láva", "víz" };
		static Color[] színek = { Color.White, Color.Brown, Color.Black, Color.Red, Color.Green, Color.Yellow, Color.White, Color.Orange, Color.Blue };
		static Random véletlen = new Random();

		#endregion

		#region Form tulajdonságai
		bool form_teljesen_kész = false;
		TextBox[] textboxok;
		TextBox[] kőtextboxok;
		Button[] gombok;
		Pálya pálya;
		bool enged;
		bool Enged
		{
			get => enged;
			set
			{
				foreach (TextBox textbox in textboxok)
					textbox.Enabled = value;
				foreach (Button gomb in gombok)
					gomb.Enabled = value;
				enged = value;
			}
		}
		#endregion

		#region Metódusok

		void Saját_InitializeComponent()
		{
			textboxok = new TextBox[]
			{
				időtextbox,
				pozícióXtextbox,
				pozícióYtextbox,
				hőtextbox,
				ultrahangtextbox,
				mivanitttextbox,
				feketetextbox,
				pirostextbox,
				zöldtextbox,
				sárgatextbox,
				hótextbox,
				pályatextbox
			};
			kőtextboxok = new TextBox[]
			{
				feketetextbox,
				pirostextbox,
				zöldtextbox,
				sárgatextbox,
				hótextbox
			};
			gombok = new Button[]
			{
				startgomb2,
				következőrobotgomb,
				elozorobotgomb,
				pályagomb,
			};
			pálya = new Pálya(this);
			pálya.Betölt();
			karesznagyításkeret.BackgroundImageLayout = ImageLayout.Stretch;
			foreach (TextBox textbox in textboxok.Where(t => t != pályatextbox))
				textbox.Enter += (s, e) => { textbox.Parent.Focus(); };

			TANÁR_ROBOTJAI();
			DIÁK_ROBOTJAI();
			if (betöltendő_pálya != "")
				Betölt(betöltendő_pálya);

			form_teljesen_kész = true;
			Robot.form = this;
		}

		void Frissít()
		{

			if (0 < Robot.ok_száma && form_teljesen_kész)
			{
				//Robot.megfigyelt = Robot.lista[Robot.megfigyeltindex];
				Control_Textjének_frissítése(robotnévlabel, Robot.akit_kiválasztottak.Név);
				Control_Textjének_frissítése(pozícióXtextbox, $"{Robot.akit_kiválasztottak.H.X}");
				Control_Textjének_frissítése(pozícióYtextbox, $"{Robot.akit_kiválasztottak.H.Y}");
				Control_Textjének_frissítése(időtextbox, $"{idő}");
				Control_Textjének_frissítése(hőtextbox, $"{Robot.akit_kiválasztottak.Hőmérő()}");
				Control_Textjének_frissítése(ultrahangtextbox, $"{Robot.akit_kiválasztottak.UltrahangSzenzor()}");
				for (int szín = 2; szín < 7; szín++)
					Control_Textjének_frissítése(kőtextboxok[szín - 2], $"{Robot.akit_kiválasztottak.Köveinek_száma_ebből(szín)}");
				karesznagyításkeret.BackgroundImage = Robot.akit_kiválasztottak.Iránykép();

				Control_Textjének_frissítése(mivanitttextbox, színnév[Robot.akit_kiválasztottak.Alatt_ez_van()]);

				Control_frissítése(monitorpanel2);
				Control_frissítése(képkeret);
				Control_frissítése(mivanalattamnagyításkeret);
			}
		}

		//        void Control_Textjének_frissítése(Control t, string s) => t.Invoke(new MethodInvoker(delegate () { t.Text = s; }));
		void Control_Textjének_frissítése(Control t, string s) => t.Text = s;
		//        void Control_frissítése(Control t) => t.Invoke(new MethodInvoker(delegate () { t.Refresh(); }));
		void Control_frissítése(Control t) => t.Refresh();

		#endregion

		#region Eseménykezelés

		/// <summary>
		/// Végrehajtja a feladatot
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void startgomb2_Click(object sender, EventArgs e)
		{
			Enged = false;
			Robot.Játék();
			Enged = true;
		}



		/// <summary>
		/// Az előző robotra vált
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void elozorobotgomb_Click(object sender, EventArgs e)
		{
			++Robot.megfigyeltindex;
			Frissít();
		}
		/// <summary>
		/// A következő robotra vált
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void következőrobotgomb_Click(object sender, EventArgs e)
		{
			--Robot.megfigyeltindex;
			Frissít();
		}
		/// <summary>
		/// pálya betöltése gombbal
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void pályagomb_Click(object sender, EventArgs e) =>
			pálya.Betölt(pályatextbox.Text);
		/// <summary>
		/// Ha valaki a pályára kattint, akkor Kareszt leteszi oda.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void képkeret_MouseDown(object sender, MouseEventArgs e)
		{
			if (0 < Robot.ok_száma)
			{
				Robot.akit_kiválasztottak.Teleport(e.X / pálya.L.X, e.Y / pálya.L.Y);
				képkeret.Refresh();
				Frissít();
			}
		}
		/// <summary>
		/// Lerajzolja a Pályánál található függvénnyel.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void képkeret_Paint(object sender, PaintEventArgs e) =>
			pálya.Rajz(e);
		void mivanalattamnagyításkeret_Paint(object sender, PaintEventArgs e)
		{
			if (0 < Robot.ok_száma)
				pálya.AlakRajz(Robot.akit_kiválasztottak.Alatt_ez_van(), e, 0, 0, new Vektor(mivanalattamnagyításkeret.Width - 2, mivanalattamnagyításkeret.Height - 2));
		}
		void karesznagyításkeret_Click(object sender, EventArgs e) =>
			Robot.akit_kiválasztottak.Fordulj(jobbra);
		void pályatextbox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				pályagomb.PerformClick();
		}

		#endregion
	}
}
