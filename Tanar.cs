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
		string betöltendő_pálya = "Palya01.txt";

		void TANÁR_ROBOTJAI()
		{
			new Robot("Karesz", 0, 0, 0, 0, 100, r.Next(15) + 13, 22 , 0);//100 hógolyóval indít
			Robot gonesz = new Robot("Golyesz", Robot.képkészlet_lilesz, 0, 0, 0, 0, 1000, r.Next(15) + 13, 3 , 2);//1000hógolyóval indít

			gonesz.Feladat = delegate
			{
				Gonesz_lép(gonesz);
			};
		}

		void Gonesz_lép(Robot gonesz)
		{
            bool irány = (r.Next(1) == 0) ? true : false;
            while (true)
            {
                if (irány)//balra KARESZ szempontjából
                {
                    //akadálytávolság; forgatott
                    //tesó miről yappel???
                    if (!(gonesz.SzélesUltrahangSzenzor().Item3 <= 1))
                    {
                        gonesz.Lőjj();
                        gonesz.Fordulj(jobbra);
                        gonesz.Lépj();
                        gonesz.Fordulj(balra);
                    }
                    else
                        irány = !irány;
                }
                else
                {
                    if (!(gonesz.SzélesUltrahangSzenzor().Item1 <= 1))
                    {
                        gonesz.Lőjj();
                        gonesz.Fordulj(balra);
                        gonesz.Lépj();
                        gonesz.Fordulj(jobbra);
                    }
                    else
                        irány = !irány;
                }
            }
		}

		void Vár(Robot gonesz, int n)
		{
			for (int i = 0; i < n; i++)
                gonesz.Várj();
		}

	}
}