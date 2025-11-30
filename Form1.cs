using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace Karesz
{
	public partial class Form1 : Form
	{

		// IDE JÖNNEK AZ ELJÁRÁSOK ÉS FÜGGVÉNYEK


		void DIÁK_ROBOTJAI()
		{
			Robot.Get("Karesz").Feladat = delegate ()
			{
				// Gonesz le akarja rombolni a játékgépet.
				// A játékgép széléből le is tört egy darabkát.
			// Segíts KARESZNAK betömni ezt a lyukat egy piros kaviccsal!
				// Vigyázz, mert Gonesz ezt nem szeretné, és hógolyókkal bombázza KARESZT.
				// Gonesz kissé ügyetlen, ezért folyton botladozik a pályán.
				// Te is lőhetsz hógolyókat, de spórolj velük, mert csak 10 van.
				// Gonesznak három élete van.
				// tipp: Ha összeütközik 2 hógolyó, mindkettő elpusztul.
				// (Karesz távolsága a rongálástól: 14)
				// tehát nem kell feltétlenül gyilkolni, a cél az, hogy ** KARESZ betömje a lyukat egy piros kaviccsal ** (meg esetleg beszéld rá Goneszt, hogy többé ne tegyen ilyet)

				Lépj();
				Lőjj();

				int a = Milyen_messze_van_hógolyó();
				bool b = Erre_jön_e_a_hógolyó();


			};
		}

	}
}





/* USERS MANUAL -- LEGFONTOSABB PARANCSOK

PÁLYASPECIFIKUS

Lőjj();							-------- Karesz lő előre egy hógolyót

Milyen_messze_van_hógolyó()		-------- Visszaadja a Karesszal szemben levő legközelebbi hógolyó távolságát, vagy -1-et
Erre_jön_e_a_hógolyó()			-------- igaz, ha Karesz felé közeledik a Karesszal szembe levő legközelebbi hógolyó

HASZNOS

Várj();							-------- Karesz kihagyja magát egy körből, amíg a többiek lépnek

MOZGÁSOK

Lépj();                          -------- Karesz előre lép egyet.
Fordulj(jobbra);                 -------- Karesz jobbra fordul.
Fordulj(balra);                  -------- Karesz balra fordul.
Vegyél_fel_egy_kavicsot();       -------- Karesz felvesz egy kavicsot.
Tegyél_le_egy_kavicsot();        -------- Karesz letesz egy fekete kavicsot
Tegyél_le_egy_kavicsot(piros);   -------- Karesz letesz egy piros kavicsot.

Minden mozgás után a robot köre véget ér és a következő robot jön. 

SZENZOROK

Van_e_előttem_fal();        -------- igaz, ha Karesz fal előtt áll, egyébként hamis.
Kilépek_e_a_pályáról();     -------- igaz, ha Karesz a pálya szélén kifele néz, egyébként hamis.
Van_e_itt_Kavics();         -------- igaz, ha Karesz épp kavicson áll, egyébként hamis.
Köveinek_száma_ebből(piros) -------- Karesz piros köveinek a száma.
Mi_van_alattam();           -------- a kavics színe, amin Karesz áll. (Ez igazából egy szám!)
Ultrahang();                -------- a Karesz előtt található tárgy távolsága. Ez a tárgy lehet fal vagy másik robot is. 
SzélesUltrahang();          -------- ugyanaz, mint az ultrahangszenzor, de ez nem csak a Karesz előtti mezőket pásztázza, hanem a szomszédos mezőket is. Egy számhármast ad vissza. 
Hőmérséklet();              -------- a Karesz által mért hőmérséklet. A láva forrása 1000 fok, amitől lépésenként távolodva a hőmérséklet 200 fokonként hűl. Az alapértelmezett hőmérséklet 0 fok.

A szenzorokat bármennyiszer használhatja a robot a saját körén belül.
*/