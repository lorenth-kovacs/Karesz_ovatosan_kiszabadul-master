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
using System.Runtime.InteropServices;

namespace Karesz
{
	partial class Form1
	{
        class Robot
		{
			#region statikus tulajdonságok
			static readonly int várakozási_idő = 100;
			public static Form1 form;
			static bool vége = false;
			public static Pálya pálya { get => Robot.form.pálya; }
			public static List<Robot> lista = new List<Robot>();
			public static HashSet<Robot> halállista = new HashSet<Robot>();
			public static int ok_száma { get => Robot.lista.Count; }
			public static ModuloSzam megfigyeltindex;
			public static Robot akit_kiválasztottak { get => lista[megfigyeltindex.ToInt()]; }
			#endregion
			#region statikus metódusok
			public static Robot Get(string n) => Robot.lista.First(x => x.Név == n);
			public static readonly Bitmap[] képkészlet_karesz = new Bitmap[4]
			{
				Properties.Resources.Karesz0,
				Properties.Resources.Karesz1,
				Properties.Resources.Karesz2,
				Properties.Resources.Karesz3
			};
			public static readonly Bitmap[] képkészlet_lilesz = new Bitmap[4]
			{
				Properties.Resources.Lilesz0,
				Properties.Resources.Lilesz1,
				Properties.Resources.Lilesz2,
				Properties.Resources.Lilesz3
			}; 
			static readonly Bitmap[] képkészlet_golyesz = new Bitmap[4]
			{
				Properties.Resources.golyesz_up,
				Properties.Resources.golyesz_right,
				Properties.Resources.golyesz_down,
				Properties.Resources.golyesz_left
			};
			
			int Indexe() => Robot.lista.FindIndex(r => r == this);
			public static bool ok_közül_valaki_még_dolgozik() => -1 < Robot.lista.FindIndex(r => !r.Kész);
			#endregion
			#region Instanciák tulajdonságai
			public string Név { get; private set; }
			public int Életei { get => életei; }
			int életei;
			Bitmap[] képkészlet;
			public Vektor h;
			public Vektor H { get => h; }
			public Vektor getHelyigény { get => helyigény; }
			Vektor helyigény;
			Vektor v;
			int[] kődb;
			Action feladat;
			public Action Feladat
			{
				get => feladat;
				set
				{
					feladat = value;
					thread = new Thread(new ThreadStart(feladat));
				}
			}
			Thread thread;

			public bool Kész { get => thread.ThreadState == ThreadState.Stopped; }
			public bool Vár { get => thread.ThreadState == ThreadState.Suspended; }
			public bool Elindult { get => thread.ThreadState != ThreadState.Unstarted; }

			#endregion
			public override string ToString() => $"{this.Név} ({this.H})";
			#region Konstruktorok

			/// <summary>
			/// Teljes konstruktor: Létrehoz egy új robotot a megadott névvel, képkészlettel, pozícióval, iránnyal és induló kövek számával.
			/// </summary>
			/// <param name="név">A robot neve</param>
			/// <param name="képkészlet">A képkészlet a Resources mappából</param>
			/// <param name="H">indulási pozíció</param>
			/// <param name="I">kezdőirány</param>
			/// <param name="kődb">induláskor a zsebeiben lévő kövek száma</param>
			/// <param name="szülőform">az eredeti form, a visszahivatkozáshoz kell</param>
			/// <param name="pálya">a pálya, amin a robot mozog</param>
			public Robot(string név, Bitmap[] képkészlet, int[] kődb, Vektor h, Vektor v, int életei )
			{
				this.Név = név;
				this.h = h;
				this.v = v;
				this.képkészlet = képkészlet;
				this.kődb = kődb;
				this.helyigény = h;
				this.életei = életei;

				if (0 == Robot.lista.Count)
					Robot.megfigyeltindex = new ModuloSzam(0,1);
				else
					Robot.megfigyeltindex.ModulusNövelése();

				Robot.lista.Add(this);
			}
			public Robot(string név, Bitmap[] képkészlet, int[] kődb, Vektor h, Vektor v)
				: this(név,
					  képkészlet,
					  kődb,
					  h,
					  v,
					  1)
			{ }

