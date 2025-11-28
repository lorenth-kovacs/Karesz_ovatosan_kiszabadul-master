using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karesz
{
    struct ModuloSzam
    {
        int n;
        int m;

        #region Konstruktor(ok)
        public ModuloSzam(int n, int m)
        { 
            this.n = n; 
            this.m = m; 
        }
        #endregion

        #region logikai operátorok

        public static bool operator ==(ModuloSzam a, ModuloSzam b) => a.n == b.n && a.m == b.m;
        public static bool operator !=(ModuloSzam a, ModuloSzam b) => !(a == b);
        public static bool operator ==(ModuloSzam a, int i) => a.n == i;
        public static bool operator !=(ModuloSzam a, int i) => !(a == i);
        public static bool operator ==(int i, ModuloSzam a) => a.n == i;
        public static bool operator !=(int i, ModuloSzam a) => !(a == i);
        #endregion

        #region műveletek
        public static ModuloSzam operator +(ModuloSzam a, int i) => new ModuloSzam((a.n + i) % a.m, a.m);
        public static ModuloSzam operator +(int i, ModuloSzam a) => new ModuloSzam((a.n + i) % a.m, a.m);
        public static ModuloSzam operator +(ModuloSzam a, ModuloSzam b)
        {
            if (a.m != b.m) 
                throw new Exception("Nem lehet különböző modulusú moduloaritmetikai számokat összeadni!");
            return a + b.n;
        }
        public static ModuloSzam operator ++(ModuloSzam a)
        {
            if (++a.n == a.m)
                a.n = 0;
            return a;
        }

        public static ModuloSzam operator -(ModuloSzam a) => new ModuloSzam(a.m - a.n, a.m);
        public static ModuloSzam operator -(ModuloSzam a, ModuloSzam b)
        {
            if (a.m != b.m) 
                throw new Exception("Nem lehet különböző modulusú moduloaritmetikai számokat kivonni egymásból!");
            return a + (-b);
        }
        public static ModuloSzam operator --(ModuloSzam a)
        {
            if (--a.n == -1)
                a.n = a.m - 1;
            return a;
        }
        public static ModuloSzam operator *(ModuloSzam a, int i) => new ModuloSzam((a.n * i) % a.m, a.m);
        public static ModuloSzam operator *(int i, ModuloSzam a) => new ModuloSzam((a.n + i) % a.m, a.m);

        public static ModuloSzam operator *(ModuloSzam a, ModuloSzam b)
        {
            if (a.m != b.m) 
                throw new Exception("Nem lehet különböző modulusú moduloaritmetikai számokat szorozni!");
            return a * b.n;
        }
        public ModuloSzam Reciprok()
        {
            for (int i = 1; i < m; i++) 
                if (this * i == 1) 
                    return new ModuloSzam(i, m);
            throw new Exception($"A(z) {m} modulus nem prím, így nem minden számnak van reciproka, pl. a(z) {n} számnak sincs.");
        }
        public static ModuloSzam operator /(ModuloSzam a, ModuloSzam b) => a * b.Reciprok();
        #endregion

        #region ToXY függvények
        public int ToInt() => n;
        public override string ToString() => $"{n} ({m})";

        #endregion

        #region modulussal kapcsolatos függvények

        public int GetModulus() => m;
        public void ModulusNövelése(int i = 1) => m += i;
        public void ModulusCsökkentése(int i = 1) 
        {
            m -= i;
            if (n == m)
                n = 0;
        }
        #endregion

    }
}
