using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Karesz
{
	partial class Form1
	{
		/// <summary>
		/// A Pálya egy olyan class, amely azon túl, hogy megrajzolja a pályát,
		/// a rendelkezésre álló robotokat is felrajzolja a pályára. Rendelkezik továbbá azokkal a függvényekkel, amelyek
		/// kiszolgálják a robotok szenzorait és cselekvéseit. Illetve itt helyezkedik el a fájlból pályát betöltő metódus is.
		/// </summary>
		class Pálya
		{
			#region Tulajdonságok

			int X { get; set; }
			int Y { get; set; }
			Vektor l;
			public Vektor L { get => l; private set => l = value; }
			Brush[] tollkészlet;
			Pen vonalzósceruza;
			int[,] tábla;
			int[,] hőtábla;

			public int this[int a, int b]
			{
				get => this[Vektor.dobozba(new Vektor(0, 0), new Vektor(a, b), new Vektor(this.X, this.Y))];
				set => this[Vektor.dobozba(new Vektor(0, 0), new Vektor(a, b), new Vektor(this.X, this.Y))] = value;
			}

			public int this[Vektor v]
			{
				get => tábla[v.X, v.Y];
				set => tábla[v.X, v.Y] = value;
			}


			PictureBox képkeret { get => szülőform.képkeret; }
			Form1 szülőform;

			#endregion

			#region Konstruktorok

			Pálya(int X, int Y, Vektor L, Brush[] tollkészlet, Pen vonalzósceruza, int[,] tábla, int[,] hőtábla, Form1 szülőform)
			{
				this.X = X;
				this.Y = Y;
				this.l = L;
				this.tollkészlet = tollkészlet;
				this.vonalzósceruza = vonalzósceruza;
				this.tábla = tábla;
				this.hőtábla = hőtábla;
				this.szülőform = szülőform;
			}
			Pálya(int X, int Y, int lxy, Form1 szülőform) :
				this(X, Y, new Vektor(lxy, lxy), Új_tollkészlet(), Új_vonalzósceruza(), new int[X, Y], new int[X, Y], szülőform)
			{ }
			public Pálya(Form1 szülőform) :
				this(41, 31, 24, szülőform)
			{ }

			#endregion

			#region Segédmetódusok konstruktorokhoz

			static SolidBrush[] Új_tollkészlet()
			{
				SolidBrush[] tollkészlet = new SolidBrush[9];
				foreach (int szín in színkódok)
					tollkészlet[szín] = new SolidBrush(színek[szín]);
				return tollkészlet;
			}
			static Pen Új_vonalzósceruza() =>
				new Pen(new SolidBrush(Color.Gray), 1);

			#endregion

			#region Strukturális/matematikai segédmetódusok

			/// <summary>
			/// Megnézi, hogy értelmezhető-e a pályán az adott pont.
			/// </summary>
			/// <param name="V">A pont, amiről megvizsgáljuk, hogy rajta van-e a pályán vagy sem</param>
			/// <returns>Igaz, ha rajta van, hamis, ha nincs.</returns>
			public bool BenneVan(Vektor V) => 
				0 <= V.X && V.X < X && 0 <= V.Y && V.Y < Y;
			/// <summary>
			/// Visszaadja egy vektorral megadott ponton lévő entitás kódját.
			/// </summary>
			/// <param name="P">A vizsgálandó pozíció</param>
			/// <returns>Az itt lévő entitás kódja.</returns>
			private int Ha_van(Vektor P, int[,] m) => 
				BenneVan(P) ? m[P.X,P.Y] : -1;
			/// <summary>
			/// Felülírja a pálya egy adott pontját azzal az értékkel, amit megadunk.
			/// </summary>
			/// <param name="P"> Az itt lévő dolgot írja át</param>
			/// <param name="ez">Erre írja át</param>
			public void LegyenItt(Vektor P, int ez) => 
				tábla[P.X, P.Y] = ez;

			/// <summary>
			/// visszaadja az A vektorral megadott rácspont szomszédait.
			/// </summary>
			/// <param name="A"></param>
			/// <returns></returns>
			List<Vektor> Szomszédai(Vektor A)
			{
				List<Vektor> result = new List<Vektor>();
				if (0 < A.X) result.Add(A.Balra());
				if (A.X < X - 1) result.Add(A.Jobbra());
				if (0 < A.Y) result.Add(A.Lent());
				if (A.Y < Y - 1) result.Add(A.Fent());
				return result;
			}

			#endregion

			#region Szenzorillesztő metódusok

			/// <summary>
			/// Megadja, hogy mi van az adott pozíción
			/// </summary>
			/// <param name="P"></param>
			/// <returns></returns>
			public int MiVanItt(Vektor P) => 
				Ha_van(P, tábla);
			/// <summary>
			/// Megadja az adott pozíció hőmérsékletét
			/// </summary>
			/// <param name="P"></param>
			/// <returns></returns>
			public int Hőmérséklet(Vektor P) => 
				Ha_van(P, hőtábla);
			/// <summary>
			/// Ellenőrzi, hogy van-e kavics az adott pozíción.
			/// </summary>
			/// <param name="P">A vizsgálandó pozíció</param>
			/// <returns>igaz, ha van, hamis, ha nincs.</returns>
			public bool VanKavics(Vektor P) => 
				MiVanItt(P) > fal;

			#endregion

			#region Rajzolómetódusok

			void Négyzetrajz(PaintEventArgs e, int tollszínkód, int x, int y, Vektor m) =>
				e.Graphics.FillRectangle(tollkészlet[tollszínkód], x * m.X, y * m.Y, m.X, m.Y);
			void Körrajz(PaintEventArgs e, int tollszínkód, int x, int y, Vektor m) =>
				e.Graphics.FillEllipse(tollkészlet[tollszínkód], x * m.X + 2, y * m.Y + 2, m.X - 4, m.Y - 4);
			void Vonalrajz(PaintEventArgs e, int x1, int y1, int x2, int y2, Vektor m) =>
				e.Graphics.DrawLine(vonalzósceruza, x1 * m.X, y1 * m.Y, x2 * m.X, y2 * m.Y);
			/// <summary>
			/// Lerajzol mindent a pályán, amit csak lehetséges. Robotokat is beleértve.
			/// </summary>
			/// <param name="vászon"></param>
			/// <param name="e"></param>
			public void Rajz(PaintEventArgs e)
			{
				for (int y = 1; y < Y; ++y) Vonalrajz(e, 0, y, X, y, l); // vízszintes vonalak
				for (int x = 1; x < X; ++x) Vonalrajz(e, x, 0, x, Y, l); // függőleges vonalak
				for (int y = 0; y < Y; ++y)
					for (int x = 0; x < X; ++x)
						AlakRajz(tábla[x, y], e, x, y, l);

				foreach (Robot robot in Robot.lista)
					e.Graphics.DrawImageUnscaledAndClipped(robot.Iránykép(), new Rectangle(robot.H.X * l.X, robot.H.Y * l.Y, l.X, l.Y));

				foreach (Hógolyó hógolyó in Hógolyó.lista)
					Körrajz(e, 1, hógolyó.h.X, hógolyó.h.Y, hógolyó.v);
			}

			public void AlakRajz(int alakid, PaintEventArgs e, int x, int y, Vektor méret)
			{
				switch (alakid)
				{
					case fal:
					case láva:
					case víz:
						Négyzetrajz(e, alakid, x, y, méret);
						break;
					case fekete:
					case piros:
					case zöld:
					case sárga:
					case hó:
						Körrajz(e, alakid, x, y, méret);
						break;
				}
			}

			#endregion

			#region Pályageneráló metódusok (fájlból vagy anélkül)

			/// <summary>
			/// Üres pályát generál (valódi betöltés overloaddal történik)
			/// </summary>
			public void Betölt()
			{
				 // a szélére -1-et rakunk, a belsejébe nullát. // szerintem ez már rég nem így működik
				/*
				for (int x = 0; x < X; x++) tábla[x, 0] = -1;
				for (int x = 0; x < X; x++) tábla[x, Y - 1] = -1;
				for (int y = 1; y < Y - 1; y++) tábla[0, y] = -1;
				for (int y = 1; y < Y - 1; y++) tábla[X - 1, y] = -1;
				*/
				for (int y = 0; y < Y; ++y)
					for (int x = 0; x < X; ++x)
						tábla[x, y] = 0;

				Hőtérképezés();
				szülőform.Frissít();
			}
			/// <summary>
			/// Betölti a megadott elérési útvonalon lévő pályát. 
			/// </summary>
			/// <param name="fájlnév"></param>
			public void Betölt(string fájlnév)
			{
				if (fájlnév == "") { Betölt(); return; }
				try
				{
					StreamReader f = new StreamReader(fájlnév);
					for (int y = 0; y < Y; ++y)
					{
						string[] sor = f.ReadLine().Split('\t');
						for (int x = 0; x < X; ++x)
							tábla[x, y] = Convert.ToInt32(sor[x]);
					}
					f.Close();
				}
				catch (FileNotFoundException)
				{
					MessageBox.Show("Nincs meg a pálya!");
				}

				Hőtérképezés();
				szülőform.Frissít();
			}

			#endregion

			#region Hőtérképezés

			/// <summary>
			/// Az A hely felmelegíti a B helyet, ha melegebb.
			/// </summary>
			/// <param name="A"></param>
			/// <param name="B"></param>
			void Melegít(Vektor A, Vektor B)
			{
                if (200 + hőtábla[B.X, B.Y] < hőtábla[A.X,A.Y])
					hőtábla[B.X, B.Y] = hőtábla[A.X, A.Y] - 200;
			}

			/// <summary>
			/// az itt lévő helyet felmelegítik a szomszédai, ha jóval melegebbek.
			/// </summary>
			/// <param name="itt"></param>
			void Melegedés(Vektor itt)
			{
                foreach (Vektor szomszéd in Szomszédai(itt))
					Melegít(szomszéd, itt);
			}
			/// <summary>
			/// A hőtábla elkészítése a pályán található láva elhelyezkedése alapján.
			/// </summary>
			void Hőtérképezés()
			{
				// A láva 1000 fokos... ("Inicializálás")
				for (int i = 0; i < X; i++)
					for (int j = 0; j < Y; j++)
						if (tábla[i, j] == 7)
							hőtábla[i, j] = 1000;

				//... és minden szomszédos mezőn 200 fokkal hűvösebb. Tehát 4-szer (1000->800->600->400->200) végigmegyünk, hogy a felmelegedést update-eljük.
				for (int k = 0; k < 4; k++)
					for (int i = 0; i < X; i++)
						for (int j = 0; j < Y; j++)
							Melegedés(new Vektor(i, j));
			}

            #endregion

            #region Egyéb metódusok

            #endregion
        }
    }
}