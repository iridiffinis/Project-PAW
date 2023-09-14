using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inchirieri_de_casete_video
{
    class Inchiriere : ICloneable, IComparable
    {
        private List<Film> listaFilme;
        private Client client;
        private List<Caseta> listaCasete;


        public Inchiriere(List<Film> listaFilme, Client client, int durata_inchiriere, List<Caseta> listaCasete)
        {
            this.listaFilme = listaFilme;
            this.client = client;
            this.listaCasete = listaCasete;
        }

        internal List<Film> ListaFilme { get => listaFilme; set => listaFilme = value; }
        internal Client Client { get => client; set => client = value; }
        internal List<Caseta> ListaCasete { get => listaCasete; set => listaCasete = value; }

        public object Clone()
        {
            Inchiriere clona = (Inchiriere)this.MemberwiseClone();
            List<Film> lc = new List<Film>();
            foreach (Film f in listaFilme)
                lc.Add((Film)f.Clone());
            clona.listaFilme = lc;
            return clona;
        }

        public int CompareTo(object obj)
        {
            Inchiriere i = (Inchiriere)obj;
            if (this.listaCasete.Count < i.listaCasete.Count)
                return -1;
            else
                if (this.listaCasete.Count > i.listaCasete.Count)
                return 1;
            else
                return 0;
        }
    }
}
