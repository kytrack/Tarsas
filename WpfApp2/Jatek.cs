using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfApp2
{
    public class Jatek
    {
        List<int> dobasok = new List<int>();
        Osveny osveny;
        int jatekosokSzama;
        int[] holJarnak;
        List<int> beertJatekosok = new List<int>();

        public Jatek(Osveny osveny, int jatekosokSzama)
        {
            this.osveny = osveny;
            this.jatekosokSzama = jatekosokSzama;
            this.holJarnak = new int[jatekosokSzama + 1];
        }

        public Jatek( Osveny osveny, int jatekosokSzama, List<int> dobasok) :this(osveny,jatekosokSzama)
        {
            this.dobasok = dobasok;
            foreach (int dobas in dobasok)
            {
                Dob(dobas);
            }
        }

        public void Mentes(string eleresiUt)
        {
            string dobasokString = "";
            foreach (int dobas in this.Dobasok)
            {
                if (dobasokString == "")
                {
                    dobasokString += dobas;
                }
                else
                {
                    dobasokString += ","+dobas;
                }
            }

            File.AppendAllText(eleresiUt,$"{dobasokString};{this.osveny.MezoStringben};{this.osveny.Sorszama};{this.jatekosokSzama};{this.holJarnak};{this.beertJatekosok}");
        }

        public void Dob(int dobas)
        {
            dobasok.Add(dobas);
            int mostaniDobo = dobasok.Count % jatekosokSzama;
            int hovaJutna = holJarnak[UtoljaraDobo] + dobas;
            holJarnak[UtoljaraDobo] = hovaJutna;
            if (hovaJutna > osveny.Hossz)
            {
                beertJatekosok.Add(UtoljaraDobo);
            }
            else
                switch (osveny.Mezok[hovaJutna])
                {
                    case 'V':
                        holJarnak[UtoljaraDobo] -= dobas; //vissza
                        break;
                    case 'E':
                        holJarnak[UtoljaraDobo] += dobas; //dupla haladás
                        if (holJarnak[UtoljaraDobo] > osveny.Hossz)
                            beertJatekosok.Add(UtoljaraDobo);
                        break;
                }
        }

        public List<int> Dobasok { get => dobasok; }
        public int EddigiDobasokSzama => dobasok.Count;
        public int MostaniKorSzama => dobasok.Count / jatekosokSzama + 1;
        public int UtoljaraDobo
        {
            get
            {
                int maradek = dobasok.Count % jatekosokSzama;
                return maradek == 0 ? jatekosokSzama : maradek;
            }
        }

        public int KiVanLegelol
        {
            get
            {
                int elolLevoPozicioja = holJarnak.Max();
                int kiaz = 0;
                while (holJarnak[kiaz] != elolLevoPozicioja)
                {
                    kiaz++;
                }
                return kiaz;
            }
        }
        public bool KorVege => UtoljaraDobo == jatekosokSzama;
        public bool JatekVege => UtoljaraDobo == jatekosokSzama && BeertJatekosok.Count > 0;

        public List<int> BeertJatekosok { get => beertJatekosok; }
        public int[] HolJarnak { get => holJarnak; }
        public int JatekosokSzama { get => jatekosokSzama; }
    }
}
