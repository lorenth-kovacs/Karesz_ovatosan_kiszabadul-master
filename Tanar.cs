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
			new Robot("Karesz", 0, 0, 0, 0, 100, r.Next(14) + 11, 16 , 0);//100 hógolyóval indít
			Robot gonesz = new Robot("Golyesz", Robot.képkészlet_lilesz, 0, 0, 0, 0, 1000, r.Next(14), 1 , 2);//1000hógolyóval indít

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
                gonesz.Lőjj();
                if (irány)//balra KARESZ szempontjából
                {
                    gonesz.Fordulj(jobbra);
                    
                    gonesz.Fordulj(balra);
                }
                else
                {
                    gonesz.Fordulj(balra);
                    if (!Van_e_előttem_fal())
                        gonesz.Lépj();
                    else
                        irány = !irány;
                    gonesz.Fordulj(jobbra);
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