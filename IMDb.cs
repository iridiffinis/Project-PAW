using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Inchirieri_de_casete_video
{
    public partial class IMDb : UserControl
    {
        public IMDb()
        {
            InitializeComponent();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.imdb.com/");
        }

        private void tbCauta_KeyDown(object sender, KeyEventArgs e)
        {
            string link = "https://www.imdb.com/find?q=";
            if (Keys.Enter == e.KeyCode)
            {
                string film = tbCauta.Text;
                char[] sep = { ' ' };
                int nr_cuv = film.Split(sep, StringSplitOptions.RemoveEmptyEntries).Length;
                string[] cuv = film.Split(sep);
                //link += cuv[0];
                for(int i=0;i<nr_cuv;i++)
                {
                    if (i == nr_cuv - 1)
                        link += cuv[i];
                    else
                        link += cuv[i] + "+";
                }
                Process.Start(link);
                tbCauta.Clear();
            }
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (((e.KeyState & 8) == 8 &&
                (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy))
                e.Effect = DragDropEffects.Copy;
            else
                if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                e.Effect = DragDropEffects.Move;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string link = "https://www.imdb.com/find?q=";
            
            string film = e.Data.GetData(typeof(string)).ToString();
            char[] sep = { ' ' };
            int nr_cuv = film.Split(sep, StringSplitOptions.RemoveEmptyEntries).Length;
            string[] cuv = film.Split(sep);
            //link += cuv[0];
            for (int i = 0; i < nr_cuv; i++)
            {
                if (i == nr_cuv - 1)
                    link += cuv[i];
                else
                    link += cuv[i] + "+";
            }
            Process.Start(link);
        }
    }
}