            public Robot(string adottnév, int[] indulókövek, Vektor hely, Vektor sebesség)
				: this(adottnév, képkészlet_karesz,
						indulókövek,
						hely,
						sebesség
						)
			{ }
			public Robot(string adottnév, int[] indulókövek, int x, int y, int f) :
				this(adottnév, indulókövek, new Vektor(x, y), new Vektor(f))
			{ }
			public Robot(string adottnév, Bitmap[] képkészlet, int fekete_db, int piros_db, int zöld_db, int sárga_db, int hó_db, int x, int y, int f, int életei) :
							this(adottnév, képkészlet, new int[] { fekete_db, piros_db, zöld_db, sárga_db, hó_db }, new Vektor(x, y), new Vektor(f), életei)
			{ }
			public Robot(string adottnév, Bitmap[] képkészlet, int fekete_db, int piros_db, int zöld_db, int sárga_db, int hó_db, int x, int y, int f) :
							this(adottnév, képkészlet, new int[] { fekete_db, piros_db, zöld_db, sárga_db, hó_db }, new Vektor(x, y), new Vektor(f))
			{ }
			public Robot(string adottnév, int fekete_db, int piros_db, int zöld_db, int sárga_db, int hó_db, int x, int y, int f) :
							this(adottnév, new int[] { fekete_db, piros_db, zöld_db, sárga_db, hó_db }, new Vektor(x, y), new Vektor(f))
			{ }
			public Robot(string adottnév, int x, int y, int f) :
				this(adottnév, 0, 0, 0, 0, 0, x, y, f)
			{ }
			public Robot(string adottnév, int x, int y) :
				this(adottnév, x, y, 0)
			{ }
			public Robot(string adottnév) :
				this(adottnév, 5, 28)
			{ }
            #endregion
            #region Játékkezelés
            static void ok_elindítása()
			{
				foreach (Robot robot in Robot.lista)
					if (!robot.Kész)
						robot.Start_or_Resume();
			}
			public static void Játék()
			{
				Robot.ok_elindítása();
				Thread.Sleep(várakozási_idő);
				while (Robot.lista.Exists(r => !r.Kész) && !Robot.vége)
				{
					if (Robot.lista.TrueForAll(r => r.Kész || r.Vár))
					{
						Hógolyó.k_léptetése();
						Robot.ok_léptetése();
						Robot.form.Frissít();
						Robot.ok_elindítása();
					}
					Thread.Sleep(várakozási_idő);
				}
				Robot.form.Frissít();
				SendKeys.Send("%"); // valamilyen misztikus okból kifolyólag nem frissít rendesen az ablak a végén, csak ha valaki az ALT gombot lenyomja...
			}
			static void ok_léptetése()
			{
				Robot.Új_lövedékek_létrehozása();
				Robot.holtak_összegyűjtése();
				Robot.holtak_eltávolítása();
				foreach (Robot robot in Robot.lista)
					robot.h = robot.helyigény;
			}

            private static void Új_lövedékek_létrehozása()
            {
                foreach ((Vektor, Vektor) p in Robot.Ellövendő_lövedékek)
                {
					(Vektor h, Vektor v) = p;
					new Hógolyó(h, v);
					/*
					Robot golyesz = new Robot("Golyesz", képkészlet_golyesz, new int[] { 0, 0, 0, 0, 0 }, h, v);
					golyesz.Feladat = delegate ()
					{
                        while (true)
							golyesz.Lépj();
					};
					*/
                }
				Robot.Ellövendő_lövedékek.Clear();
            }

            static void holtak_eltávolítása()
			{
				foreach (Robot robot in Robot.halállista)
				{
					robot.Sírkő_letétele();
					if (robot.Indexe() < megfigyeltindex.ToInt())
						--megfigyeltindex;
					Robot.lista.Remove(robot);
					megfigyeltindex.ModulusCsökkentése();

                    if (robot.Elindult && !robot.Kész)
                    {
						robot.thread.Suspend();
                    }
				}
				Robot.halállista.Clear();
			}

