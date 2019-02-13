using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.IO.Ports;
namespace exp1
{
    public partial class gestionOperation : Form
    {
        public gestionOperation()
        {
            InitializeComponent();
        }
        public static string MyConString = "SERVER=localhost;" + "DATABASE=adsl;" + "UID=root;" + "PASSWORD=;";
        public void affichage()
        {


            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT * FROM operation";
            MySqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {
               //string dateformater = Reader.GetString("datepaiement");
                //dateformater = dateformater.Substring(0, 10);

                dataGridView1.Rows.Add(Reader.GetString("id_operation"), Reader.GetString("id_contrat"), Reader.GetString("somme"), Reader.GetString("datepaiement"), Reader.GetString("nom_prenom"), Reader.GetString("type"), Reader.GetString("modalite"), Reader.GetString("source"));

            }
            connection.Close();

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void gestionOperation_Load(object sender, EventArgs e)
        {
            affichage();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT * FROM operation where id_contrat like '%"+ textBox4.Text +"%' or nom_prenom like '%"+ textBox4.Text+"%' ";
            MySqlDataReader Reader = command.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (Reader.Read())
            {
                //string dateformater = Reader.GetString("datepaiement");
                //dateformater = dateformater.Substring(0, 10);

                dataGridView1.Rows.Add(Reader.GetString("id_operation"), Reader.GetString("id_contrat"), Reader.GetString("somme"), Reader.GetString("datepaiement"), Reader.GetString("nom_prenom"), Reader.GetString("type"), Reader.GetString("modalite"));

            }
            connection.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int i = 0;
            Decimal somme = 0; 
            int nbligne = dataGridView1.Rows.Count;
           // MessageBox.Show(nbligne.ToString());
            while (i <= (nbligne - 2))
            {
                //MessageBox.Show(i.ToString());
               // somme = somme  + Convert.ToDecimal( dataGridView1.Rows[i].Cells[2].Value.ToString() )  ;
                i++;

            }
            dataGridView1.Rows.Add("Nombre operation", nbligne.ToString(), "Somme benefice ", somme , "","","","");
           // MessageBox.Show(somme.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
