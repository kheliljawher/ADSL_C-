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
    public partial class gestiontarif : Form
    {
        public gestiontarif()
        {
            InitializeComponent();
        }

        public static string MyConString = "SERVER=localhost;" +
                    "DATABASE=adsl;" +
                    "UID=root;" +
                    "PASSWORD=;";
        public void affichage()
        {


            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT * FROM tarif ";
            MySqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {

                dataGridView1.Rows.Add(Reader.GetString("id_tarif"), Reader.GetString("nom_produit"), Reader.GetString("duree"), Reader.GetString("prix"), Reader.GetString("tva"));

            }
            connection.Close();

        }

        private void gestiontarif_Load(object sender, EventArgs e)
        {
            this.Width = MDIParent1.ActiveForm.Width;
            this.Height = 800;


            affichage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into tarif (`nom_produit`, `duree`, `prix`, `tva`) values ('" + comboBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";
            connection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("tarif ajouter avec succés ");
            connection.Close();

            dataGridView1.Rows.Clear();
            affichage();

            comboBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Veuillez selectionner un tarif à modifier");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(MyConString);
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "update tarif set nom_produit='" + comboBox1.Text + "',duree='" + textBox2.Text + "' , prix='" + textBox3.Text + "' , tva='" + textBox4.Text + "' where id_tarif = '" + textBox5.Text + "'";
                //MessageBox.Show(command.CommandText);
                connection.Open();

                command.ExecuteNonQuery();
                MessageBox.Show("tarif modifier avec success");
                dataGridView1.Rows.Clear();
                connection.Close();

                affichage();

                comboBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Veuillez selectionner un tarif à supprimer");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(MyConString);
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "delete from  tarif   where id_tarif = '" + textBox5.Text + "'";
                //MessageBox.Show(command.CommandText);
                connection.Open();

                command.ExecuteNonQuery();
                MessageBox.Show("tarif supprimer avec success");
                dataGridView1.Rows.Clear();
                connection.Close();

                affichage();

                comboBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                


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

                        textBox5.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[0].Value.ToString();

                        comboBox1.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[1].Value.ToString();
                        textBox2.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[2].Value.ToString();
                        textBox3.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[3].Value.ToString();

                        textBox4.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[4].Value.ToString();

                    }
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT * FROM  tarif where nom_produit='" + comboBox1.Text + "'";
            MySqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                comboBox1.Text = Reader.GetString("nom_produit");

            }
            connection.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        public int i;
        private void button4_Click(object sender, EventArgs e)
        {
            Document docum = new Document(PageSize.LETTER, 50f, 50f, 80f, 60f);
            PdfWriter wri = PdfWriter.GetInstance(docum, new FileStream(@"C:\Intel\tickettarif.pdf", FileMode.Create));
            docum.Open();

            Paragraph dat = new Paragraph(DateTime.Now + " GMT")
            {
                Alignment = 0
            };
            docum.Add(dat);
            Paragraph paragraphe = new Paragraph("Liste des tarif\n ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 22f, 0, BaseColor.BLACK))
            {
                Alignment = 0
            };
            docum.Add(paragraphe);
            Paragraph paragraphe2 = new Paragraph("tarif:" + comboBox1.Text + "\n\n", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 14f, 0, BaseColor.BLACK))
            {
                Alignment = Right
            };
            paragraphe2.Alignment = Element.ALIGN_RIGHT;
            docum.Add(paragraphe2);
            Paragraph pay1 = new Paragraph("\n", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 12f, 0, BaseColor.BLACK));
            docum.Add(pay1);
            PdfPTable table = new PdfPTable(8);
            table.HorizontalAlignment = 0;
            table.TotalWidth = 550f;
            table.LockedWidth = true;

            table.AddCell("id");
            table.AddCell("Nom_produit");
            table.AddCell("duree");
            table.AddCell("prix");
            table.AddCell("TVA");



           // int nbligne = dataGridView1.Rows.Count;
            // MessageBox.Show(nbligne.ToString());
            //  while (i <= (nbligne-2))
            // {
            //   MessageBox.Show(i.ToString());
            table.AddCell(dataGridView1.Rows[i].Cells[0].Value.ToString());
            table.AddCell(dataGridView1.Rows[i].Cells[1].Value.ToString());
            table.AddCell(dataGridView1.Rows[i].Cells[2].Value.ToString());
            table.AddCell(dataGridView1.Rows[i].Cells[3].Value.ToString());
            table.AddCell(dataGridView1.Rows[i].Cells[4].Value.ToString());


            //    i++;
            // }

            docum.Add(table);
          //  Paragraph pay2 = new Paragraph("Total TTC : " + label1.Text + "\n Merci pour votre visite ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 14f, 1, BaseColor.BLACK));
            //docum.Add(pay2);


            docum.Close();
            //Pdf.PrintPDFs(@"C:\temp\ticket.pdf");
            MessageBox.Show("Docuement creer avec success");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
