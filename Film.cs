using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inchirieri_de_casete_video
{
    class Film : ICloneable, IComparable
    {
        private readonly int id;
        private string denumire;
        private float rating;
        private string regizor;
        private int an_aparitie;
        private List<string> listaActori;
        private string clasificare;
        private List<string> genuri;

        public Film()
        {
        }

        public Film(int id, string denumire, float rating, string regizor, int an_aparitie, List<string> listaActori, string clasificare, List<string> genuri)
        {
            this.id = id;
            this.denumire = denumire;
            this.rating = rating;
            this.regizor = regizor;
            this.an_aparitie = an_aparitie;
            this.listaActori = listaActori;
            this.clasificare = clasificare;
            this.genuri = genuri;
        }

        public int Id => id;

        public string Denumire { get => denumire; set => denumire = value; }
        public float Rating { get => rating; set => rating = value; }
        public string Regizor { get => regizor; set => regizor = value; }
        public int An_aparitie { get => an_aparitie; set => an_aparitie = value; }
        public List<string> ListaActori { get => listaActori; set => listaActori = value; }
        public string Clasificare { get => clasificare; set => clasificare = value; }
        public List<string> Genuri { get => genuri; set => genuri = value; }

        public object Clone()
        {
            Film clona = (Film)this.MemberwiseClone();
            List<string> lc = new List<string>();
            foreach (string c in listaActori)
                lc.Add((string)c.Clone());
            clona.listaActori = lc;
            return clona;
        }

        public int CompareTo(object obj)
        {
            Film f = (Film)obj;
            if (this.rating < f.rating)
                return -1;
            else
                if (this.rating > f.rating)
                return 1;
            else
                return 0;
        }
    }
}
