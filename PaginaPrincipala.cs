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
using System.Data.OleDb;

namespace Inchirieri_de_casete_video
{
    public partial class PaginaPrincipala : Form
    {
        string connString;
        List<Film> listaFilme = new List<Film>();
        Dictionary<string, float> ratings = new Dictionary<string, float>();
        int nr = 0;
        bool k = false;
        const int marg = 5;

        Color culoare = Color.DarkSlateGray;
        Font font = new Font(FontFamily.GenericSerif, 9);
        Graphics gr;

        public PaginaPrincipala()
        {
            InitializeComponent();
            connString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = filme.accdb";
            timer1.Start();
        }

        private void evidentaClientiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClienti frmC = new FormClienti();
            frmC.ShowDialog();
        }

        private void tabelaFilmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFilme frmF = new FormFilme();
            frmF.ShowDialog();
        }

        private void evidentaInchirieriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInchirieri frmI = new FormInchirieri();
            frmI.ShowDialog();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            preluareDateFilme();
            foreach(Film f in listaFilme)
            {
                ratings[f.Denumire] = f.Rating;
                nr++;
                k = true;
            }
            pictureBox1.Visible = false;
            splitContainer1.Panel2.Invalidate();
        }

        private void preluareDateFilme()
        {
            OleDbConnection conexiune = new OleDbConnection(connString);
            try
            {
                conexiune.Open();
                OleDbCommand comanda = new OleDbCommand();
                comanda.Connection = conexiune;
                comanda.CommandText = "SELECT * FROM filme";
                OleDbDataReader reader = comanda.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);
                    string denumire = reader["Denumire"].ToString();
                    List<string> genuri = new List<string>(reader["Gen"].ToString().Trim().Split(','));
                    int an_aparitie = Convert.ToInt32(reader["An aparitie"]);
                    string regizor = reader["Regizor"].ToString();
                    float rating = (float)Convert.ToDouble(reader["Rating"]);
                    List<string> actori = new List<string>(reader["Actori"].ToString().Trim().Split(','));
                    string clasificare = reader["Clasificare"].ToString();
                    Film f = new Film(id, denumire, rating, regizor, an_aparitie, actori, clasificare, genuri);
                    listaFilme.Add(f);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexiune.Close();
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            if (k == true)
            {
                Graphics gr = e.Graphics;
                
                Rectangle rec = new Rectangle(splitContainer1.Panel2.ClientRectangle.X+marg,
                    splitContainer1.Panel2.ClientRectangle.Y+2*marg,
                    splitContainer1.Panel2.ClientRectangle.Width-2*marg,
                    splitContainer1.Panel2.ClientRectangle.Height-3*marg);
                Pen pen = new Pen(Color.DarkGreen, 3);
                gr.DrawRectangle(pen, rec);

                float latime = rec.Width / nr / 3;
                float distanta = (rec.Width - nr * latime) / (nr + 1);
                float valMax = ratings.Values.Max();

                Brush br = new SolidBrush(culoare);
                Rectangle[] recs = new Rectangle[nr];
                for (int i = 0; i < nr; i++)
                {
                    recs[i] = new Rectangle((int)(rec.Location.X + (i + 1) * distanta + i * latime),
                        (int)(rec.Location.Y + rec.Height - ratings.ElementAt(i).Value / valMax * rec.Height*0.7),
                        (int)latime,
                        (int)(ratings.ElementAt(i).Value / valMax * rec.Height));
                    gr.FillRectangle(br, recs[i]);
                    gr.DrawString(ratings.ElementAt(i).Key.ToString(), font,
                        br, new Point(recs[i].Location.X, recs[i].Location.Y - font.Height));
                }
                ratings.Clear();
                nr = 0;
                k = false;

                button1.Enabled = false;
                //gr.Clear(splitContainer1.Panel2.BackColor);
                //timer1.Interval = 20000;
                //timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Visible = false;

            timer1.Stop();
        }

        private void splitContainer1_Panel2_Click(object sender, EventArgs e)
        {
            //gr.Clear(Color.Ivory);
            pictureBox1.Visible = true;
            //button1.Enabled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //pictureBox1.Visible = false;
        }
    }
}
