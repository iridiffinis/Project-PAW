using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;

namespace Inchirieri_de_casete_video
{
    public partial class FormFilme : Form
    {
        string connStringFilme;
        public FormFilme()
        {
            InitializeComponent();
            connStringFilme = "Provider =  Microsoft.ACE.OLEDB.12.0; Data Source = filme.accdb";
        }

        private void FormFilme_Load(object sender, EventArgs e)
        {
            OleDbConnection conexiune = new OleDbConnection(connStringFilme);
            OleDbDataAdapter adaptor = new OleDbDataAdapter("SELECT * FROM filme", conexiune);
            DataSet ds = new DataSet();
            adaptor.Fill(ds, "filme");
            DataTable tabela = ds.Tables["filme"];
            dataGridView1.DataSource = tabela;
        }

        private void stergeSelectiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OleDbConnection conexiune = new OleDbConnection(connStringFilme);
            
            try
            {
                conexiune.Open();
                OleDbCommand comanda = new OleDbCommand();
                comanda.Connection = conexiune;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int id = Convert.ToInt32(row.Cells[0].Value);
                    comanda.CommandText = "DELETE FROM filme WHERE ID=" + id;
                    comanda.ExecuteNonQuery();
                    dataGridView1.Rows.RemoveAt(row.Index);
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexiune.Close();
            }
        }

        private void adaugaRandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OleDbConnection conexiune = new OleDbConnection(connStringFilme);
            OleDbCommand comanda = new OleDbCommand();
            comanda.Connection = conexiune;
            try
            {
                conexiune.Open();
                comanda.CommandText = "INSERT INTO filme VALUES(?,?,?,?,?,?,?,?)";

                int index = dataGridView1.CurrentCell.RowIndex;
                
                comanda.Parameters.Add("ID", OleDbType.Integer).Value = dataGridView1.Rows[index].Cells[0].Value;
                comanda.Parameters.Add("Denumire", OleDbType.LongVarChar).Value = dataGridView1.Rows[index].Cells[1].Value;
                comanda.Parameters.Add("Gen", OleDbType.VarChar).Value = dataGridView1.Rows[index].Cells[2].Value;
                comanda.Parameters.Add("An aparitie", OleDbType.Integer).Value = dataGridView1.Rows[index].Cells[3].Value;
                comanda.Parameters.Add("Regizor", OleDbType.VarChar).Value = dataGridView1.Rows[index].Cells[4].Value;
                comanda.Parameters.Add("Rating", OleDbType.Double).Value = dataGridView1.Rows[index].Cells[5].Value;
                comanda.Parameters.Add("Actori", OleDbType.LongVarChar).Value = dataGridView1.Rows[index].Cells[6].Value;
                comanda.Parameters.Add("Clasificare", OleDbType.VarChar).Value = dataGridView1.Rows[index].Cells[7].Value;
                comanda.ExecuteNonQuery();

                OleDbDataAdapter adaptor = new OleDbDataAdapter("SELECT * FROM filme", conexiune);
                DataSet ds = new DataSet();
                adaptor.Fill(ds, "filme");
                DataTable tabela = ds.Tables["filme"];
                dataGridView1.DataSource = tabela;

                //OpenFileDialog openFile = new OpenFileDialog();
                //openFile.InitialDirectory = @"C:\Users\irina\OneDrive\Documents\Lucruri pentru facultate\An II\Sem 2\Programarea aplicațiilor Windows\seminar\Inchirieri de casete video\bin\Debug";
                //Process.Start("costuri.csv");

                FormCost form = new FormCost();
                form.ShowDialog();

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

        private void acceseazaBazaDeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = @"C:\Users\irina\OneDrive\Documents\Lucruri pentru facultate\An II\Sem 2\Programarea aplicațiilor Windows\seminar\Inchirieri de casete video\bin\Debug";

            //if (openFile.ShowDialog() == DialogResult.OK)
            //{
            //    string cale = openFile.FileName;
            //    Process.Start(cale);
            //}
            Process.Start("filme.accdb");
        }

        private void adaugaCostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = @"C:\Users\irina\OneDrive\Documents\Lucruri pentru facultate\An II\Sem 2\Programarea aplicațiilor Windows\seminar\Inchirieri de casete video\bin\Debug";
            Process.Start("costuri.csv");
            //FormCost form = new FormCost();
            //form.ShowDialog();

        }

        //private void actualizatiDateleToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    OleDbConnection conexiune = new OleDbConnection(connStringFilme);
        //    OleDbCommand comanda = new OleDbCommand();
        //    comanda.Connection = conexiune;
        //    try
        //    {
        //        conexiune.Open();


        //        int index = dataGridView1.CurrentCell.RowIndex;
        //        int coloana = dataGridView1.CurrentCell.ColumnIndex;
        //        string numeColoana = dataGridView1.Columns[coloana].Name;

        //        comanda.CommandText = "UPDATE filme SET ? = ? WHERE ? = ?";



        //        comanda.ExecuteNonQuery();

        //        OleDbDataAdapter adaptor = new OleDbDataAdapter("SELECT * FROM filme", conexiune);
        //        DataSet ds = new DataSet();
        //        adaptor.Fill(ds, "filme");
        //        DataTable tabela = ds.Tables["filme"];
        //        dataGridView1.DataSource = tabela;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        conexiune.Close();
        //    }
        //}

        //private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        //{
        //    e.Row.Cells["ID"].Value = "";
        //    e.Row.Cells["Denumire"].Value = "";
        //    e.Row.Cells["Gen"].Value = "";
        //    e.Row.Cells["An Aparitie"].Value = "";
        //    e.Row.Cells["Regizor"].Value = "";
        //    e.Row.Cells["Rating"].Value = "";
        //    e.Row.Cells["Actori"].Value = "";
        //    e.Row.Cells["Clasificare"].Value = "";
        //}
    }
}
