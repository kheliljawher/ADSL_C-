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
    public partial class gestionutilisateur : Form
    {
        public gestionutilisateur()
        {
            InitializeComponent();
        }
        public static string MyConString = "SERVER=localhost;" +
                    "DATABASE=adsl;" +
                    "UID=root;" +
                    "PASSWORD=;";
        public void affichage ()
        {


            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "SELECT * FROM user ";
            MySqlDataReader Reader = command.ExecuteReader();
            while (Reader.Read())
            {

                dataGridView1.Rows.Add(Reader.GetString("id_user"), Reader.GetString("login"), Reader.GetString("password"), Reader.GetString("nom_prenom"), Reader.GetString("type"));

            }
            connection.Close();
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string MyConString = "SERVER=localhost;DATABASE=adsl;UID=root;password=";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into user (`login`, `password`, `nom_prenom`, `type`) values ('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','"+ comboBox1.Text+"')";
            connection.Open();
            command.ExecuteNonQuery();
            MessageBox.Show("utilisateur ajouter avec succés ");
            connection.Close();

            dataGridView1.Rows.Clear();
            affichage();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
        }

        private void gestionutilisateur_Load(object sender, EventArgs e)
        {
            this.Width = MDIParent1.ActiveForm.Width; 
            this.Height = 800 ;
           // string MyConString = "SERVER=localhost;DATABASE=adsl;UID=root;password=";
            affichage();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 selectedCellCount =dataGridView1.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                if (dataGridView1.AreAllCellsSelected(true))
                {
                    MessageBox.Show("All cells are selected", "Selected Cells");
                }
                else
                {
                    System.Text.StringBuilder sb =new System.Text.StringBuilder();

                    for (int i = 0; i < selectedCellCount; i++)
                    {
                        sb.Append("Row: ");
                        sb.Append(dataGridView1.SelectedCells[i].RowIndex.ToString());
                        sb.Append(", Column: ");
                        sb.Append(dataGridView1.SelectedCells[i].ColumnIndex.ToString());
                        sb.Append(Environment.NewLine);

                        textBox2.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[1].Value.ToString();
                        textBox3.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[2].Value.ToString();
                        textBox1.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[0].Value.ToString();
                        comboBox1.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[4].Value.ToString();
                        textBox4.Text = dataGridView1.Rows[dataGridView1.SelectedCells[i].RowIndex].Cells[3].Value.ToString();

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("Veuillez selectionner un utilisateur à supprimer");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(MyConString);
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "delete from  user   where id_user = '" + textBox1.Text + "'";
                //MessageBox.Show(command.CommandText);
                connection.Open();

                command.ExecuteNonQuery();
                MessageBox.Show("utilisateur supprimer avec success");
                dataGridView1.Rows.Clear();
                connection.Close();

                affichage();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                comboBox1.Text = "";


            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Veuillez selectionner un utilisateur à modifier");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection(MyConString);
                MySqlCommand command = connection.CreateCommand();

                command.CommandText = "update user set login='"+ textBox2.Text+"',password='"+ textBox3.Text+"' , nom_prenom='"+textBox4.Text+"' , type='"+comboBox1.Text+"'  where id_user = '" + textBox1.Text + "'";
                //MessageBox.Show(command.CommandText);
                connection.Open();

                command.ExecuteNonQuery();
                MessageBox.Show("utilisateur modifier avec success");
                dataGridView1.Rows.Clear();
                connection.Close();

                affichage();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                comboBox1.Text = "";



            }
        }
        public int i ;
        private void button4_Click(object sender, EventArgs e)
        {
              
            Document docum = new Document(PageSize.LETTER, 50f, 50f, 80f, 60f);
            PdfWriter wri = PdfWriter.GetInstance(docum, new FileStream(@"C:\Intel\ticket.pdf", FileMode.Create));
            docum.Open();

            Paragraph dat = new Paragraph(DateTime.Now + " GMT")
            {
                Alignment = 0
            };
            docum.Add(dat);
            Paragraph paragraphe = new Paragraph("Liste des utilisateurs\n ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 22f, 0, BaseColor.BLACK))
            {
                Alignment = 0
            };
            docum.Add(paragraphe);
            Paragraph paragraphe2 = new Paragraph("utilisateur:" + comboBox1.Text + "\n\n", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 14f, 0, BaseColor.BLACK))
            {
                Alignment = Right
            };
            paragraphe2.Alignment = Element.ALIGN_RIGHT;
            docum.Add(paragraphe2);
            Paragraph pay1 = new Paragraph("\n", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 12f, 0, BaseColor.BLACK));
            docum.Add(pay1);
            PdfPTable table = new PdfPTable(5);
            table.HorizontalAlignment = 0;
            table.TotalWidth = 550f;
            table.LockedWidth = true;

            table.AddCell("id_user");
            table.AddCell("login");
            table.AddCell("password");
            table.AddCell("nom_prenom");
            table.AddCell("type");
            


            int nbligne = dataGridView1.Rows.Count;
           // MessageBox.Show(nbligne.ToString());
          //  while (i <= (nbligne-2))
            //{
              //  MessageBox.Show(i.ToString());
                table.AddCell(dataGridView1.Rows[i].Cells[0].Value.ToString());
                table.AddCell(dataGridView1.Rows[i].Cells[1].Value.ToString());
                table.AddCell(dataGridView1.Rows[i].Cells[2].Value.ToString());
                table.AddCell(dataGridView1.Rows[i].Cells[3].Value.ToString());
                table.AddCell(dataGridView1.Rows[i].Cells[4].Value.ToString());
               

                
               // i++;
          //  }

            docum.Add(table);
            Paragraph pay2 = new Paragraph("\n Merci pour votre visite ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 14f, 1, BaseColor.BLACK));
            docum.Add(pay2);


            docum.Close();
            //Pdf.PrintPDFs(@"C:\temp\ticket.pdf");
            MessageBox.Show("Docuement creé avec success");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
