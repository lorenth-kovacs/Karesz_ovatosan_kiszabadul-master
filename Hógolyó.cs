using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Karesz
{
    partial class Form1
    {
        class Hógolyó
        {
            public Vektor h;
            public Vektor v;
            public Vektor helyigény;

            public Hógolyó(Vektor h, Vektor v)
            {
                this.v = v;
                this.h = h;
                lista.Add(this);
            }

            //static bool Egymassal_szembe(Hógolyó egyik, Hógolyó másik) => ((egyik.v.X == másik.v.X) != (egyik.v.Y == másik.v.Y)) && (egyik.helyigény == másik.h);

            public static void k_léptetése()
            {
                foreach (Hógolyó hógolyó in lista)
                {
                    hógolyó.helyigény = hógolyó.h + hógolyó.v;
                }
                // Ütköző hógolyók:
                for (int i = lista.Count - 1; i >= 0; i--)
                {
                    bool kész = false;
                    int mivanitt = Robot.pálya.MiVanItt(lista[i].helyigény);
                    // ha falba megy:
                    if (mivanitt == 1)
                    {
                        lista.RemoveAt(i);
                        continue;
                    }
                    // Összeütköző hógolyók:
                    for (int j = i + 1; j < lista.Count; j++)
                    {
                        if ((lista[i].helyigény == lista[j].helyigény) || (lista[i].helyigény == lista[j].h && lista[i].h == lista[j].helyigény))
                        {
                            lista.RemoveAt(j);
                            lista.RemoveAt(i);
                            break;
                        }
                    }
                }
                foreach (Hógolyó hógolyó in lista)
                {
                    hógolyó.h = hógolyó.helyigény;
                }
            }

            public static List<Hógolyó> lista = new List<Hógolyó>();
        }
    }
}