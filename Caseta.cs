using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inchirieri_de_casete_video
{
    class Caseta : IComparable
    {
        private int filmId;
        private float costStd;
        private int durata_inchiriere;

        public Caseta(int filmId, float costStd, int durata_inchiriere)
        {
            this.filmId = filmId;
            this.costStd = costStd;
            this.durata_inchiriere = durata_inchiriere;
        }

        public float CostStd { get => costStd; set => costStd = value; }
        public int Durata_inchiriere { get => durata_inchiriere; set => durata_inchiriere = value; }
        public int FilmId { get => filmId; set => filmId = value; }

        public int CompareTo(object obj)
        {
            Caseta c = (Caseta)obj;
            if (this.durata_inchiriere < c.durata_inchiriere)
                return -1;
            else
                if (this.durata_inchiriere > c.durata_inchiriere)
                return 1;
            else
                return 0;
        }

        public float tarifCalculat()
        {
            if (durata_inchiriere <= 7)
                return costStd;
            else
            {
                return costStd + 0.5f * durata_inchiriere;
            }
        }
    }
}
