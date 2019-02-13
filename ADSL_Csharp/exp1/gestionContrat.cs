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
using System.Globalization;

namespace exp1
{
    public partial class gestionContrat : Form
    {
        public gestionContrat()
        {
            InitializeComponent();
        }
        public static string MyConString = "SERVER=localhost;" + "DATABASE=adsl;" + "UID=root;" + "PASSWORD=;";

        public void affichage()
        {


            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT * FROM contrat,client,tarif where contrat.id_client=client.id_client and contrat.id_tarif=tarif.id_tarif";
            MySqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string dateformater = Reader.GetString("date");
                dateformater = dateformater.Substring(0, 10);

                dataGridView1.Rows.Add(Reader.GetString("id_contrat"), Reader.GetString("modalite"),Reader.GetString("nom_prenom"),Reader.GetString("nom_produit") ,dateformater);

            }
            connection.Close();

        }
        private void gestionContrat_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT * FROM client";
            MySqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {

                comboBox1.Items.Add(Reader.GetString("nom_prenom")   );
            }
           

            //connection.Close();

            MySqlConnection connection1 = new MySqlConnection(MyConString);
            MySqlCommand command1 = connection1.CreateCommand();
            connection1.Open();
            command1.CommandText = "SELECT * FROM tarif";
            MySqlDataReader Reader1 = command1.ExecuteReader();
            while (Reader1.Read())
            {

                comboBox2.Items.Add(Reader1.GetString("nom_produit"));
            }


            connection1.Close();
            affichage();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT * FROM  tarif where nom_produit='"+ comboBox2.Text + "'";
            MySqlDataReader Reader = command.ExecuteReader();
            label9.Text = "";
            while (Reader.Read())
            {
                label9.Text = Reader.GetString("prix");

            }
            connection.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
          label7.Text =Convert.ToString( Convert.ToDecimal(  comboBox3.Text )* Convert.ToDecimal( label9.Text) );

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            MySqlConnection connection2 = new MySqlConnection(MyConString);
            MySqlCommand command2 = connection2.CreateCommand();
            connection2.Open();
            command2.CommandText = "SELECT count(id_client) as nbr FROM  client  ";
            MySqlDataReader Reader2 = command2.ExecuteReader();
            Reader2.Read();
            if(  Reader2.GetUInt16("nbr") >13 )
            {
                MessageBox.Show("Vous avez attendre nombre limite des clients veuillez nous contacter 52155530 ");
                //while (Reader.Read())
                connection2.Close();
            }
           
            else
            { 
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into contrat (`modalite`, `date` ,`id_client`,`id_tarif`) values ('" + comboBox3.Text + "','" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "',(select id_client from client where nom_prenom='" + comboBox1.Text + "'),(select id_tarif from tarif where nom_produit='" + comboBox2.Text + "'))";
            connection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("contrat ajouter avec succés ");
            connection.Close();

            dataGridView1.Rows.Clear();
            affichage();
            textBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            label9.Text  = "";
            label7.Text = "";

            }
        }



      

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Veuillez selectionner un contrat à modifier");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(MyConString);
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "update contrat set modalite='" + comboBox1.Text + "',nom_prenom='" + comboBox2.Text + "',nom_produit='" + comboBox3.Text + "'  where id_contrat = '" + textBox1.Text + "'";
                //MessageBox.Show(command.CommandText);
                connection.Open();
                command.ExecuteNonQuery();


                MessageBox.Show("contrat modifier avec success");
                dataGridView1.Rows.Clear();
                connection.Close();

                affichage();

                textBox1.Text= "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                label9.Text = "";
                label7.Text = "";


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Veuillez selectionner un contrat à supprimer");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(MyConString);
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "delete from contrat where id_contrat = '" + textBox1.Text + "'";
                //MessageBox.Show(command.CommandText);
                connection.Open();

                command.ExecuteNonQuery();
                MessageBox.Show("contrat supprimer avec success");
                dataGridView1.Rows.Clear();
                connection.Close();

                affichage();

                textBox1.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                label9.Text  = "";
                label7.Text = "";


            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 selectedCellCount = dataGridView1.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                if (dataGridView1.AreAllCellsSelected(true))
                {
                    MessageBox.Show("All cells are selected", "Selected Cells");
                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    for (int i = 0; i < selectedCellCount; i++)
                    {
                        sb.Append("Row: ");
                        sb.Append(dataGridView1.SelectedCells[i].RowIndex.ToString());
                        sb.Append(", Column: ");
                        sb.Append(dataGridView1.SelectedCells[i].ColumnIndex.ToString());
                        sb.Append(Environment.NewLine);

                        comboBox1.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[2].Value.ToString();
                        textBox1.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[0].Value.ToString();
                        comboBox2.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[3].Value.ToString();
                        dateTimePicker1.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[4].Value.ToString();
                        comboBox3.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[1].Value.ToString();
                       
                    }
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           // label7.Text = Convert.ToString(Convert.ToDecimal(comboBox3.Text) * Convert.ToDecimal(textBox2.Text));

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Document docum = new Document(PageSize.LETTER, 50f, 50f, 80f, 60f);
            PdfWriter wri = PdfWriter.GetInstance(docum, new FileStream(@"C:\Intel\facture.pdf", FileMode.Create));
            docum.Open();

            Paragraph dat = new Paragraph(DateTime.Now + " GMT")
            {
                Alignment = 0
            };
            docum.Add(dat);
            Paragraph paragraphe = new Paragraph("Facture\n ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 22f, 0, BaseColor.BLACK))
            {
                Alignment = 0
            };
            docum.Add(paragraphe);
            Paragraph paragraphe2 = new Paragraph("utilisateur:" + connexion.nomuser + "\n\n", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 14f, 0, BaseColor.BLACK))
            {
                Alignment = Right
            };
            paragraphe2.Alignment = Element.ALIGN_RIGHT;
            docum.Add(paragraphe2);
            Paragraph pay1 = new Paragraph("\n", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 12f, 0, BaseColor.BLACK));
            docum.Add(pay1);
            PdfPTable table2 = new PdfPTable(5);
            table2.HorizontalAlignment = 0;
            table2.TotalWidth = 550f;
            table2.LockedWidth = true;

            table2.AddCell("N contact");
            table2.AddCell("Nom client");
            table2.AddCell("Type ADSL");
            table2.AddCell("modalite");
            table2.AddCell("montant");

           
               
                table2.AddCell(textBox1.Text);
                table2.AddCell(comboBox1.Text);
                table2.AddCell(comboBox2.Text);
                table2.AddCell(comboBox3.Text);
                table2.AddCell(label7.Text);




            docum.Add(table2);
            Paragraph pay2 = new Paragraph("\n Merci pour votre visite ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 14f, 1, BaseColor.BLACK));
            docum.Add(pay2);

            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into operation (`id_contrat`, `nom_prenom`, `type`, `modalite`, `somme`,`datepaiement`,`source`) values ('" + textBox1.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + label7.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+ connexion.nomuser+"')";
            connection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("operation ajouter avec succés ");
            connection.Close();

            dataGridView1.Rows.Clear();
            affichage();

            docum.Close();
            //Pdf.PrintPDFs(@"C:\temp\ticket.pdf");
            MessageBox.Show("Docuement creé avec success");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }




    }
   

}

