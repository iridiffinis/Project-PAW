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
    public partial class FormClienti : Form
    {
        List<Client> listaClienti = new List<Client>();
        public FormClienti()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbCod.Text == "")
                errorProvider1.SetError(tbCod, "Introduceti codul!");
            else
                if (tbNume.Text == "")
                errorProvider1.SetError(tbNume, "Introduceti numele!");
            else
                if (tbAnNastere.Text == "")
                errorProvider1.SetError(tbAnNastere, "Introduceti anul!");
            else
                if (!(Convert.ToInt32(tbAnNastere.Text) > 1900 && Convert.ToInt32(tbAnNastere.Text) < System.DateTime.Now.Year))
                errorProvider1.SetError(tbAnNastere, "Introduceti un an valid!");
            else
                if (tbEmail.Text == "")
                errorProvider1.SetError(tbEmail, "Introduceti adresa de e-mail!");
            else
            {
                errorProvider1.Clear();
                try
                {
                    int cod = Convert.ToInt32(tbCod.Text);
                    string nume = tbNume.Text;
                    int an_nastere = Convert.ToInt32(tbAnNastere.Text);
                    string email = tbEmail.Text;
                    Client c = new Client(cod, nume, an_nastere, email);
                    listaClienti.Add(c);
                    //MessageBox.Show(c.ToString());

                    //adaugare in listView
                    listViewClienti.Items.Clear();
                    foreach (Client lvc in listaClienti)
                    {
                        ListViewItem itmClient = new ListViewItem(lvc.Cod.ToString());
                        itmClient.SubItems.Add(lvc.Nume);
                        itmClient.SubItems.Add(lvc.calculareVarsta());
                        itmClient.SubItems.Add(lvc.Email);
                        listViewClienti.Items.Add(itmClient);
                    }

                    //salvare in fisier
                    StreamWriter swCienti = new StreamWriter("clienti.csv");
                    foreach (Client fc in listaClienti)
                    {
                        swCienti.Write(fc.Cod);
                        swCienti.Write(",");
                        swCienti.Write(fc.Nume);
                        swCienti.Write(",");
                        swCienti.Write(fc.An_nastere);
                        swCienti.Write(",");
                        swCienti.Write(fc.Email);
                        swCienti.WriteLine();
                    }
                    swCienti.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    tbCod.Clear();
                    tbNume.Clear();
                    tbAnNastere.Clear();
                }
            }
        }

        private void tbAnNastere_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)8)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void FormClienti_Load(object sender, EventArgs e)
        {
            //preluare date
            StreamReader srClienti = new StreamReader("clienti.csv");
            string linie = null;
            while((linie = srClienti.ReadLine()) != null)
            {
                int cod = Convert.ToInt32(linie.Split(',')[0]);
                string nume = linie.Split(',')[1];
                int an_nastere = Convert.ToInt32(linie.Split(',')[2]);
                string email = linie.Split(',')[3];
                Client c = new Client(cod, nume, an_nastere, email);
                listaClienti.Add(c);
            }
            srClienti.Close();

            //afisare date
            listViewClienti.Items.Clear();
            foreach (Client lvc in listaClienti)
            {
                ListViewItem itmClient = new ListViewItem(lvc.Cod.ToString());
                itmClient.SubItems.Add(lvc.Nume);
                itmClient.SubItems.Add(lvc.calculareVarsta());
                itmClient.SubItems.Add(lvc.Email);
                listViewClienti.Items.Add(itmClient);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem itmClient in listViewClienti.Items)
            {
                if(itmClient.Checked)
                {
                    int cod = Convert.ToInt32(itmClient.SubItems[0].Text);
                    for (int i = 0; i < listaClienti.Count; i++)
                        if (listaClienti[i].Cod == cod)
                            listaClienti.RemoveAt(i);
                    itmClient.Remove();
                }
            }
            StreamWriter swCienti = new StreamWriter("clienti.csv");
            foreach (Client fc in listaClienti)
            {
                swCienti.Write(fc.Cod);
                swCienti.Write(",");
                swCienti.Write(fc.Nume);
                swCienti.Write(",");
                swCienti.Write(fc.An_nastere);
                swCienti.Write(",");
                swCienti.Write(fc.Email);
                swCienti.WriteLine();
            }
            swCienti.Close();

            button2.Visible = false;
        }

        private void listViewClienti_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if(e.Item.Checked)
            {
                button2.Visible = true;
            }
        }

        private void stergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem itmClient in listViewClienti.Items)
            {
                if (itmClient.Selected)
                {
                    int cod = Convert.ToInt32(itmClient.SubItems[0].Text);
                    for (int i = 0; i < listaClienti.Count; i++)
                        if (listaClienti[i].Cod == cod)
                            listaClienti.RemoveAt(i);
                    itmClient.Remove();
                }
            }
            StreamWriter swCienti = new StreamWriter("clienti.csv");
            foreach (Client fc in listaClienti)
            {
                swCienti.Write(fc.Cod);
                swCienti.Write(",");
                swCienti.Write(fc.Nume);
                swCienti.Write(",");
                swCienti.Write(fc.An_nastere);
                swCienti.Write(",");
                swCienti.Write(fc.Email);
                swCienti.WriteLine();
            }
            swCienti.Close();
        }

        private void copiereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem itmClient in listViewClienti.Items)
            {
                string text = "";
                if (itmClient.Selected)
                {
                    //int cod = Convert.ToInt32(itmClient.SubItems[0].Text);
                    //for (int i = 0; i < listaClienti.Count; i++)
                    //    if (listaClienti[i].Cod == cod)
                    //        Clipboard.SetDataObject(listaClienti[i]);
                    text += itmClient.SubItems[0].Text + "," + itmClient.SubItems[1].Text + "," + itmClient.SubItems[2].Text + "," + itmClient.SubItems[3].Text;
                    Clipboard.SetText(text);
                }
            }
        }

        private void listViewClienti_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode)
            {
                foreach (ListViewItem itm in listViewClienti.Items)
                {
                    if (itm.Checked || itm.Selected)
                    {
                        itm.Remove();
                    }
                }
            }
        }

        
    }
}