			static void holtak_összegyűjtése()
			{
                Robot.Halállistához(r => pálya.MiVanItt(r.helyigény) == fal);// falba lép
                Robot.Halállistához(r => !pálya.BenneVan(r.helyigény)); // kiesik a pályáról
                Robot.Halállistához(r => r.Van_e_itt_hógolyó()); // Kiüti egy hógolyó
                Robot.Halállistához((r1, r2) => r1.helyigény == r2.helyigény); // egy helyre léptek
                Robot.Halállistához((r1, r2) => r1.helyigény == r2.H && r2.helyigény == r1.H);// átmentek egymáson / megpróbáltak helyet cserélni
            }
            void Sírkő_letétele()
			{
				pálya.LegyenItt(H, fekete);
			}
			bool Van_e_itt_hógolyó()
			{
                for (int i = 0; i < Hógolyó.lista.Count; i++)
                {
                    if (this.helyigény == Hógolyó.lista[i].helyigény)
                    {
                        Hógolyó.lista.RemoveAt(i);
						this.életei--;
						Mondd(életei + " életem maradt.");
						return this.életei == 0;
                    }
                }
				return false;
            }
			static void Halállistához(Func<Robot, bool> predikátum)
			{
				foreach (Robot robot in Robot.lista)
				{
					if(predikátum(robot))
						Robot.halállista.Add(robot);
				}
			}
			static void Halállistához(Func<Robot, Robot, bool> predikátum)
			{
				for (int i = 0; i < Robot.lista.Count; i++)
					for (int j = i+1; j < Robot.lista.Count; j++)
						if (predikátum(Robot.lista[i], Robot.lista[j]))
						{
							Robot.halállista.Add(Robot.lista[i]);
							Robot.halállista.Add(Robot.lista[j]);
						}
			}
			void Start_or_Resume()
			{
				if (this.thread.ThreadState == ThreadState.Unstarted)
					this.thread.Start();
				else if (this.Vár)
					this.thread.Resume();
			}
			#endregion
			#region Motorok
			/// <summary>
			/// Elhelyezi a Robotot a megadott helyre.
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			public void Teleport(int x, int y)
			{
				(h.X, h.Y) = (x, y);
				(helyigény.X, helyigény.Y) = (x, y);
			}
			/// <summary>
			/// Lépteti a robotot a megfelelő irányba.
			/// </summary>
			public void Lépj()
			{
				helyigény = h+v;
				Cselekvés_vége();
			}
			/// <summary>
			/// Elforgatja a robotot a megadott irányban. (Csak normális irányokra reagál.)
			/// </summary>
			/// <param name="forgásirány"></param>
			public void Fordulj(int forgásirány)
			{
				v.Forgat(forgásirány);
				Cselekvés_vége();
			}
			/// <summary>
			/// Lerakja az adott színű követ a pályán a robot helyére.
			/// </summary>
			/// <param name="szín"></param>
			public void Tegyél_le_egy_kavicsot(int szín = fekete)
			{
				if (pálya.MiVanItt(H) != üres)
					Mondd("Nem tudom a kavicsot lerakni, mert van lerakva kavics!");
				else if (kődb[szín - 2] <= 0)
					Mondd($"Nem tudom a kavicsot lerakni, mert nincs {színnév[szín]} színű kavicsom!");
				else
				{
					pálya.LegyenItt(H, szín);
					--kődb[szín - 2];
					idő++;
				}
				if (szín == piros && pálya.MiVanItt(H) == piros)
					vége = true;
				Cselekvés_vége();
			}
			/// <summary>
			/// Felveszi azt, amin éppen áll -- feltéve ha az nem fal, stb.
			/// </summary>
			public void Vegyél_fel_egy_kavicsot()
			{
				if (pálya.MiVanItt(H) > fal)
				{
					++kődb[pálya.MiVanItt(H) - 2];
					pálya.LegyenItt(H, üres);
					idő++;
				}
				else
					Mondd(": Nem tudom a kavicsot felvenni!");

				Cselekvés_vége();
			}
			public void Lőjj() //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
			{
				if (0 < kődb[hó - 2])
				{
					--kődb[hó - 2];
					Robot.Ellövendő_lövedékek.Add((this.H + this.v, this.v));
				}
				else
					Mondd(": Nincsen nálam hó!");

				Cselekvés_vége();
			}

			static HashSet<(Vektor, Vektor)> Ellövendő_lövedékek = new HashSet<(Vektor, Vektor)>();

            public void Várj() => Cselekvés_vége();
			public void Mondd(string ezt) => MessageBox.Show(Név + ": " + ezt);

			#endregion
			#region Szenzorok

