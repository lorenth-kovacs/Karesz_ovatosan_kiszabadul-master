using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karesz
{
	/// <summary>
	/// Egészek fölötti kétdimenziós vektorok (Z^2)
	/// </summary>
	struct Vektor
	{
        #region tulajdonságok

        public int X, Y;

        #endregion

        #region konstruktorok

        public Vektor(int x, int y) =>
			(X, Y) = (x, y);
		public Vektor(Vektor V) :
			this(V.X, V.Y)
		{ }

		/// <summary>
		/// 0:É, 1:NY, 2:D, 3:K, ...
		/// </summary>
		/// <param name="f"></param>
		public Vektor(int f)
		{
			if (3 < f) f %= 4;
			if (f < 0) f *= -1;

			switch (f)
			{
				case 0:
					(X, Y) = (0, -1);
					break;
				case 1:
					(X, Y) = (1, 0);
					break;
				case 2:
					(X, Y) = (0, 1);
					break;
				case 3:
					(X, Y) = (-1, 0);
					break;
				default:
					(X, Y) = (0, 0);
					break;
			}
		}

		#endregion

		#region operátorok (+,-,*,/,==,!=)

		public static Vektor operator +(Vektor u, Vektor v) =>
			new Vektor(u.X + v.X, u.Y + v.Y);
		public static Vektor operator -(Vektor u, Vektor v) =>
			new Vektor(u.X - v.X, u.Y - v.Y);
		public static int operator *(Vektor u, Vektor v) =>
			u.X * v.X + u.Y * v.Y;
		public static Vektor operator *(Vektor u, int a) =>
			new Vektor(u.X * a, u.Y * a);
		public static Vektor operator *(int a, Vektor u) =>
			u * a;
		public static Vektor operator /(Vektor u, int a) =>
			new Vektor(u.X / a, u.Y / a);

		public static bool operator ==(Vektor u, Vektor v) =>
			u.X == v.X && u.Y == v.Y;
		public static bool operator !=(Vektor u, Vektor v) =>
			!(u == v);

		#endregion

		#region egyéb műveletek

		public int HosszN() =>
			X * X + Y * Y;
		public void Forgat(int i) =>
			(X, Y) = (-i * Y, i * X); // fordított a koordinátarendszer!
		/// <summary>
		/// A négy égtáj fele mutató irányvektort lekódoljuk egy int-be. 
		/// 0: észak
		/// 1: kelet
		/// 2: dél
		/// 3: nyugat
		/// </summary>
		/// <returns></returns>
		public int ToInt() =>
			Y == -1 ? 0 : (X == 1 ? 1 : (Y == 1 ? 2 : 3));
		public int TavN(Vektor Q) =>
			(this - Q).HosszN();
		public Vektor Balra() =>
			new Vektor(X - 1, Y);
		public Vektor Jobbra() =>
			new Vektor(X + 1, Y);
		public Vektor Fent() =>
			new Vektor(X, Y + 1);
		public Vektor Lent() =>
			new Vektor(X, Y - 1);

		public Vektor Forgatott(int i)
		{
			Vektor v = new Vektor(this);
			v.Forgat(i);
			return v;
		}

		public override string ToString() => $"{X}, {Y}";

		static int intervallumba(int b, int x, int j) => Math.Min(Math.Max(b, x), j);
		public static Vektor dobozba(Vektor ba, Vektor v, Vektor jf) => new Vektor(intervallumba(ba.X, v.X, jf.X), intervallumba(ba.Y, v.Y, jf.Y));

		#endregion
	}
}
