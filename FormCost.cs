using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Inchirieri_de_casete_video
{
    public partial class FormCost : Form
    {
        public FormCost()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //StreamWriter sw = new StreamWriter("");
            //sw.Write(tbID.Text);
            //sw.Write(",");
            //sw.Write(tbCost.Text);
            //sw.WriteLine();
            //sw.Close();

            string fisier = "costuri.csv";
            string costNou = tbID.Text + "," + tbCost.Text;

            File.AppendAllText(fisier, costNou);

            tbID.Clear();
            tbCost.Clear();
        }
    }
}