			(Func<Vektor, bool>, Func<Vektor, Vektor, int>) Hógolyótaláló_predikátum()
			{
				Func<Vektor, bool> predikatum;
				Func<Vektor, Vektor, int> különbség;
                if (this.v.X == 0)
                {
                    if (this.v.Y == -1) // fel
                    {
                        predikatum = hely1 => hely1.X == this.h.X && hely1.Y < this.h.Y;
						különbség = (kareszhely, hógolyóhely) => kareszhely.Y - hógolyóhely.Y;
                    }
                    else // le
                    {
                        predikatum = hely1 => hely1.X == this.h.X && hely1.Y > this.h.Y;
						különbség = (kareszhely, hógolyóhely) => hógolyóhely.Y - kareszhely.Y;
                    }
                }
                else
                {
                    if (this.v.X == -1)
                    { // balra
                        predikatum = hely1 => hely1.X < this.h.X && hely1.Y == this.h.Y;
                           különbség = (kareszhely, hógolyóhely) => kareszhely.X - hógolyóhely.X;
                    }
                    else // jobbra
                    {
                        predikatum = hely1 => hely1.X > this.h.X && hely1.Y == this.h.Y;
                        különbség = (kareszhely, hógolyóhely) => hógolyóhely.X - kareszhely.X;
                    }
                }
				return (predikatum, különbség);
            }
            public int Milyen_messze_van_hógolyó()
			{
				int result = -1;
				(Func<Vektor, bool> predikatum, Func<Vektor, Vektor, int> különbség)  = Hógolyótaláló_predikátum();
				foreach(Hógolyó hógolyó in Hógolyó.lista)
				{
					int temp;
					if (predikatum(hógolyó.h) && (result == -1 | (temp = különbség(this.h, hógolyó.h)) < temp)) // direkt szimpla |
					{
						result = temp;
					}
				}
				return result;
			}
			public bool Erre_jön_e_a_hógolyó()
			{
				(Func<Vektor, bool> predikatum, Func<Vektor, Vektor, int> különbség) = Hógolyótaláló_predikátum();
				bool result = false;
				int különbség_int = -1;
				foreach(Hógolyó hógolyó in Hógolyó.lista)
				{
					int temp;
					if (predikatum(hógolyó.h) && (különbség_int == -1 | (temp = különbség(this.h, hógolyó.h)) < temp)) {
						különbség_int = temp;
						result = (hógolyó.v.X == this.v.X) != (hógolyó.v.Y == this.v.Y);
					}
				}
				return result;
			}

            /// <summary>
            /// Megadja, hogy az adott színből mennyi köve van a robotnak.
            /// </summary>
            /// <param name="szín"></param>
            /// <returns></returns>
            public int Köveinek_száma_ebből(int szín) => kődb[szín - 2];

			/// <summary>
			/// Megadja, hogy kavicson áll-e a robot.
			/// </summary>
			/// <returns></returns>
			public bool Alatt_van_kavics() =>
				pálya.MiVanItt(H) > fal;

			/// <summary>
			/// Megadja, hogy min áll a robot
			/// </summary>
			/// <returns></returns>
			public int Alatt_ez_van() =>
				pálya.MiVanItt(H);

			/// <summary>
			/// Megadja, hogy mi van a robot előtt az adott helyen -- (1 = fal, -1 = kilép)
			/// </summary>
			/// <returns></returns>
			int MiVanElőttem(Vektor Itt) =>
				pálya.BenneVan(Itt) ? pálya.MiVanItt(Itt) : -1;

			/// <summary>
			/// megadja, hogy mi van a robot előtt
			/// </summary>
			/// <returns></returns>
			public int MiVanElőttem() =>
				MiVanElőttem(H + v);

			/// <summary>
			/// Pontosan akkor igaz, ha a robot előtt fal van.
			/// </summary>
			/// <returns></returns>
			public bool Előtt_fal_van() => this.MiVanElőttem() == fal;
			/// <summary>
			/// Pontosan akkor igaz, ha a robot a pálya szélén van és a következő lépéssel kizuhanna a pályáról.
			/// </summary>
			/// <returns></returns>
			public bool Ki_fog_lépni_a_pályáról() => this.MiVanElőttem() == nincs_pálya;

			/// <summary>
			/// megadja, hogy milyen messze van a robot előtti legközelebbi olyan objektum, amely vissza tudja verni a hangot (per pill. másik robot vagy fal)
			/// </summary>
			/// <returns></returns>
			public int UltrahangSzenzor() => Akadálytávolság(H, v);
			public (int, int, int) SzélesUltrahangSzenzor()
				=> (Akadálytávolság(H + v.Forgatott(balra), v), Akadálytávolság(H, v), Akadálytávolság(H + v.Forgatott(jobbra), v));


			int Akadálytávolság(Vektor hely, Vektor sebesség)
			{
				int d = 1;
				Vektor J = new Vektor(hely + sebesség);
				while (pálya.BenneVan(J) && !(pálya.MiVanItt(J) == 1 || Más_robot_van_itt(J)))
				{
					J += sebesség;
					d++;
				}
				return pálya.BenneVan(J) ? d : -1;
			}
			bool Más_robot_van_itt(Vektor v) => -1 < Robot.lista.FindIndex(r => r.H == v);
			HashSet<Vektor> Más_robotok_helyei() => Robot.lista.Select(x => x.H).ToHashSet();
			public int Hőmérő() =>
				pálya.Hőmérséklet(H);
			#endregion
			#region Formkezeléshez és szálkezeléshez szolgáló metódusok

			/// <summary>
			/// Visszaadja a sebességvektor számkódját, ami a képek kezeléséhez kell.
			/// </summary>
			/// <returns></returns>
			public Bitmap Iránykép() => képkészlet[v.ToInt()];
			void Cselekvés_vége()
			{
				if (!Kész && Elindult)
					this.thread.Suspend();
			}

			#endregion
		}
	}
}