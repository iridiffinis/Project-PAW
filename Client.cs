using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inchirieri_de_casete_video
{
    class Client : ICloneable, IComparable
    {
        private readonly int cod;
        private string nume;
        private int an_nastere;
        private string email;

        public Client(int cod, string nume, int an_nastere, string email)
        {
            this.cod = cod;
            this.nume = nume;
            this.an_nastere = an_nastere;
            this.email = email;
        }

        public int Cod => cod;

        public string Nume { get => nume; set => nume = value; }
        public int An_nastere { get => an_nastere; set => an_nastere = value; }
        public string Email { get => email; set => email = value; }

        public string calculareVarsta()
        {
            return (System.DateTime.Now.Year - an_nastere).ToString();
        }

        public object Clone()
        {
            return (Client)this.MemberwiseClone();
        }

        public int CompareTo(object obj)
        {
            Client c = (Client)obj;
            if (Int32.Parse(this.calculareVarsta()) < Int32.Parse(c.calculareVarsta()))
                return -1;
            else
                if (Int32.Parse(this.calculareVarsta()) > Int32.Parse(c.calculareVarsta()))
                return 1;
            else
                return 0;
        }

        public override string ToString()
        {
            return "Clientul cu codul " + cod + " se numeste " + nume + " si este in varsta de " + this.calculareVarsta() + " de ani.";
        }
    }
}
