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
    public partial class gestionclient : Form
    {
        public gestionclient()
        {
            InitializeComponent();
        }

        public static string MyConString = "SERVER=localhost;" + "DATABASE=adsl;" + "UID=root;" + "PASSWORD=;";

        public void affichage()
        {


            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT * FROM client";
            MySqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {

              dataGridView1.Rows.Add(Reader.GetString("id_client"), Reader.GetString("nom_prenom"), Reader.GetString("adresse"), Reader.GetString("date_naissance"), Reader.GetString("cin"), Reader.GetString("date_cin"), Reader.GetString("numero_gsm"), Reader.GetString("numero_fixe"));

            }
            connection.Close();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void gestionclient_Load(object sender, EventArgs e)
        {
            this.Width = MDIParent1.ActiveForm.Width;
            this.Height = 800;

            affichage();
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

                        textBox1.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[0].Value.ToString();
                        textBox2.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[1].Value.ToString();
                        textBox3.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[2].Value.ToString();
                        dateTimePicker1.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[3].Value.ToString();
                        textBox8.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[4].Value.ToString();
                        dateTimePicker2.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[5].Value.ToString();
                        textBox6.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[6].Value.ToString();
                        textBox7.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[7].Value.ToString();


                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into client (`nom_prenom`, `adresse`, `date_naissance`, `cin`, `date_cin`, `numero_gsm`, `numero_fixe`) values ('" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "','" + textBox8.Text + "','" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "','" + textBox6.Text + "','" + textBox7.Text + "')";
            connection.Open();
           // MessageBox.Show(command.CommandText);
            command.ExecuteNonQuery();
            MessageBox.Show("client  ajouter avec succés ");
            connection.Close();

            dataGridView1.Rows.Clear();
            affichage();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Veuillez selectionner un client à supprimer");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(MyConString);
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "delete from  client   where id_client = '" + textBox1.Text + "'";
                //MessageBox.Show(command.CommandText);
                connection.Open();

                command.ExecuteNonQuery();
                MessageBox.Show("client supprimer avec success");
                //if (MessageBox.Show("vous devez vraiment supprimer cette client !!", "Message", MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes) ;
                dataGridView1.Rows.Clear();
                connection.Close();

                affichage();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Veuillez selectionner un client à modifier");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(MyConString);
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "update client set nom_prenom='" + textBox2.Text + "',adresse='" + textBox3.Text + "' , date_naissance='" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' , cin='" + textBox8.Text + "' ,date_cin='" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "' ,numero_gsm='" + textBox6.Text + "' ,numero_fixe='" +textBox7.Text +"'  where id_client = '" + textBox1.Text + "'";
                //MessageBox.Show(command.CommandText);
                connection.Open();
                command.ExecuteNonQuery();


                MessageBox.Show("client modifier avec success");
                dataGridView1.Rows.Clear();
                connection.Close();

                affichage();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox8.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";


            }
        }

        public int i;
        private void button4_Click(object sender, EventArgs e)
        {
            Document docum = new Document(PageSize.LETTER, 50f, 50f, 80f, 60f);
            PdfWriter wri = PdfWriter.GetInstance(docum, new FileStream(@"C:\Intel\ticketclient.pdf", FileMode.Create));
            docum.Open();

            Paragraph dat = new Paragraph(DateTime.Now + " GMT")
            {
                Alignment = 0
            };
            docum.Add(dat);
            Paragraph paragraphe = new Paragraph("Liste des client\n ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 22f, 0, BaseColor.BLACK))
            {
                Alignment = 0
            };
            docum.Add(paragraphe);
            Paragraph paragraphe2 = new Paragraph("client:" + textBox1.Text + "\n\n", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 14f, 0, BaseColor.BLACK))
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
            table.AddCell("Nom_Prenom");
            table.AddCell("Adresse");
            table.AddCell("Date_Nass");
            table.AddCell("Cin");
            table.AddCell("Date_Cin");
            table.AddCell("Numero_Gsm");
            table.AddCell("Numero_Fixe");
            


          //  int nbligne = dataGridView1.Rows.Count;
           // MessageBox.Show(nbligne.ToString());
          //  while (i <= (nbligne-2))
           // {
             //   MessageBox.Show(i.ToString());
                table.AddCell(dataGridView1.Rows[i].Cells[0].Value.ToString());
                table.AddCell(dataGridView1.Rows[i].Cells[1].Value.ToString());
                table.AddCell(dataGridView1.Rows[i].Cells[2].Value.ToString());
                table.AddCell(dataGridView1.Rows[i].Cells[3].Value.ToString());
                table.AddCell(dataGridView1.Rows[i].Cells[4].Value.ToString());
                table.AddCell(dataGridView1.Rows[i].Cells[5].Value.ToString());
                table.AddCell(dataGridView1.Rows[i].Cells[6].Value.ToString());
                table.AddCell(dataGridView1.Rows[i].Cells[7].Value.ToString());

                
            //    i++;
           // }

            docum.Add(table);
           // Paragraph pay2 = new Paragraph("Total TTC : " + label1.Text + "\n Merci pour votre visite ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 14f, 1, BaseColor.BLACK));
           // docum.Add(pay2);


            docum.Close();
            //Pdf.PrintPDFs(@"C:\temp\ticket.pdf");
            MessageBox.Show("Docuement creer avec success");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
        }

       

