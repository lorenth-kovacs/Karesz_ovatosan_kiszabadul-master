using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Karesz
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

        public static void k_léptetése()
        {
            foreach (Hógolyó hógolyó in Hógolyó.lista)
            {
                hógolyó.helyigény = hógolyó.h + hógolyó.v;
            }
        }

        public static List<Hógolyó> lista = new List<Hógolyó> ();
    }
}
