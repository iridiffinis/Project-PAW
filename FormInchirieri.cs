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
using System.Drawing.Printing;

namespace Inchirieri_de_casete_video
{
    public partial class FormInchirieri : Form
    {
        string connString;
        List<Client> listaClienti = new List<Client>();
        List<Film> listaFilme = new List<Film>();
        Dictionary<int, float> listaCosturi = new Dictionary<int, float>();
        List<Caseta> caseteInchiriate = new List<Caseta>();
        string clientNou;
        private Font font;
        private StreamReader reader;

        public FormInchirieri()
        {
            InitializeComponent();
            connString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = filme.accdb";
        }

        private void FormInchirieri_Load(object sender, EventArgs e)
        {
            preluareDateClienti();
            preluareDateFilme();
            preluareCosturi();

            foreach(Client c in listaClienti)
            {
                cbClient.Items.Add(c.Cod + " - " + c.Nume);
            }
        }

        private void preluareCosturi()
        {
            StreamReader srCost = new StreamReader("costuri.csv");
            string linie = null;
            while((linie=srCost.ReadLine())!=null)
            {
                int idFilm = Convert.ToInt32(linie.Split(',')[0]);
                float costStd = (float)Convert.ToDouble(linie.Split(',')[1]);
                listaCosturi[idFilm] = costStd;
            }
            srCost.Close();
        }

        private void preluareDateClienti()
        {
            StreamReader srClienti = new StreamReader("clienti.csv");
            string linie = null;
            while ((linie = srClienti.ReadLine()) != null)
            {
                int cod = Convert.ToInt32(linie.Split(',')[0]);
                string nume = linie.Split(',')[1];
                int an_nastere = Convert.ToInt32(linie.Split(',')[2]);
                string email = linie.Split(',')[3];
                Client c = new Client(cod, nume, an_nastere, email);
                listaClienti.Add(c);
            }
            srClienti.Close();
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

        private void cbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            listView1.Visible = true;
            clientNou = cbClient.Text;
            cbFilm.Items.Clear();
            foreach (Film f in listaFilme)
                cbFilm.Items.Add(f.Denumire);
        }

        private void cbFilm_MouseEnter(object sender, EventArgs e)
        {
            if (cbFilm.SelectedItem != null)
                toolTip1.SetToolTip(cbFilm, cbFilm.SelectedText);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string film = cbFilm.Text;
                int durataInch = Convert.ToInt32(tbDurata.Text);
                float costFilm, tarif;
                int idFilm;

                //adugare tarif calculat in functie de durata
                foreach(Film f in listaFilme)
                {
                    if (f.Denumire == film)
                    {
                        idFilm = f.Id;
                        foreach (KeyValuePair<int, float> costF in listaCosturi)
                        {
                            if (f.Id == costF.Key)
                            {
                                costFilm = costF.Value;
                                Caseta caseta = new Caseta(idFilm, costFilm, durataInch);
                                caseteInchiriate.Add(caseta);
                                
                            }
                        }
                    }
                }
                Caseta cst = caseteInchiriate[caseteInchiriate.Count - 1];
                tarif = cst.tarifCalculat();

                ListViewItem itm = new ListViewItem(film);
                itm.SubItems.Add(durataInch.ToString());
                itm.SubItems.Add(tarif.ToString());
                listView1.Items.Add(itm);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cbFilm.Text="";
                tbDurata.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float tarif=0.0f;
            string filme = "";
            ListViewItem item = new ListViewItem(clientNou);
            foreach(ListViewItem inch in listView1.Items)
            {
                filme +=inch.SubItems[0].Text.ToString()+"; ";
                tarif += (float)Convert.ToDouble(inch.SubItems[2].Text);
            }
            item.SubItems.Add(filme);
            item.SubItems.Add(tarif.ToString());
            listView2.Items.Add(item);
            listView1.Items.Clear();
            //cbFilm.Items.Clear();
        }

        private void formatTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "(*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                foreach(ListViewItem itm in listView2.Items)
                {
                    sw.WriteLine("{0}{1}{2}{3}{4}", itm.SubItems[0].Text, " | ",itm.SubItems[1].Text, " | ",itm.SubItems[2].Text);
                    
                }
                sw.Close();
            }
        }

        private void formatCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "(*.csv)|*.csv";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                //sw.WriteLine("Nume client,Filme inchiriate,Tarif");
                foreach(ListViewItem itm in listView2.Items)
                {
                    sw.WriteLine(string.Format("{0},{1},{2}", itm.SubItems[0].Text, itm.SubItems[1].Text, itm.SubItems[2].Text));

                }
                sw.Close();
            }
        }

        private void imprimareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string numeFisier;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Fisier pentru imprimare";
            openFile.InitialDirectory = @"C:\Users\irina\OneDrive\Documents\Lucruri pentru facultate\An II\Sem 2\Programarea aplicațiilor Windows\seminar\Inchirieri de casete video\bin\Debug";
            openFile.Filter = "Text files (*.txt | .txt | All files (*.*) | *.*";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                numeFisier = openFile.FileName;
                reader = new StreamReader(numeFisier);
                font = new Font("Helvetica", 11);
                printPreviewDialog1.ShowDialog();
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                //printDocument1.Print();
                
                reader.Close();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float liniiPerPagina = 0;
            float yPoz = 0;
            int nr = 0;
            float stgMargine = e.MarginBounds.Left;
            float susMargine = e.MarginBounds.Top;
            string linie = null;
            liniiPerPagina = e.MarginBounds.Height / font.GetHeight(g);
            while(nr<liniiPerPagina &&((linie=reader.ReadLine())!=null))
            {
                yPoz = susMargine + (nr * font.GetHeight(g));
                g.DrawString(linie, font, Brushes.Black, stgMargine, yPoz, new StringFormat());
                nr++;
            }
            if (linie != null)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        private void stergereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem itm in listView2.Items)
            {
                if (itm.Checked || itm.Selected)
                {
                    itm.Remove();
                }
            }
        }

        private void preluareDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "(*.csv) | *.csv";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                
                string linie = null;
                while ((linie = sr.ReadLine()) != null)
                {
                    ListViewItem itm = new ListViewItem(linie.Split(',')[0]);
                    itm.SubItems.Add(linie.Split(',')[1]);
                    itm.SubItems.Add(linie.Split(',')[2]);
                    listView2.Items.Add(itm);
                }
                
                                
                sr.Close();
            }
        }

        private void listView2_KeyDown(object sender, KeyEventArgs e)
        {
            if(Keys.Delete==e.KeyCode)
            {
                foreach (ListViewItem itm in listView2.Items)
                {
                    if (itm.Checked || itm.Selected)
                    {
                        itm.Remove();
                    }
                }
            }
        }

        private void reinitializareEvidantaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Inchideti formularul pentru inchirieri de casete?","Iesire",MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
                this.Close();
            }
        }

        private void cbFilm_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbFilm.SelectedIndex > -1 && cbFilm.DroppedDown == false)
                cbFilm.DoDragDrop(cbFilm.Text, DragDropEffects.Copy | DragDropEffects.Move);
            else
                cbFilm.DroppedDown = true;
        }
    }
}
