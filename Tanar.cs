using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Karesz
{
    public partial class Form1 : Form
    {

		static Random r = new Random();
		string betöltendő_pálya = "../../Palyak/Palya01.txt";

		void TANÁR_ROBOTJAI()
		{
			new Robot("Karesz", 0, 0, 0, 0, 100, 16, 16, 2);//100 hógolyóval indít
			Robot m = new Robot("Golyesz", Robot.képkészlet_lilesz, 0, 0, 0, 0, 1000, 1, 1, 1);//1000hógolyóval indít

			m.Feladat = delegate
			{
				while (true)
					Körjárat(m);
			};
		}

		void Bénázik_ennyit(Robot m, int n)
		{
			for (int i = 0; i < n; i++)
			{
				Vár(m, r.Next(2));
				m.Lépj();
			}
		}

		void Bénázik_a_falig(Robot m)
		{
			while (!m.Előtt_fal_van())
			{
				Vár(m, r.Next(4));
				m.Lépj();
			}
		}


		void Vár(Robot m, int n)
		{
			for (int i = 0; i < n; i++)
				m.Várj();
		}

		void Körjárat(Robot m)
		{
			Bénázik_a_falig(m);
			m.Fordulj(jobbra);
			int ennyit_megy_be= r.Next(3);
			Bénázik_ennyit(m, ennyit_megy_be);

			m.Fordulj(jobbra);
			m.Fordulj(jobbra);
			Bénázik_ennyit(m, ennyit_megy_be);
			m.Fordulj(balra);
			Bénázik_ennyit(m, 17);
			m.Fordulj(jobbra);
			m.Fordulj(jobbra);

		}

	}
}